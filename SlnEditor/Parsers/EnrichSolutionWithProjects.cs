using SlnEditor.Exceptions;
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
        private readonly ProjectDefinitionParser _projectDefinitionParser = new ProjectDefinitionParser();

        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {
            if (solution == null) throw new ArgumentNullException(nameof(solution));
            if (fileContents == null) throw new ArgumentNullException(nameof(fileContents));

            solution.Projects = GetProjectsFlat(solution, fileContents);
            ParseProjectHierarchy(fileContents, solution);
        }

        private IList<IProject> GetProjectsFlat(Solution solution, IEnumerable<string> fileContents)
        {
            var flatProjects = new List<IProject>();
            foreach (var line in fileContents)
            {
                if (!_projectDefinitionParser.TryParseProjectDefinition(solution, line, out var project) ||
                    project == null) continue;

                flatProjects.Add(project);
            }

            return flatProjects;
        }

        private static void ParseProjectHierarchy(IList<string> fileContents,
            Solution solution)
        {

            var sectionContents = SectionParser.GetFileContentsInGlobalSection(
                fileContents,
                "NestedProjects", out var sourceLine);

            solution.GlobalSections.Add(new NestedProjectsSection
            {
                SourceLine = sourceLine,
            });

            var nestedProjectMappings = GetNestedProjectMappings(sectionContents);

            ApplyProjectNesting(solution.Projects, nestedProjectMappings);
        }

        private static IList<NestedProjectMapping> GetNestedProjectMappings(IEnumerable<string> sectionContents)
        {
            var nestedProjectMappings = new List<NestedProjectMapping>();
            foreach (var nestedProject in sectionContents)
            {
                if (TryGetNestedProjectMapping(nestedProject, out var nestedProjectMapping) &&
                    nestedProjectMapping != null)
                {
                    nestedProjectMappings.Add(nestedProjectMapping);
                }
            }

            return nestedProjectMappings;
        }

        private static bool TryGetNestedProjectMapping(string nestedProject,
            out NestedProjectMapping? nestedProjectMapping)
        {
            // https://regexr.com/653pi
            const string pattern = @"{(?<targetProjectId>[A-Za-z0-9\-]+)} = {(?<destinationProjectId>[A-Za-z0-9\-]+)}";

            nestedProjectMapping = null;
            var match = Regex.Match(nestedProject, pattern);
            if (!match.Success) return false;

            var targetProjectId = match.Groups["targetProjectId"].Value;
            var destinationProject = match.Groups["destinationProjectId"].Value;

            nestedProjectMapping = new NestedProjectMapping(targetProjectId, destinationProject);
            return true;
        }

        private static void ApplyProjectNesting(IList<IProject> flatProjects, IList<NestedProjectMapping> nestedProjectMappings)
        {
            var flatProjectList = flatProjects.ToList();
            foreach (var project in flatProjectList)
            {
                ApplyNestingForProject(project, flatProjectList, nestedProjectMappings);
            }
        }

        private static void ApplyNestingForProject(IProject project, IEnumerable<IProject> flatProjects, IEnumerable<NestedProjectMapping> nestedProjectMappings)
        {
            var mappingCandidate = nestedProjectMappings.FirstOrDefault(mapping => mapping.TargetId == project.Id);
            if (mappingCandidate == null)
            {
                return;
            }

            var destinationCandidate = flatProjects.FirstOrDefault(proj => proj.Id == mappingCandidate.DestinationId);
            if (destinationCandidate == null)
                throw new UnexpectedSolutionStructureException(
                    $"Expected to find a project with id '{mappingCandidate.DestinationId}', but found none");

            if (!(destinationCandidate is SolutionFolder solutionFolder))
                throw new UnexpectedSolutionStructureException(
                    $"Expected project with id '{destinationCandidate.Id}' to be a Solution-Folder but found '{destinationCandidate.GetType()}'");

            solutionFolder.Projects.Add(project);
        }
    }
}
