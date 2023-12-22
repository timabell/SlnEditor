using SlnEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionFolderFiles : IEnrichSolution
    {
        private readonly ProjectDefinitionParser _projectDefinitionParser = new ProjectDefinitionParser();
        private bool _inASolutionItemsSection;

        private SolutionFolder? _solutionFolderForCurrentSection;

        /*
         * line block:
         * Project("...
         * ProjectSection(SolutionItems) = preProject
         * file1/file1 \
         * file2/file2  }-- we want these
         * file3/file3 /
         * EndProjectSection
         */
        public void Enrich(Solution solution, IList<string> fileContents)
        {
            if (solution == null) throw new ArgumentNullException(nameof(solution));
            if (fileContents == null) throw new ArgumentNullException(nameof(fileContents));

            foreach (var line in fileContents)
                ProcessLine(solution, line);
        }

        private void ProcessLine(Solution solution, string line)
        {
            if (_solutionFolderForCurrentSection == null)
            {
                // if the project-definition could be parsed we can assume we are in a "Project" --> "EndProject" block
                TryGetSolutionFolder(solution, line, out _solutionFolderForCurrentSection);
                return;
            }

            DetermineEndProject(line);
            AddSolutionItemFile(solution, line);
            DetermineProjectItemsSection(line);
        }

        private void TryGetSolutionFolder(
            Solution solution,
            string line,
            out SolutionFolder? solutionFolder)
        {
            solutionFolder = null;
            if (!_projectDefinitionParser.TryParseProjectDefinition(solution, line, out var project))
                return;

            if (!(project is SolutionFolder slnFolder))
                return;

            var actualSolutionFolder = solution
                .AllProjects
                .OfType<SolutionFolder>()
                .FirstOrDefault(folder => folder.Id == slnFolder.Id);
            if (actualSolutionFolder == null) return;

            _inASolutionItemsSection = false;
            solutionFolder = actualSolutionFolder;
        }

        private void DetermineEndProject(string line)
        {
            if (!line.StartsWith("EndProject")) return;

            _solutionFolderForCurrentSection = null;
            _inASolutionItemsSection = false;
        }

        private void DetermineProjectItemsSection(string line)
        {
            if (_inASolutionItemsSection) return;

            _inASolutionItemsSection = line.StartsWith("ProjectSection(SolutionItems)");
        }

        private void AddSolutionItemFile(ISolution solution, string line)
        {
            if (!_inASolutionItemsSection) return;

            var solutionItemFile = GetSolutionItemFile(line);

            if (solutionItemFile is null)
            {
                return;
            }

            _solutionFolderForCurrentSection?.Files.Add(solutionItemFile);
        }

        private static string? GetSolutionItemFile(string line)
            => line.Split('=').FirstOrDefault()?.Trim();
    }
}
