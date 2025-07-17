namespace NetResults.Core
{
    public record Success<T>(T Value) : Result<T>
    {
        public override T Value { get; } = Value;
        public override bool IsSuccess => true;
        public override string? Message => null;
    }

    public record Success() : Result
    {
        public override bool IsSuccess => true;
        public override string? Message => null;
    }
}
