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
                sb.AppendLine($"Project(\"{{{project.TypeGuid.ToString().ToUpper()}}}\") = \"{project.Name}\", \"{project.Path}\", \"{{{project.Id.ToString().ToUpper()}}}\"");
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
            sb.AppendLine("EndGlobal");
            /*
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Debug|x64 = Debug|x64
		Debug|x86 = Debug|x86
		Release|Any CPU = Release|Any CPU
		Release|x64 = Release|x64
		Release|x86 = Release|x86
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x64.ActiveCfg = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x64.Build.0 = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x86.ActiveCfg = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Debug|x86.Build.0 = Debug|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|Any CPU.Build.0 = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x64.ActiveCfg = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x64.Build.0 = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x86.ActiveCfg = Release|Any CPU
		{EDC2B9FC-02D0-4541-8484-CAB27B00252D}.Release|x86.Build.0 = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x64.ActiveCfg = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x64.Build.0 = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x86.ActiveCfg = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Debug|x86.Build.0 = Debug|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|Any CPU.Build.0 = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x64.ActiveCfg = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x64.Build.0 = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x86.ActiveCfg = Release|Any CPU
		{BB52A27D-766E-4ECD-B888-BD86405134C1}.Release|x86.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {7F92F20E-4C3D-4316-BF60-105559EFEAFF}
	EndGlobalSection
EndGlobal
");
*/
            return sb.ToString();
        }
    }
}
