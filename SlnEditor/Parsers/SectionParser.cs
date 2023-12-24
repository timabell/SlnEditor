using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class SectionParser
    {
        public static IList<string> GetFileContentsInGlobalSection(
            IList<string> fileContents,
            string sectionName, out int sourceLine)
        {
            var startSection = $"GlobalSection({sectionName}";
            const string endSection = "EndGlobalSection";

            var fileContentsInGlobalSection = GetFileContentsInSection(fileContents, startSection, endSection, out var parsedSourceLine);
            sourceLine = parsedSourceLine;
            return fileContentsInGlobalSection;
        }

        private static IList<string> GetFileContentsInSection(
            IList<string> fileContents,
            string startSection,
            string endSection,
            out int sourceLine)
        {
            sourceLine = fileContents
                .TakeWhile(line => !line.StartsWith(startSection))
                .Count()+1;

            var section = fileContents
                .SkipWhile(line => !line.StartsWith(startSection))
                .TakeWhile(line => !line.StartsWith(endSection))
                .Where(line => !line.StartsWith(startSection))
                .Where(line => !line.StartsWith(endSection))
                .Where(line => !string.IsNullOrWhiteSpace(line));

            return section.ToList();
        }
    }
}
