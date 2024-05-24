using SlnEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SlnEditor.Mappings
{
    /// <summary>
    /// List of known project type to guid mappings.
    /// Can map either way.
    /// </summary>
    public class ProjectTypeMap
    {
        private static readonly IReadOnlyDictionary<ProjectType, Guid> Mappings = new ReadOnlyDictionary<ProjectType, Guid>(
        new Dictionary<ProjectType, Guid>{
            { ProjectType.AspNet5 , new Guid("8BB2217D-0F2D-49D1-97BC-3654ED321F3B")},
            { ProjectType.AspNetMvc1 , new Guid("603C0E0B-DB56-11DC-BE95-000D561079B0")},
            { ProjectType.AspNetMvc2 , new Guid("F85E285D-A4E0-4152-9332-AB1D724D3325")},
            { ProjectType.AspNetMvc3 , new Guid("E53F8FEA-EAE0-44A6-8774-FFD645390401")},
            { ProjectType.AspNetMvc4 , new Guid("E3E379DF-F4C6-4180-9B81-6769533ABE47")},
            { ProjectType.AspNetMvc5 , new Guid("349C5851-65DF-11DA-9384-00065B846F21")},
            { ProjectType.AzureSdk , new Guid("CC5FD16D-436D-48AD-A40C-5A424C6E3E79")},
            { ProjectType.CPlusPlus , new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942")},
            { ProjectType.CSharp , new Guid("9A19103F-16F7-4668-BE54-9A1E7A4F7556")},
            { ProjectType.CSharp2 , new Guid("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC")},
            { ProjectType.CSharpDynamicsAxAot , new Guid("BF6F8E12-879D-49E7-ADF0-5503146B24B8")},
            { ProjectType.Database , new Guid("4F174C21-8C12-11D0-8340-0000F80270F8")},
            { ProjectType.Database2 , new Guid("A9ACE9BB-CECE-4E62-9AA4-C7E7C5BD2124")},
            { ProjectType.DeploymentCab , new Guid("3EA9E505-35AC-4774-B492-AD1749C4943A")},
            { ProjectType.DeploymentMergeModule , new Guid("06A35CCD-C46D-44D5-987B-CF40FF872267")},
            { ProjectType.DeploymentSetup , new Guid("978C614F-708E-4E1A-B201-565925725DBA")},
            { ProjectType.DeploymentSmartDeviceCab , new Guid("AB322303-2255-48EF-A496-5904EB18DA55")},
            { ProjectType.DistributedSystem , new Guid("F135691A-BF7E-435D-8960-F99683D2D49C")},
            { ProjectType.FSharp , new Guid("F2A71F9B-5D33-465A-A702-920D77279786")},
            { ProjectType.JSharp , new Guid("E6FDF86B-F3D1-11D4-8576-0002A516ECE8")},
            { ProjectType.LegacySmartDeviceCSharp , new Guid("20D4826A-C6FA-45DB-90F4-C717570B9F32")},
            { ProjectType.LegacySmartDeviceVb , new Guid("CB4CE8C6-1BDB-4DC7-A4D3-65A1999772F8")},
            { ProjectType.MicroFramework , new Guid("B69E3092-B931-443C-ABE7-7E7B65F2A37F")},
            { ProjectType.MonoTouch , new Guid("6BC8ED88-2882-458C-8E55-DFD12B67127B")},
            { ProjectType.MonoTouchBinding , new Guid("F5B4F3BC-B597-4E2B-B552-EF5D8A32436F")},
            { ProjectType.PortableClassLibrary , new Guid("786C830F-07A1-408B-BD7F-6EE04809D6DB")},
            { ProjectType.ProjectFolders , new Guid("66A26720-8FB5-11D2-AA7E-00C04F688DDE")},
            { ProjectType.SSIS , new Guid("D183A3D8-5FD8-494B-B014-37F57B35E655")},
            { ProjectType.SharePointCSharp , new Guid("593B0543-81F6-4436-BA1E-4747859CAAE2")},
            { ProjectType.SharePointVb , new Guid("EC05E597-79D4-47f3-ADA0-324C4F7C7484")},
            { ProjectType.SharePointWorkflow , new Guid("F8810EC1-6754-47FC-A15F-DFABD2E3FA90")},
            { ProjectType.SilverLight , new Guid("A1591282-1198-4647-A2B1-27E5FF5F6F3B")},
            { ProjectType.SmartDeviceCSharp , new Guid("4D628B5B-2FBC-4AA6-8C16-197242AEB884")},
            { ProjectType.SmartDeviceVb , new Guid("68B1623D-7FB9-47D8-8664-7ECEA3297D4F")},
            { ProjectType.SolutionFolder , new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8")},
            { ProjectType.SqlServerDataTools, new Guid("00D1A9C2-B5F0-4AF3-8072-F6C62B433612")},
            { ProjectType.Test , new Guid("3AC096D0-A1C2-E12C-1390-A8335801FDAB")},
            { ProjectType.UniversalWindowsClassLibrary , new Guid("A5A43C5B-DE2A-4C0C-9213-0A381AF9435A")},
            { ProjectType.VbNet , new Guid("F184B08F-C81C-45F6-A57F-5ABD9991F28F")},
            { ProjectType.VisualDatabaseTools , new Guid("C252FEB5-A946-4202-B1D4-9916A0590387")},
            { ProjectType.VisualStudioInstallerProjectExtension , new Guid("54435603-DBB4-11D2-8724-00A0C9A8B90C")},
            { ProjectType.Vsta , new Guid("A860303F-1F3F-4691-B57E-529FC101A107")},
            { ProjectType.Vsto , new Guid("BAA0C2D2-18E2-41B9-852F-F413020CAA33")},
            { ProjectType.Wcf , new Guid("3D9AD99F-2412-4246-B90B-4EAA41C64699")},
            { ProjectType.WebSite , new Guid("E24C65DC-7377-472B-9ABA-BC803B73C61A")},
            { ProjectType.WindowsPhoneAppCSharp , new Guid("C089C8C0-30E0-4E22-80C0-CE093F111A43")},
            { ProjectType.WindowsPhoneAppVb , new Guid("DB03555F-0C8B-43BE-9FF9-57896B3C5E56")},
            { ProjectType.WindowsPhoneWebView , new Guid("76F1466A-8B6D-4E39-A767-685A06062A39")},
            { ProjectType.WindowsStoreApps , new Guid("BC8A1FFA-BEE3-4634-8014-F334798102B3")},
            { ProjectType.WorkflowCSharp , new Guid("14822709-B5A1-4724-98CA-57A101D1B079")},
            { ProjectType.WorkflowFoundation , new Guid("32F31D43-81CC-4C15-9DE6-3FC5453562B6")},
            { ProjectType.WorkflowVb , new Guid("D59BE175-2ED0-4C54-BE3D-CDAA9F3214C8")},
            { ProjectType.Wpf , new Guid("60DC8134-EBA5-43B8-BCC9-BB4BC16C2548")},
            { ProjectType.XamarinAndroid , new Guid("EFBA0AD7-5A72-4C68-AF49-83D382785DCF")},
            { ProjectType.XnaWindows , new Guid("6D335F3A-9D43-41b4-9D22-F6F17C4BE596")},
            { ProjectType.XnaXbox , new Guid("2DF5C3F4-5A5F-47a9-8E94-23B4456F55E2")},
            { ProjectType.XnaZune , new Guid("D399B71A-8929-442a-A9AC-8BEC78BB2433")},
        });

        private readonly IReadOnlyDictionary<Guid, ProjectType> _reverse;

        public ProjectTypeMap()
        {
            // https://stackoverflow.com/questions/10966331/two-way-bidirectional-dictionary-in-c/50387165#50387165
            _reverse = Mappings.ToDictionary(pair => pair.Value, pair => pair.Key);
        }

        /// <summary>
        /// Map from Guid to project type
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<Guid, ProjectType> Types => _reverse;

        /// <summary>
        /// Map from project type to guid
        /// </summary>
        public IReadOnlyDictionary<ProjectType, Guid> Guids => Mappings;
    }
}
