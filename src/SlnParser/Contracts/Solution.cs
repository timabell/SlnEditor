using SlnParser.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SlnParser.Contracts
{
    /// <inheritdoc />
    public class Solution : ISolution
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Solution" />
        /// </summary>
        public Solution()
        {
            Projects = new List<IProject>();
            ConfigurationPlatforms = new List<ConfigurationPlatform>();
        }

        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public FileInfo? File { get; set; }

        /// <inheritdoc />
        public string FileFormatVersion { get; set; } = string.Empty;

        /// <inheritdoc />
        public VisualStudioVersion VisualStudioVersion { get; set; } = new VisualStudioVersion();

        /// <inheritdoc />
        public IList<IProject> AllProjects {
            get
            {
                return Flatten(Projects);
            }
        }

        private IList<IProject> Flatten(IList<IProject> projects)
        {
            var flattened = new List<IProject>();
            foreach (var project in projects)
            {
                flattened.Add(project);
                var folder = project as SolutionFolder;
                if (folder?.Projects.Any() is true)
                {
                    flattened.AddRange(Flatten(folder.Projects));
                }
            }

            return flattened;
        }

        /// <inheritdoc />
        public IList<IProject> Projects { get; internal set; }

        /// <inheritdoc />
        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; internal set; }

        /// <inheritdoc/>
        public Guid? Guid { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string Write()
        {
            return SolutionWriter.Write(this);
        }
    }
}
