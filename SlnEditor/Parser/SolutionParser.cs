using SlnEditor.Contracts;
using SlnEditor.Contracts.Helper;
using SlnEditor.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parser
{
    internal class SolutionParser
    {
        private readonly IList<IEnrichSolution> _solutionEnrichers;

        /// <summary>
        ///     Creates a new instance of <see cref="SolutionParser" />
        /// </summary>
        public SolutionParser()
        {
            _solutionEnrichers = new List<IEnrichSolution>
            {
                new EnrichSolutionWithProjects(),
                new EnrichSolutionWithSolutionConfigurationPlatforms(),
                /*
                 * NOTE: It's important that this happens _after_ the 'EnrichSolutionWithProjects',
                 * because we need the parsed projects before we can map the configurations to them
                 */
                new EnrichSolutionWithProjectConfigurationPlatforms(),
                new EnrichSolutionWithSolutionFolderFiles(),
                new EnrichSolutionWithSolutionGuid(),
            };
        }

        public void ParseInto(string slnContents, Solution targetSolution)
        {
            var separators = new[] { "\r\n", "\r", "\n" };
            var lines = slnContents.Split(separators, StringSplitOptions.None); // https://stackoverflow.com/questions/1547476/split-a-string-on-newlines-in-net/1547483#1547483

            var allLinesTrimmed = lines
                .Select(line => line.Trim())
                .Where(line => line.Length > 0)
                .ToList();

            foreach (var enricher in _solutionEnrichers)
            {
                enricher.Enrich(targetSolution, allLinesTrimmed);
            }

            foreach (var line in lines)
            {
                ProcessLine(line, targetSolution);
            }
        }

        private static void ProcessLine(string line, Solution solution)
        {
            ProcessSolutionFileFormatVersion(line, solution);
            ProcessVisualStudioVersion(line, solution);
            ProcessMinimumVisualStudioVersion(line, solution);
        }

        private static void ProcessSolutionFileFormatVersion(string line, Solution solution)
        {
            if (!line.StartsWith("Microsoft Visual Studio Solution File, ")) return;

            /*
             * 54 characters, because...
             * "Microsoft Visual Studio Solution File, Format Version " is 54 characters long
             */
            var fileFormatVersion = string.Concat(line.Skip(54));
            solution.FileFormatVersion = fileFormatVersion;
        }

        private static void ProcessVisualStudioVersion(string line, Solution solution)
        {
            if (!line.StartsWith("VisualStudioVersion = ")) return;

            // because "VisualStudioVersion = " is 22 characters long
            var visualStudioVersion = string.Concat(line.Skip(22));

            solution.VisualStudioVersion.Version = visualStudioVersion;
        }

        private static void ProcessMinimumVisualStudioVersion(string line, ISolution solution)
        {
            if (!line.StartsWith("MinimumVisualStudioVersion = ")) return;

            // because "MinimumVisualStudioVersion = " is 29 characters long
            var minimumVisualStudioVersion = string.Concat(line.Skip(29));

            solution.VisualStudioVersion.MinimumVersion = minimumVisualStudioVersion;
        }
    }
}
