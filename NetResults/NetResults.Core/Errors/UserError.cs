namespace NetResults.Core.Errors
{
    public record UserError(string ErrorMessage) : ErrorBase
    {
        public override string Message() => ErrorMessage;
    }
}
