using SlnEditor.Models;
using System.Collections.Generic;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionConfigurationPlatforms : IEnrichSolution
    {
        private readonly SolutionConfigurationPlatformParser _parser = new SolutionConfigurationPlatformParser();

        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {
            var section = _parser.Parse(
                fileContents,
                "SolutionConfiguration");

            solution.GlobalSections.Add(section);
        }
    }
}
