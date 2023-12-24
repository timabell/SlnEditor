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
            sb.Append(solution.ConfigurationPlatformsSection.Render());

            var projectConfigurationPlatformsSection = solution.GlobalSection<ProjectConfigurationPlatformsSection>();
            if (projectConfigurationPlatformsSection != null)
            {
                sb.Append(projectConfigurationPlatformsSection.Render());
            }

            var nestedProjectsSection = solution.GlobalSection<NestedProjectsSection>();
            if (nestedProjectsSection != null)
            {
                sb.Append(nestedProjectsSection.Render());
            }

            var solutionPropertiesSection = solution.GlobalSection<SolutionPropertiesSection>();
            if (solutionPropertiesSection != null)
            {
                sb.Append(solutionPropertiesSection.Render());
            }
            var extensibilityGlobalsSection = solution.GlobalSection<ExtensibilityGlobalsSection>();

            if (extensibilityGlobalsSection != null)
            {
                sb.Append(extensibilityGlobalsSection.Render());
            }

            sb.AppendLine("EndGlobal");
            return sb.ToString();
        }
    }
}
