namespace NetResults.Core
{
    public record UserErrorResult<T>(UserError Error) : Result<T>
    {
        public override T? Value => default;
        public override bool IsSuccess => false;
        public override string? Message => Error?.Message;
        public UserError Error { get; } = Error;
    }

    public record UserErrorResult(UserError Error) : Result
    {
        public override bool IsSuccess => false;
        public override string? Message => Error?.Message;
        public UserError Error { get; } = Error;
    }

    public record UserError(string Message);
}
