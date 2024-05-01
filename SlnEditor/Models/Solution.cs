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
        /// Projects that are not the child of any other project, i.e. the top level.
        /// A flattened list can be created with <see cref="Extensions.FlatProjectList"/>.
        /// The list of root projects and the solution folder hierarchy is the source of truth for what projects exist in the solution.
        /// This list of "root" projects and the hierarchy of folders and projects can be modified as you wish, and will be flattened
        /// and rendered out to sln format on demand (with <see cref="ToString"/>)
        /// </summary>
        public IList<IProject> RootProjects { get; set; } = new List<IProject>();

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
                throw new InvalidOperationException($"{sections.Count} {nameof(T)} in {GlobalSections}, sections must be unique");
            }
            if (sections.Count == 0)
            {
                throw new InvalidOperationException($"{nameof(T)} not present in {GlobalSections}");
            }
            return sections.Single();
        }

        private List<IGlobalSection> BuildDefaultSections()
        {
            return new List<IGlobalSection>
            {
                new ConfigurationPlatformsSection(),
                new ProjectConfigurationPlatformsSection(this),
                new NestedProjectsSection(this),
                new SolutionPropertiesSection(),
                new ExtensibilityGlobalsSection(),
            };
        }
    }
}
