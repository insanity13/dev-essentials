namespace NetValidator.Core
{
    public readonly struct ValidationResult(bool isValid, IReadOnlyDictionary<string, string[]>? errors)
    {
        public static readonly ValidationResult Success = new(true, null);

        public bool IsValid { get; } = isValid;
        public IReadOnlyDictionary<string, string[]> Errors { get; } = errors ?? new Dictionary<string, string[]>();

        public static ValidationResult Fail(IReadOnlyDictionary<string, string[]> errors) => new(false, errors);
    }
}
