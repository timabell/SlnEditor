using SlnEditor.Exceptions;
using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class SolutionConfigurationPlatformParser
    {
        public ConfigurationPlatformsSection Parse(IList<string> fileContents, string sectionName)
        {
            if (fileContents == null) throw new ArgumentNullException(nameof(fileContents));
            if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(sectionName));

            var section = SectionParser.ExtractGlobalSection(fileContents, sectionName);

            var sourceLine = section.SourceStartLine + 1;
            return new ConfigurationPlatformsSection
            {
                SourceLine = section.SourceStartLine,
                ConfigurationPlatforms = section.Lines.Select(l => ParseLine(l, sourceLine)).ToList(),
            };
        }

        private static ConfigurationPlatform ParseLine(string line, int sourceLine)
        {
            var match = Regex.Match(line, @"(?<name>.+) = (?<buildConfiguration>.+?)(?:\|(?<buildPlatform>.+))?$");

            if (!match.Success)
            {
                throw new UnexpectedSolutionStructureException(
                    "Expected to find ConfigurationPlatform but pattern did not match");
            }

            return new ConfigurationPlatform(
                match.Groups["name"].Value,
                match.Groups["buildConfiguration"].Value,
                match.Groups["buildPlatform"].Value,
                sourceLine);
        }
    }
}
