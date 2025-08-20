namespace NetResults.Core.Errors
{
    public record NotFound(string ErrorMessage) : ErrorBase
    {
        public override string Message() => ErrorMessage;
    }
}
