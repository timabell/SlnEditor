using SlnEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class SolutionParser
    {
        private readonly IList<IEnrichSolution> _solutionEnrichers = new List<IEnrichSolution>
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

        /// <summary>
        /// Creates a new instance of <see cref="SolutionParser" />
        /// </summary>
        public SolutionParser()
        {
        }

        /// <summary>
        /// Read a file and apply everything found to the provided solution parameter.
        /// </summary>
        /// <param name="content">Text contents of a sln file</param>
        /// <param name="solution">Instance of Solution class to mutate</param>
        public void ParseInto(string content, Solution solution)
        {
            var separators = new[] { "\r\n", "\r", "\n" };
            var lines = content.Split( separators, StringSplitOptions.None ); // https://stackoverflow.com/questions/1547476/split-a-string-on-newlines-in-net/1547483#1547483
            var allLinesTrimmed = lines
                .Select(line => line.Trim())
                .Where(line => line.Length > 0)
                .ToList();

            foreach (var enricher in _solutionEnrichers)
            {
                enricher.Enrich(solution, allLinesTrimmed);
            }

            foreach (var line in lines)
            {
                ProcessLine(line, solution);
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
            if (!line.StartsWith("Microsoft Visual Studio Solution File, "))
            {
                return;
            }

            /*
             * 54 characters, because...
             * "Microsoft Visual Studio Solution File, Format Version " is 54 characters long
            */
            var fileFormatVersion = string.Concat(line.Skip(54));
            solution.FileFormatVersion = fileFormatVersion;
        }

        private static void ProcessVisualStudioVersion(string line, Solution solution)
        {
            if (!line.StartsWith("VisualStudioVersion = "))
            {
                return;
            }

            // because "VisualStudioVersion = " is 22 characters long
            var visualStudioVersion = string.Concat(line.Skip(22));

            solution.VisualStudioVersion.Version = visualStudioVersion;
        }

        private static void ProcessMinimumVisualStudioVersion(string line, ISolution solution)
        {
            if (!line.StartsWith("MinimumVisualStudioVersion = "))
            {
                return;
            }

            // because "MinimumVisualStudioVersion = " is 29 characters long
            var minimumVisualStudioVersion = string.Concat(line.Skip(29));

            solution.VisualStudioVersion.MinimumVersion = minimumVisualStudioVersion;
        }
    }
}
