namespace NetResults.Core
{
    public record InternalErrorResult<T>(InternalError Error) : Result<T>
    {
        public override T? Value => default;
        public override bool IsSuccess => false;
        public InternalError Error { get; } = Error;
        public override string? Message => Error?.Message;
    }

    public record InternalErrorResult(InternalError Error) : Result
    {
        public override bool IsSuccess => false;
        public InternalError Error { get; } = Error;
        public override string? Message => Error?.Message;
    }

    public record InternalError(string Message, Exception? Exception = null);
}
