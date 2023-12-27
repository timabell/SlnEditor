using System;

namespace SlnEditor.Models
{
    /// <summary>
    /// A project that can be contained in a <see cref="Solution" />.
    /// Can be either a <see cref="SolutionFolder"/> or a <see cref="Project"/>.
    /// </summary>
    public interface IProject : ISourceLine
    {
        /// <summary>
        ///     The Id of the Project
        /// </summary>
        Guid Id { get; }

        /// <summary>
        ///     The Name of the Project
        /// </summary>
        string Name { get; }

        /// <summary>
        ///
        /// </summary>
        string Path { get; }

        /// <summary>
        ///     The Id of the Project-Type
        /// </summary>
        Guid TypeGuid { get; }

        /// <summary>
        ///     The well-known <see cref="Type" />
        /// </summary>
        ProjectType Type { get; }

        string Render();
    }
}
