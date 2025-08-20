namespace NetResults.Core.Errors
{
    public record ValidationError(IReadOnlyDictionary<string, string[]> Errors) : ErrorBase
    {
        public ValidationError(string field, string message) : this(new Dictionary<string, string[]> { { field, new[] { message } } }) { }

        public override string Message()
        {
            return string.Join("; ", Errors.SelectMany(kvp => kvp.Value.Select(v => $"{kvp.Key}: {v}")));
        }
    }
}
