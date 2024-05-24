namespace SlnEditor.Models
{
    /// <summary>
    /// The well-known project types
    /// </summary>
    public enum ProjectType
    {
        /// <summary>
        ///     Deployment Merge Module
        /// </summary>
        DeploymentMergeModule,

        /// <summary>
        ///     C# Workflow
        /// </summary>
        WorkflowCSharp,

        /// <summary>
        ///     Legacy (2003) Smart Device (C#)
        /// </summary>
        LegacySmartDeviceCSharp,

        /// <summary>
        ///     Solution Folder
        /// </summary>
        SolutionFolder,

        /// <summary>
        ///     XNA (XBox)
        /// </summary>
        XnaXbox,

        /// <summary>
        ///     Workflow Foundation
        /// </summary>
        WorkflowFoundation,

        /// <summary>
        ///     ASP.NET MVC 5
        /// </summary>
        AspNetMvc5,

        /// <summary>
        ///     Test
        /// </summary>
        Test,

        /// <summary>
        ///     Windows Communication Foundation (WCF)
        /// </summary>
        Wcf,

        /// <summary>
        ///     Deployment Cab
        /// </summary>
        DeploymentCab,

        /// <summary>
        ///     Smart Device (C#)
        /// </summary>
        SmartDeviceCSharp,

        /// <summary>
        ///     Database
        /// </summary>
        Database,

        /// <summary>
        ///     Database - alternative guid
        /// </summary>
        Database2,

        /// <summary>
        ///     Visual Studio 2015 Installer Project Extension
        /// </summary>
        VisualStudioInstallerProjectExtension,

        /// <summary>
        ///     SharePoint (C#)
        /// </summary>
        SharePointCSharp,

        /// <summary>
        ///     ASP.NET MVC 1.0
        /// </summary>
        AspNetMvc1,

        /// <summary>
        ///     Windows Presentation Foundation (WPF)
        /// </summary>
        Wpf,

        /// <summary>
        ///     Smart Device (VB.NET)
        /// </summary>
        SmartDeviceVb,

        /// <summary>
        ///     Project Folders
        /// </summary>
        ProjectFolders,

        /// <summary>
        ///     MonoTouch
        /// </summary>
        MonoTouch,

        /// <summary>
        ///     XNA (Windows)
        /// </summary>
        XnaWindows,

        /// <summary>
        ///     Windows Phone 8/8.1 Blank/Hub/Webview App
        /// </summary>
        WindowsPhoneWebView,

        /// <summary>
        ///     Portable Class Library
        /// </summary>
        PortableClassLibrary,

        /// <summary>
        ///     ASP.NET 5
        /// </summary>
        AspNet5,

        /// <summary>
        ///     C++
        /// </summary>
        CPlusPlus,

        /// <summary>
        ///     Deployment Setup
        /// </summary>
        DeploymentSetup,

        /// <summary>
        ///     C# Class Library
        /// </summary>
        CSharp,

        /// <summary>
        /// SQL Server Integration Services Projects
        /// https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects
        /// </summary>
        SSIS,


        /// <summary>
        ///     C# Class Library, alternative guid
        /// </summary>
        CSharp2,

        /// <summary>
        ///     SilverLight
        /// </summary>
        SilverLight,

        /// <summary>
        ///     Universal Windows Class Library
        /// </summary>
        UniversalWindowsClassLibrary,

        /// <summary>
        ///     Visual Studio Tools for Applications (VSTA)
        /// </summary>
        Vsta,

        /// <summary>
        ///     Deployment Smart Device Cab
        /// </summary>
        DeploymentSmartDeviceCab,

        /// <summary>
        ///     Micro Framework
        /// </summary>
        MicroFramework,

        /// <summary>
        ///     Visual Studio Tools for Office (VSTO)
        /// </summary>
        Vsto,

        /// <summary>
        ///     Windows Store Apps (Metro Apps
        /// </summary>
        WindowsStoreApps,

        /// <summary>
        ///     C# in Dynamics 2012 AX AOT
        /// </summary>
        CSharpDynamicsAxAot,

        /// <summary>
        ///     Windows Phone 8/8.1 App (C#)
        /// </summary>
        WindowsPhoneAppCSharp,

        /// <summary>
        ///     Visual Database Tools
        /// </summary>
        VisualDatabaseTools,

        /// <summary>
        ///     Legacy (2003) Smart Device (VB.NET)
        /// </summary>
        LegacySmartDeviceVb,

        /// <summary>
        ///     XNA (Zune)
        /// </summary>
        XnaZune,

        /// <summary>
        ///     Workflow (VB.NET)
        /// </summary>
        WorkflowVb,

        /// <summary>
        ///     Windows Phone 8/8.1 App (VB.NET)
        /// </summary>
        WindowsPhoneAppVb,

        /// <summary>
        ///     Web Site
        /// </summary>
        WebSite,

        /// <summary>
        ///     ASP.NET MVC 4
        /// </summary>
        AspNetMvc4,

        /// <summary>
        ///     ASP.NET MVC 3
        /// </summary>
        AspNetMvc3,

        /// <summary>
        ///     J#
        /// </summary>
        JSharp,

        /// <summary>
        ///     SharePoint (VB.NET)
        /// </summary>
        SharePointVb,

        /// <summary>
        ///     Xamarin.Android / Mono for Android
        /// </summary>
        XamarinAndroid,

        /// <summary>
        ///     Distributed System
        /// </summary>
        DistributedSystem,

        /// <summary>
        ///     VB.NET
        /// </summary>
        VbNet,

        /// <summary>
        ///     F#
        /// </summary>
        FSharp,

        /// <summary>
        ///     MonoTouch Binding
        /// </summary>
        MonoTouchBinding,

        /// <summary>
        ///     ASP.NET MVC 2.0
        /// </summary>
        AspNetMvc2,

        /// <summary>
        ///     SharePoint Workflow
        /// </summary>
        SharePointWorkflow,

        /// <summary>
        /// Sql Server Data Tools, aka Database projects.
        /// </summary>
        SqlServerDataTools,

        /// <summary>
        /// Some kind of azure cloud project.
        /// - https://github.com/timabell/SlnEditor/issues/11
        /// - https://stackoverflow.com/questions/27556901/visual-studio-2013-2015-cant-load-azure-project
        /// - https://github.com/microsoft/slngen/blob/main/src/Microsoft.VisualStudio.SlnGen/VisualStudioProjectTypeGuids.cs#L12-L15
        /// </summary>
        AzureSdk,
    }
}
