using SlnEditor.Exceptions;
using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class ProjectConfigurationPlatformParser
    {
        public ProjectConfigurationPlatformsSection Parse(IList<string> fileContents, string sectionName, Solution solution)
        {
            if (fileContents == null) throw new ArgumentNullException(nameof(fileContents));
            if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(sectionName));

            var section = SectionParser.ExtractGlobalSection(fileContents, sectionName);
            var sourceLine = section.SourceStartLine + 1;
            var projectConfigurationPlatforms = section.Lines.Select(line => ParseConfigurationPlatform(line, sourceLine++)).ToList();

            return new ProjectConfigurationPlatformsSection(solution)
            {
                SourceLine = section.SourceStartLine,
                ProjectConfigurationPlatforms = projectConfigurationPlatforms,
            };
        }

        private static ProjectConfigurationPlatform ParseConfigurationPlatform(string line, int sourceLine)
        {
            var match = Regex.Match(line,
                @"((?<projectId>\{[A-Za-z0-9\-]+\}).)?(?<name>.+) = (?<buildConfiguration>.+?)(?:\|(?<buildPlatform>.+))?$");
            if (!match.Success)
            {
                throw new UnexpectedSolutionStructureException(
                    "Expected to find ConfigurationPlatform but pattern did not match");
            }

            var configurationProjectId = Guid.Parse(match.Groups["projectId"].Value);

            var configurationName = match.Groups["name"].Value;
            var buildConfiguration = match.Groups["buildConfiguration"].Value;
            var buildPlatform = match.Groups["buildPlatform"].Value;

            var configurationPlatform = new ConfigurationPlatform(
                configurationName,
                buildConfiguration,
                buildPlatform,
                sourceLine);

            return new ProjectConfigurationPlatform(projectId: configurationProjectId, configurationPlatform);
        }
    }
}
