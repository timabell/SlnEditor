using System.Collections.Generic;

namespace SlnEditor.Contracts.Helper
{
    internal interface IEnrichSolution
    {
        void Enrich(Solution solution, IList<string> fileContents);
    }
}
