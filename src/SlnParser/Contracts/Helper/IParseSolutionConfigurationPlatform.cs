using System.Collections.Generic;

namespace SlnParser.Contracts.Helper
{
    internal interface IParseSolutionConfigurationPlatform
    {
        IList<ProjectConfigurationPlatform> Parse(
            IList<string> fileContents,
            string sectionName);
    }
}
