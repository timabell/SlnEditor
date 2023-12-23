using SlnEditor.Models;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionConfigurationPlatforms : IEnrichSolution
    {
        private readonly ConfigurationPlatformParser _configurationPlatformParser = new ConfigurationPlatformParser();

        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {
            var projectConfigurations = _configurationPlatformParser.Parse(
                fileContents,
                "SolutionConfiguration");
            solution.ConfigurationPlatforms = projectConfigurations
                .Select(projectConfiguration => projectConfiguration.ConfigurationPlatform)
                .ToList()
                ;
        }
    }
}
