using System;
using System.Diagnostics;

namespace SlnEditor.Models
{
    /// <summary>
    ///     A Configuration of a Solution or Project describing which configuration and build-platform is targeted
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ConfigurationPlatform : ISourceLine
    {
        public int? SourceLine { get; internal set; }
        int? ISourceLine.SourceLine => SourceLine;

        /// <summary>
        ///     Create a new instance of <see cref="ConfigurationPlatform" />
        /// </summary>
        /// <param name="name">The name of the <see cref="ConfigurationPlatform" /></param>
        /// <param name="configuration">The configuration of the <see cref="ConfigurationPlatform" /></param>
        /// <param name="platform">The build-platform of the <see cref="ConfigurationPlatform" /></param>
        public ConfigurationPlatform(
            string name,
            string configuration,
            string platform)
        {
            Name = name;
            Configuration = configuration;
            Platform = platform;
        }

        internal ConfigurationPlatform(string configurationName, string buildConfiguration, string buildPlatform, int sourceLine)
        {
            Name = configurationName;
            Configuration = buildConfiguration;
            Platform = buildPlatform;
            SourceLine = sourceLine;
        }

        /// <summary>
        ///     The name of <see cref="ConfigurationPlatform" />
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The configuration the <see cref="ConfigurationPlatform" /> is targeting
        /// </summary>
        public string Configuration { get; }

        /// <summary>
        ///     The build-platform the <see cref="ConfigurationPlatform" /> is targeting
        /// </summary>
        public string Platform { get; }

        public string Render() => $"\t\t{Name} = {Name}";
        public string Render(Guid projectId) => $"\t\t{{{projectId.ToString().ToUpper()}}}.{Name} = {Configuration}|{Platform}";


        private string DebuggerDisplay => $"{Name}";
    }
}
