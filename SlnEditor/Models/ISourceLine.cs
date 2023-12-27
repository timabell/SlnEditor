namespace SlnEditor.Models
{
    /// <summary>
    /// Consistent way of anything in the models noting information about the relevant source that was parsed.
    /// </summary>
    public interface ISourceLine
    {
        /// <summary>
        /// Line number this section was found at when parsing.
        /// Used internally to provide stable ordering.
        /// </summary>
        public int? SourceLine { get; }
    }
}
