using SlnEditor.Models;
using System.Collections.Generic;

namespace SlnEditor.Parsers
{
    internal interface IEnrichSolution
    {
        void Enrich(Solution solution, IList<string> fileContents, bool bestEffort);
    }
}
