using System;
using System.Collections.Generic;

namespace SlnEditor.Models
{
    /// <summary>
    ///     An interface representing all the information contained in a Visual Studio Solution File (sln)
    /// </summary>
    public interface ISolution
    {
        /// <summary>
        ///     The file format version of the solution
        /// </summary>
        string FileFormatVersion { get; set; }

        /// <summary>
        ///     The <see cref="VisualStudioVersion" /> of the solution
        /// </summary>
        VisualStudioVersion VisualStudioVersion { get; set; }

        /// <summary>
        /// All projects in the solution regardless of whether they are nested,
        /// stored in the order they are found in the file.
        /// </summary>
        IList<IProject> Projects { get; }

        /// <summary>
        /// Projects that are not the child of any other project, i.e. the top level.
        /// Calculated on the fly.
        /// </summary>
        IReadOnlyList<IProject> RootProjects { get; }

        /// <summary>
        ///     The <see cref="ConfigurationPlatform" />s configured for this solution
        /// </summary>
        IList<ConfigurationPlatform> ConfigurationPlatforms { get; }

        /// <summary>
        /// The <see cref="Guid"/> of the solution.
        /// </summary>
        Guid? Guid { get; }
    }
}
