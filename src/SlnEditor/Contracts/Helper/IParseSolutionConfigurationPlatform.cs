﻿using System.Collections.Generic;

namespace SlnEditor.Contracts.Helper
{
    internal interface IParseSolutionConfigurationPlatform
    {
        IList<ProjectConfigurationPlatform> Parse(
            IList<string> fileContents,
            string sectionName);
    }
}