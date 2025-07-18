namespace NetOptions.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class OptionsAttribute(string section, bool validateOnStart = true) : Attribute
    {
        public string Section { get; } = section;
        public bool ValidateOnStart { get; } = validateOnStart;
    }
}
