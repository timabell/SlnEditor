using SlnEditor.Models.GlobalSections;
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
        /// <summary>
        /// Build a blank solution with sensible default values
        /// </summary>
        public Solution()
        {
            GlobalSections = BuildDefaultSections();
            FileFormatVersion = "12.00";
            VisualStudioVersion = new VisualStudioVersion
            {
                MinimumVersion = "10.0.40219.1",
                Version = "17.0.31410.414",
            };
        }

        /// <summary>
        /// Parse an existing sln file's contents
        /// </summary>
        /// <param name="contents">The raw text of a solution file</param>
        /// <param name="bestEffort">If set to true will not throw exceptions for any parsing failures. Unfinished feature, contributions welcome.</param>
        public Solution(string contents, bool bestEffort = false)
        {
            new SolutionParser(bestEffort).ParseInto(contents, this);
        }

        public IList<IGlobalSection> GlobalSections { get; internal set; } = new List<IGlobalSection>();


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

        public ConfigurationPlatformsSection ConfigurationPlatformsSection => GlobalSection<ConfigurationPlatformsSection>();
        public IList<ConfigurationPlatform> ConfigurationPlatforms => ConfigurationPlatformsSection.ConfigurationPlatforms;

        public SolutionPropertiesSection SolutionPropertiesSection => GlobalSection<SolutionPropertiesSection>();
        public SolutionProperties SolutionProperties => SolutionPropertiesSection.SolutionProperties;

        public Guid? Guid => GlobalSection<ExtensibilityGlobalsSection>().SolutionGuid;

        /// <summary>
        /// Convert in memory solution to sln file format for writing to or overwriting a .sln file
        /// </summary>
        public override string ToString()
        {
            return SolutionWriter.Write(this);
        }

        public T GlobalSection<T>() where T : class, IGlobalSection
        {
            var sections = GlobalSections.OfType<T>().ToList();
            if (sections.Count > 1)
            {
                throw new InvalidOperationException( $"{sections.Count} {nameof(T)} in {GlobalSections}, sections must be unique");
            }
            if (sections.Count == 0)
            {
                throw new InvalidOperationException( $"{nameof(T)} not present in {GlobalSections}");
            }
            return sections.Single();
        }

        private List<IGlobalSection> BuildDefaultSections()
        {
            return new List<IGlobalSection>
            {
                new ConfigurationPlatformsSection(),
                new ProjectConfigurationPlatformsSection(Projects),
                new NestedProjectsSection(Projects),
                new SolutionPropertiesSection(),
                new ExtensibilityGlobalsSection(),
            };
        }
    }
}
