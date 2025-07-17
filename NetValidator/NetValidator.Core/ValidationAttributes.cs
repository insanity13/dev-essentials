namespace NetValidator.Core
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class ValidateAttribute : Attribute
    {
        public ValidateAttribute() { }
    }
}
