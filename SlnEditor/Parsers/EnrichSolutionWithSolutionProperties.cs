using SlnEditor.Exceptions;
using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithSolutionProperties : IEnrichSolution
    {
        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {
            var section = SectionParser.ExtractGlobalSection(fileContents, "SolutionProperties");

            solution.GlobalSections.Add(new SolutionPropertiesSection
            {
                SourceLine = section.SourceStartLine,
                SolutionProperties = new SolutionProperties
                {
                    HideSolutionNode = section.Lines
                        .Select(ExtractHideSolutionNode)
                        .FirstOrDefault(x => x.HasValue),
                },
            });
        }

        private static bool? ExtractHideSolutionNode(string line)
        {
            const string pattern = @"\s*HideSolutionNode\s*=\s*([A-Za-z0-9\-]+)";
            var match = Regex.Match(line, pattern);
            if (!match.Success)
            {
                return null;
            }

            var boolString = match.Groups[1].Value;
            var success = bool.TryParse(boolString, out var parsedBool);
            if (!success)
            {
                throw new UnexpectedSolutionStructureException(
                    "Unexpected HideSolutionNode value, expected true/false");
            }

            return parsedBool;
            return bool.Parse(boolString);
        }
    }
}
