using SlnEditor.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SlnEditor.Contracts
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

        /// <summary>
        /// Parse an existing sln file's contents
        /// </summary>
        /// <param name="contents">The raw text of a solution file</param>
        public Solution(string contents)
        {
            new SolutionParser().ParseText(contents, this);
        }

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

        private static IList<IProject> Flatten(IEnumerable<IProject> projects)
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
        public IList<IProject> Projects { get; internal set; } = new List<IProject>();

        /// <inheritdoc />
        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; internal set; } = new List<ConfigurationPlatform>();

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
