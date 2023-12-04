using System.Collections.Generic;

namespace SlnEditor.Contracts.Helper
{
    internal interface ISectionParser
    {
        IList<string> GetFileContentsInGlobalSection(
            IList<string> fileContents,
            string sectionName);
    }
}
