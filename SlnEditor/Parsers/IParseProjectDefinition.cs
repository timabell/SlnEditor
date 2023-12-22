using SlnEditor.Models;

namespace SlnEditor.Parsers
{
    internal interface IParseProjectDefinition
    {
        bool TryParseProjectDefinition(
            Solution solution,
            string projectDefinition,
            out IProject? project);
    }
}
