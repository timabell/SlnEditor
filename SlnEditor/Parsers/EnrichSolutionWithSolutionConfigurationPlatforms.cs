using SlnEditor.Models;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionConfigurationPlatforms : IEnrichSolution
    {
        private readonly SolutionConfigurationPlatformParser _solutionConfigurationPlatformParser = new SolutionConfigurationPlatformParser();

        public void Enrich(Solution solution, IList<string> fileContents)
        {
            var projectConfigurations = _solutionConfigurationPlatformParser.Parse(
                fileContents,
                "SolutionConfiguration");
            solution.ConfigurationPlatforms = projectConfigurations
                .Select(projectConfiguration => projectConfiguration.ConfigurationPlatform)
                .ToList()
                ;
        }
    }
}
