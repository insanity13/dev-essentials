namespace NetResults.Core
{
    public record ValidationErrorResult<T>(ValidationError Error) : Result<T>
    {
        public override T? Value => default;
        public override bool IsSuccess => false;
        public ValidationError Error { get; } = Error;
        public override string? Message => "Validation error";
    }

    public record ValidationResult(ValidationError Error) : Result
    {
        public override bool IsSuccess => Error?.Errors.Count == 0;
        public ValidationError Error { get; } = Error;
        public override string? Message => Error?.Errors.Count > 0 ? "Validation error" : null;

        public static implicit operator ValidationResult(ValidationError error) => new(error);
    }

    public record ValidationError(IReadOnlyDictionary<string, string[]> Errors)
    {
        public ValidationError(string field, string message) : this(new Dictionary<string, string[]> { { field, new[] { message } } }) { }
    }
}
