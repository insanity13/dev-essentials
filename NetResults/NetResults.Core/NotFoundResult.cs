namespace NetResults.Core
{
    public record NotFoundResult<T>(NotFound Error) : Result<T>
    {
        public override T? Value => default;
        public override bool IsSuccess => false;
        public override string? Message => Error?.Message;
        public NotFound Error { get; } = Error;
    }

    public record NotFoundResult(NotFound Error) : Result
    {
        public override bool IsSuccess => false;
        public override string? Message => Error?.Message;
        public NotFound Error { get; } = Error;
    }

    public record NotFound(string Message);
}
