﻿using System;
using System.Runtime.Serialization;

namespace SlnEditor.Exceptions
{
    /// <summary>
    /// An <see cref="Exception" /> that describes an unexpected structure of a Solution
    /// </summary>
    [Serializable]
    public class UnexpectedSolutionStructureException : Exception
    {
        /// <summary>
        ///     Creates a new instance of <see cref="UnexpectedSolutionStructureException" />
        /// </summary>
        /// <param name="message">The message why the structure is unexpected</param>
        public UnexpectedSolutionStructureException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="UnexpectedSolutionStructureException" />
        /// </summary>
        /// <param name="message">The message why the structure is unexpected</param>
        /// <param name="inner">The inner <see cref="Exception" /></param>
        public UnexpectedSolutionStructureException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <inheritdoc />
        protected UnexpectedSolutionStructureException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
