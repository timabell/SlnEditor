using SlnEditor.Mappings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SlnEditor.Models
{
    /// <summary>
    /// A Solution Folder that can be contained in a <see cref="Solution" />.
    /// Can contain files, <see cref="Project"/>s and other SolutionFolders.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SolutionFolder : IProject
    {
        internal SolutionFolder(Guid id, string name, int lineNumber)
        {
            Id = id;
            Name = name;
            Path = name;
            SourceLine = lineNumber;
        }

        public SolutionFolder(Guid id, string name)
        {
            Id = id;
            Name = name;
            Path = name;
        }

        public SolutionFolder(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Path = name;
        }

        /// <summary>
        /// The contained <see cref="IProject" />s in the Solution Folder
        /// </summary>
        public IList<IProject> Projects { get; set; } = new List<IProject>();

        /// <summary>
        /// The contained files in the Solution Folder.
        /// The string value is the relative path to the file.
        /// The path separator *must* be windows format backslashes ('\')
        /// regardless of platform.
        /// </summary>
        public IList<string> Files { get; set; } = new List<string>();

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Path { get; }

        /// <inheritdoc />
        public Guid TypeGuid => new ProjectTypeMap().Guids[Type];

        /// <inheritdoc />
        public ProjectType Type => ProjectType.SolutionFolder;

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.Header());

            if (Files.Any())
            {
                sb.AppendLine("\tProjectSection(SolutionItems) = preProject");
                foreach (var file in Files)
                {
                    sb.AppendLine($"\t\t{file} = {file}");
                }

                sb.AppendLine("\tEndProjectSection");
            }

            sb.AppendLine("EndProject");
            return sb.ToString();
        }

        private string DebuggerDisplay => $"\"{Name}\" Id: \"{Id}\"";

        public string RenderNestedProjects()
        {
            var sb = new StringBuilder();
            foreach (var subProject in Projects)
            {
                sb.AppendLine(
                    $"\t\t{{{subProject.Id.ToString().ToUpper()}}} = {{{Id.ToString().ToUpper()}}}");
            }

            return sb.ToString();
        }

        public int? SourceLine { get; }
    }
}
