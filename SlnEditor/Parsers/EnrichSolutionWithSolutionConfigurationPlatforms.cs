using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
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
                "SolutionConfiguration", out var sourceLine);

            solution.GlobalSections.Add(new ConfigurationPlatformsSection
            {
                SourceLine = sourceLine,
                ConfigurationPlatforms = projectConfigurations
                    .Select(projectConfiguration => projectConfiguration.ConfigurationPlatform)
                    .ToList(),
            });
        }
    }
}
