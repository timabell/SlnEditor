using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionGuid : IEnrichSolution
    {
        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {
            var sectionContents = SectionParser.ExtractGlobalSection(
                fileContents,
                "ExtensibilityGlobals");

            var section = new ExtensibilityGlobalsSection
            {
                SourceLine = sectionContents.SourceStartLine,
                SolutionGuid = sectionContents.Lines
                    .Select(ExtractSolutionGuid)
                    .FirstOrDefault(x => x.HasValue),
            };

            foreach (var line in sectionContents.Lines)
            {
                if (ExtractSolutionGuid(line) == null)
                {
                    section.AdditionalEntries.Add(line.Trim());
                }
            }

            solution.GlobalSections.Add(section);
        }

        private static Guid? ExtractSolutionGuid(string line)
        {
            const string pattern = @"\s*SolutionGuid\s*=\s*{([A-Fa-f0-9\-]+)}";
            var match = Regex.Match(line, pattern);
            if (!match.Success)
            {
                return null;
            }

            var guidString = match.Groups[1].Value;
            return new Guid(guidString);
        }
    }
}
