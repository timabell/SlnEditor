using SlnEditor.Models;
using SlnEditor.Models.GlobalSections;
using System.Linq;
using System.Text;

namespace SlnEditor.Writers
{
    internal static class SolutionWriter
    {
        public static string Write(Solution solution)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine($"Microsoft Visual Studio Solution File, Format Version {solution.FileFormatVersion}");
            sb.Append(solution.VisualStudioVersion.Render());
            foreach (var project in solution.Projects)
            {
                sb.Append(project.Render());
            }

            sb.AppendLine("Global");
            foreach (var section in solution.GlobalSections)
            {
                sb.Append(section.Render());
            }
            sb.AppendLine("EndGlobal");
            return sb.ToString();
        }
    }
}
