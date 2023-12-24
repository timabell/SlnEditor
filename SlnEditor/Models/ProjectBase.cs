namespace SlnEditor.Models
{
    internal static class RenderHelpers
    {
        public static string Header(this IProject project) =>
            $"Project(\"{{{project.TypeGuid.ToString().ToUpper()}}}\") = \"{project.Name}\", \"{project.Path}\", \"{{{project.Id.ToString().ToUpper()}}}\"";
    }
}
