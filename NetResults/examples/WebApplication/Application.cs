namespace WebApplication
{
    internal static class ApplicationInfo
    {
        public static Lazy<string?> Build = new(() => System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString());
    }
}
