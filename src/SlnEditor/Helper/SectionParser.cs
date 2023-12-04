using SlnEditor.Contracts.Helper;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Helper
{
    internal class SectionParser : ISectionParser
    {
        public IList<string> GetFileContentsInGlobalSection(
            IList<string> fileContents,
            string sectionName)
        {
            var startSection = $"GlobalSection({sectionName}";
            const string endSection = "EndGlobalSection";

            return GetFileContentsInSection(fileContents, startSection, endSection);
        }

        private static IList<string> GetFileContentsInSection(
            IList<string> fileContents,
            string startSection,
            string endSection)
        {
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
