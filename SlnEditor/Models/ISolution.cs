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
        ///     A flat list of all <see cref="IProject" />s contained in the solution
        /// </summary>
        IList<IProject> AllProjects { get; }

        /// <summary>
        ///     A structured list of all <see cref="IProject" />s contained in the solution
        /// </summary>
        IList<IProject> Projects { get; }

        /// <summary>
        ///     The <see cref="ConfigurationPlatform" />s configured for this solution
        /// </summary>
        IList<ConfigurationPlatform> ConfigurationPlatforms { get; }

        /// <summary>
        /// The <see cref="Guid"/> of the solution.
        /// </summary>
        Guid? Guid { get; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        string Write();
    }
}
