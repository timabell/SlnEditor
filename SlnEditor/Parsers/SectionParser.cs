using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class Excerpt
    {
        public int SourceStartLine { get; }
        public IList<string> Lines { get; }

        public Excerpt(IList<string> lines, int sourceStartLine)
        {
            SourceStartLine = sourceStartLine;
            Lines = lines;
        }
    }

    internal static class SectionParser
    {
        public static Excerpt ExtractGlobalSection(IList<string> fileContents, string sectionName)
        {
            var startSection = $"GlobalSection({sectionName}";
            const string endSection = "EndGlobalSection";

            var parsedSourceLine = fileContents
                .TakeWhile(line => !line.StartsWith(startSection))
                .Count() + 1;

            var section = fileContents
                .SkipWhile(line => !line.StartsWith(startSection))
                .TakeWhile(line => !line.StartsWith(endSection))
                .Where(line => !line.StartsWith(startSection))
                .Where(line => !line.StartsWith(endSection))
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            return new Excerpt(section, parsedSourceLine);
        }
    }
}
