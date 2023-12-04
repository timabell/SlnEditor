using System.Collections.Generic;

namespace SlnParser.Contracts.Helper
{
    internal interface IEnrichSolution
    {
        void Enrich(Solution solution, IList<string> fileContents);
    }
}
