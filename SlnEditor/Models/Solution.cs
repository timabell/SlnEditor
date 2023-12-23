using SlnEditor.Parsers;
using SlnEditor.Writers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Models
{
    /// <inheritdoc />
    public class Solution : ISolution
    {
        public Solution()
        {
        }

        /// <summary>
        /// Parse an existing sln file's contents
        /// </summary>
        /// <param name="contents">The raw text of a solution file</param>
        /// <param name="bestEffort">If set to true will, will not throw errors for any parsing failures</param>
        public Solution(string contents, bool bestEffort = false)
        {
            new SolutionParser(bestEffort).ParseInto(contents, this);
        }

        /// <inheritdoc />
        public string FileFormatVersion { get; set; } = string.Empty;

        /// <inheritdoc />
        public VisualStudioVersion VisualStudioVersion { get; set; } = new VisualStudioVersion();

        /// <inheritdoc />
        public IList<IProject> Projects { get; internal set; } = new List<IProject>();

        // Find all the projects with no parent solution folder
        /// <inheritdoc />
        public IReadOnlyList<IProject> RootProjects =>
            Projects.Where(child =>
                    Projects.OfType<SolutionFolder>().All(
                        parent => parent.Projects.All(x => x != child)))
                .ToList();

        /// <inheritdoc />
        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; internal set; } =
            new List<ConfigurationPlatform>();

        /// <inheritdoc />
        public SolutionProperties SolutionProperties { get; internal set; } = new SolutionProperties();

        /// <inheritdoc/>
        public Guid? Guid { get; internal set; }

        /// <summary>
        /// Convert in memory solution to sln file format for writing to or overwriting a .sln file
        /// </summary>
        public override string ToString()
        {
            return SolutionWriter.Write(this);
        }
    }
}
