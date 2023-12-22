using SlnEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionGuid : IEnrichSolution
    {
        private readonly SectionParser _sectionParser = new SectionParser();

        public void Enrich(Solution solution, IList<string> fileContents)
        {
            var sectionContents = _sectionParser.GetFileContentsInGlobalSection(
                fileContents,
                "ExtensibilityGlobals");

            solution.Guid = sectionContents
                .Select(ExtractSolutionGuid)
                .FirstOrDefault(x => x.HasValue);
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
