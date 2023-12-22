using System.Collections.Generic;

namespace SlnEditor.Parsers
{
    internal interface IParseSolutionConfigurationPlatform
    {
        IList<ProjectConfigurationPlatform> Parse(
            IList<string> fileContents,
            string sectionName);
    }
}
