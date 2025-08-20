using NetResults.Core.Errors;

namespace NetResults.Core
{
    public readonly record struct Result<T>
    {
        public T? Value { get; }
        public ErrorBase? Error { get; }
        public readonly bool IsSuccess => Error is null;
        public readonly bool IsFailed => !IsSuccess;

        public Result(T? value, ErrorBase? error = null) { Value = value; Error = error; }

        public static implicit operator Result<T>(T value) => new(value);

        public static implicit operator Result<T>(NotFound error) => new(default, error);
        public static implicit operator Result<T>(ValidationError error) => new(default, error);
        public static implicit operator Result<T>(InternalError error) => new(default, error);
        public static implicit operator Result<T>(UserError error) => new(default, error);
        public static implicit operator Result<T>(ErrorBase error) => new(default, error);
    }

    public readonly record struct Result
    {
        public ErrorBase? Error { get; }
        public readonly bool IsSuccess => Error is null;
        public readonly bool IsFailed => !IsSuccess;

        public Result() { }
        public Result(ErrorBase? error)
        {
            Error = error;
        }

        public static Result Success() => _successResult;
        private static readonly Result _successResult = new();

        public static implicit operator Result(NotFound error) => new(error);
        public static implicit operator Result(ValidationError error) => new(error);
        public static implicit operator Result(InternalError error) => new(error);
        public static implicit operator Result(UserError error) => new(error);
        public static implicit operator Result(ErrorBase error) => new(error);
    }
}
