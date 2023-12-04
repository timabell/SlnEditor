using System.Collections.Generic;

namespace SlnParser.Contracts.Helper
{
    internal interface ISectionParser
    {
        IList<string> GetFileContentsInGlobalSection(
            IList<string> fileContents,
            string sectionName);
    }
}
