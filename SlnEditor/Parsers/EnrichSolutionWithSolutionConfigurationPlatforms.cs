using SlnEditor.Models;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionConfigurationPlatforms : IEnrichSolution
    {
        private readonly IParseSolutionConfigurationPlatform _parseSolutionConfigurationPlatform;

        public EnrichSolutionWithSolutionConfigurationPlatforms()
        {
            _parseSolutionConfigurationPlatform = new SolutionConfigurationPlatformParser();
        }

        public void Enrich(Solution solution, IList<string> fileContents)
        {
            var projectConfigurations = _parseSolutionConfigurationPlatform.Parse(
                fileContents,
                "SolutionConfiguration");
            solution.ConfigurationPlatforms = projectConfigurations
                .Select(projectConfiguration => projectConfiguration.ConfigurationPlatform)
                .ToList()
                ;
        }
    }
}
