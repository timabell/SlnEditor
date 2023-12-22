using System.Collections.Generic;

namespace SlnEditor.Parsers
{
    internal interface ISectionParser
    {
        IList<string> GetFileContentsInGlobalSection(
            IList<string> fileContents,
            string sectionName);
    }
}
