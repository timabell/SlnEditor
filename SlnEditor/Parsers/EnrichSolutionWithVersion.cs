using SlnEditor.Models;
using System.Collections.Generic;
using System.Linq;

namespace SlnEditor.Parsers
{
    internal class EnrichSolutionWithVersion : IEnrichSolution
    {
        public void Enrich(Solution solution, IList<string> fileContents, bool bestEffort)
        {

            foreach (var line in fileContents)
            {
                ProcessSolutionFileFormatVersion(line, solution);
                ProcessVisualStudioVersion(line, solution);
                ProcessMinimumVisualStudioVersion(line, solution);
            }
        }

        private static void ProcessSolutionFileFormatVersion(string line, Solution solution)
        {
            if (!line.StartsWith("Microsoft Visual Studio Solution File, "))
            {
                return;
            }

            /*
             * 54 characters, because...
             * "Microsoft Visual Studio Solution File, Format Version " is 54 characters long
            */
            var fileFormatVersion = string.Concat(line.Skip(54));
            solution.FileFormatVersion = fileFormatVersion;
        }

        private static void ProcessVisualStudioVersion(string line, Solution solution)
        {
            if (!line.StartsWith("VisualStudioVersion = "))
            {
                return;
            }

            // because "VisualStudioVersion = " is 22 characters long
            var visualStudioVersion = string.Concat(line.Skip(22));

            solution.VisualStudioVersion.Version = visualStudioVersion;
        }

        private static void ProcessMinimumVisualStudioVersion(string line, Solution solution)
        {
            if (!line.StartsWith("MinimumVisualStudioVersion = "))
            {
                return;
            }

            // because "MinimumVisualStudioVersion = " is 29 characters long
            var minimumVisualStudioVersion = string.Concat(line.Skip(29));

            solution.VisualStudioVersion.MinimumVersion = minimumVisualStudioVersion;
        }
    }
}
