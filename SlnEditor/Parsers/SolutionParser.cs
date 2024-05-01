using SlnEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class SolutionParser
    {
        private readonly bool _bestEffort;

        private readonly IList<IEnrichSolution> _solutionEnrichers = new List<IEnrichSolution>
        {
            new EnrichSolutionWithVersion(),
            new EnrichSolutionWithProjects(),
            new EnrichSolutionWithSolutionConfigurationPlatforms(),
            new EnrichSolutionWithProjectConfigurationPlatforms(), // It's important that this happens _after_ the 'EnrichSolutionWithProjects', because we need the parsed projects before we can map the configurations to them
            new EnrichSolutionWithSolutionProperties(),
            new EnrichSolutionWithSolutionGuid(),
        };

        /// <summary>
        /// Creates a new instance of <see cref="SolutionParser" />
        /// </summary>
        /// <param name="bestEffort"></param>
        public SolutionParser(bool bestEffort)
        {
            _bestEffort = bestEffort;
        }

        /// <summary>
        /// Read a file and apply everything found to the provided solution parameter.
        /// </summary>
        /// <param name="content">Text contents of a sln file</param>
        /// <param name="solution">Instance of Solution class to mutate</param>
        public void ParseInto(string content, Solution solution)
        {
            var separators = new[] { "\r\n", "\r", "\n" };
            var lines = content.Split(separators, StringSplitOptions.None); // https://stackoverflow.com/questions/1547476/split-a-string-on-newlines-in-net/1547483#1547483
            var allLinesTrimmed = lines
                .Select(line => line.Trim())
                .ToList();

            solution.GlobalSections.Clear();
            foreach (var enricher in _solutionEnrichers)
            {
                enricher.Enrich(solution, allLinesTrimmed, _bestEffort);
            }

            // todo: move sorting to render, add more tests
            solution.GlobalSections = solution.GlobalSections.OrderBy(s => s.SourceLine).ToList();
        }
    }
}
