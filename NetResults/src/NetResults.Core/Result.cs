namespace NetResults.Core
{
    public abstract record Result<T> : Result
    {
        public abstract T? Value { get; }

        public static implicit operator Result<T>(T value) => new Success<T>(value);
        public static implicit operator Result<T>(NotFound error) => new NotFoundResult<T>(error);
        public static implicit operator Result<T>(ValidationError error) => new ValidationResult<T>(error);
        public static implicit operator Result<T>(InternalError error) => new InternalErrorResult<T>(error);
        public static implicit operator Result<T>(UserError error) => new UserErrorResult<T>(error);
    }

    public abstract record Result
    {
        public static Success Success() => _successResult;
        private static readonly Success _successResult = new();

        public abstract string? Message { get; }
        public abstract bool IsSuccess { get; }
        public bool IsFailed => !IsSuccess;

        public static implicit operator Result(NotFound error) => new NotFoundResult(error);
        public static implicit operator Result(ValidationError error) => new ValidationResult(error);
        public static implicit operator Result(InternalError error) => new InternalErrorResult(error);
        public static implicit operator Result(UserError error) => new UserErrorResult(error);
    }
}
