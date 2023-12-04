using SlnParser.Contracts;
using System;
using System.Linq;
using System.Text;

namespace SlnParser.Helper
{
    /// <summary>
    ///
    /// </summary>
    public static class SolutionWriter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string Write(Solution solution)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine($"Microsoft Visual Studio Solution File, Format Version {solution.FileFormatVersion}");
            sb.AppendLine($"# Visual Studio Version {solution.VisualStudioVersion.MajorVersion}");
            sb.AppendLine($"VisualStudioVersion = {solution.VisualStudioVersion.Version}");
            sb.AppendLine($"MinimumVisualStudioVersion = {solution.VisualStudioVersion.MinimumVersion}");
            foreach (var project in solution.AllProjects)
            {
                sb.AppendLine(
                    $"Project(\"{{{project.TypeGuid.ToString().ToUpper()}}}\") = \"{project.Name}\", \"{project.Path}\", \"{{{project.Id.ToString().ToUpper()}}}\"");
                if (project is SolutionFolder solutionFolder && solutionFolder.Files.Any())
                {
                    sb.AppendLine("\tProjectSection(SolutionItems) = preProject");
                    foreach (var file in solutionFolder.Files)
                    {
                        sb.AppendLine($"\t\t{file.Name} = {file.Name}");
                    }

                    sb.AppendLine("\tEndProjectSection");
                }

                sb.AppendLine("EndProject");
            }

            sb.AppendLine("Global");
            sb.AppendLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            foreach (var platform in solution.ConfigurationPlatforms)
            {
                sb.AppendLine($"\t\t{platform.Name} = {platform.Name}");
            }

            sb.AppendLine("\tEndGlobalSection");
            sb.AppendLine("\tGlobalSection(SolutionProperties) = preSolution");
            sb.AppendLine("\t\tHideSolutionNode = FALSE");
            sb.AppendLine("\tEndGlobalSection");
            sb.AppendLine("EndGlobal");
            return sb.ToString();
        }
    }
}
