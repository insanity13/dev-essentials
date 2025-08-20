namespace NetResults.Core.Errors
{
    public record InternalError(string ErrorMessage, Exception? Exception = null) : ErrorBase
    {
        public override string Message() => ErrorMessage;
    }
}
