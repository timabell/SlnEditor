using SlnEditor.Parsers;
using SlnEditor.Writers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Models
{
    /// <summary>
    /// All the information contained in a Visual Studio Solution File (sln)
    /// </summary>
    public class Solution
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

        public string FileFormatVersion { get; set; } = string.Empty;

        public VisualStudioVersion VisualStudioVersion { get; set; } = new VisualStudioVersion();

        /// <summary>
        /// All projects in the solution regardless of whether they are nested,
        /// stored in the order they are found in the file.
        /// </summary>
        public IList<IProject> Projects { get; internal set; } = new List<IProject>();

        /// <summary>
        /// Projects that are not the child of any other project, i.e. the top level.
        /// Calculated on the fly.
        /// </summary>
        public IReadOnlyList<IProject> RootProjects =>
            Projects.Where(child =>
                    Projects.OfType<SolutionFolder>().All(
                        parent => parent.Projects.All(x => x != child))) // Find all the projects with no parent solution folder
                .ToList();

        /// <inheritdoc />
        public IList<ConfigurationPlatform> ConfigurationPlatforms { get; internal set; } =
            new List<ConfigurationPlatform>();

        public SolutionProperties SolutionProperties { get; } = new SolutionProperties();

        /// <inheritdoc/>
        public Guid? Guid { get; set; }

        /// <summary>
        /// Convert in memory solution to sln file format for writing to or overwriting a .sln file
        /// </summary>
        public override string ToString()
        {
            return SolutionWriter.Write(this);
        }
    }
}
