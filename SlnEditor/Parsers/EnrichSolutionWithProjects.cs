using SlnEditor.Exceptions;
using SlnEditor.Mappings;
using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithProjects : IEnrichSolution
    {
        private readonly ProjectTypeMap _projectTypeMap = new ProjectTypeMap();

        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {
            if (solution == null) throw new ArgumentNullException(nameof(solution));
            if (fileContents == null) throw new ArgumentNullException(nameof(fileContents));

            // Get projects
            var allProjects = ParseFlatProjectList(fileContents, bestEffort);

            // Process nested project section
            var nestedProjectsSection = SectionParser.ExtractGlobalSection(fileContents, "NestedProjects");
            solution.GlobalSections.Add(
                new NestedProjectsSection(solution) { SourceLine = nestedProjectsSection.SourceStartLine, });
            var nestedProjectMappings = ParseNestedProjectMappings(nestedProjectsSection.Lines);
            AttachChildProjects(allProjects, nestedProjectMappings, bestEffort);

            // Find root project list to use as source of truth, i.e. those with no parent relationships
            solution.RootProjects = allProjects
                .Where(child => allProjects.OfType<SolutionFolder>()
                    .All(parentFolder => !parentFolder.Projects.Contains(child)))
                .ToList();
        }

        /// <summary>
        /// Solution files have all projects listed in a flat list.
        /// Hierarchy is held in a separate <see cref="NestedProjectsSection"/>
        /// </summary>
        private IList<IProject> ParseFlatProjectList(IEnumerable<string> fileContents, bool bestEffort)
        {
            var projectDefinitionParser = new ProjectDefinitionParser();
            var solutionFolderDefinitionParser = new SolutionFolderDefinitionParser();
            var lineNumber = 0;
            var projects = new List<IProject>();
            IProject? currentProject = null;
            var inItemsSection = false;
            foreach (var line in fileContents)
            {
                lineNumber++;
                var project = projectDefinitionParser.Parse(line, lineNumber);
                if (project != null)
                {
                    currentProject = project;
                    continue;
                }

                var solutionFolder = solutionFolderDefinitionParser.Parse(line, lineNumber);
                if (solutionFolder != null)
                {
                    currentProject = solutionFolder;
                    continue;
                }

                var isItemsStart = ParseProjectSolutionItemsSectionStart(line);
                if (isItemsStart)
                {
                    inItemsSection = true;
                    continue;
                }

                if (inItemsSection && currentProject is SolutionFolder currentSolutionFolder)
                {
                    var file = SolutionFolderDefinitionParser.ParseFile(line, lineNumber, bestEffort);
                    if (file != null)
                    {
                        currentSolutionFolder.Files.Add(file);
                    }
                }

                if (ParseProjectSectionEnd(line))
                {
                    inItemsSection = false;
                    continue;
                }

                if (ParseProjectEndLine(line))
                {
                    if (currentProject == null)
                    {
                        if (bestEffort)
                        {
                            continue;
                        }

                        throw new UnexpectedSolutionStructureException(
                            $"Found EndProject when not in project block. Line {lineNumber}");
                    }

                    projects.Add(currentProject);
                    currentProject = null;
                }
            }

            // return fileContents.Select(line => _parser.ParseProjectStartLine(line: line, lineNumber++))
            // .OfType<IProject>().ToList();
            return projects;
        }

        private static IList<NestedProjectMapping> ParseNestedProjectMappings(IEnumerable<string> sectionContents)
            => sectionContents.Select(NestedProjectMapping.Parse).OfType<NestedProjectMapping>().ToList();

        private static void AttachChildProjects(IList<IProject> allProjects,
            IList<NestedProjectMapping> nestedProjectMappings, bool bestEffort)
        {
            foreach (var childProject in allProjects)
            {
                // Find mapping (if any) where this project is the child
                var nestedProjectMapping =
                    nestedProjectMappings.FirstOrDefault(mapping => mapping.ChildProjectId == childProject.Id);
                if (nestedProjectMapping == null)
                {
                    continue;
                }

                var parent = allProjects.FirstOrDefault(project => project.Id == nestedProjectMapping.ParentProjectId);
                if (parent == null) // corrupt mapping
                {
                    if (bestEffort)
                    {
                        continue;
                    }

                    throw new UnexpectedSolutionStructureException(
                        $"Invalid {nameof(NestedProjectMapping)}. Parent project '{nestedProjectMapping.ParentProjectId}' not found");
                }

                if (!(parent is SolutionFolder parentSolutionFolder))
                {
                    if (bestEffort)
                    {
                        continue;
                    }

                    throw new UnexpectedSolutionStructureException(
                        $"Invalid {nameof(NestedProjectMapping)}. Parent project '{nestedProjectMapping.ParentProjectId}' is not a solution folder");
                }

                parentSolutionFolder.Projects.Add(childProject);
            }
        }

        private static bool ParseProjectEndLine(string line) => line.StartsWith("EndProject");

        private bool ParseProjectSolutionItemsSectionStart(string line) => line.StartsWith("ProjectSection(SolutionItems)");
        private bool ParseProjectSectionEnd(string line) => line.StartsWith("EndProjectSection");

        private class NestedProjectMapping
        {
            private NestedProjectMapping(
                string childProjectId,
                string parentProjectId)
            {
                ChildProjectId = new Guid(childProjectId);
                ParentProjectId = new Guid(parentProjectId);
            }

            public Guid ChildProjectId { get; }

            public Guid ParentProjectId { get; }

            public static NestedProjectMapping? Parse(string line)
            {
                var match = Regex.Match(line,
                    @"{(?<childProjectId>[A-Za-z0-9\-]+)} = {(?<parentProjectId>[A-Za-z0-9\-]+)}");

                if (!match.Success)
                {
                    return null;
                }

                return new NestedProjectMapping(
                    childProjectId: match.Groups["childProjectId"].Value,
                    parentProjectId: match.Groups["parentProjectId"].Value);
            }
        }
    }
}
