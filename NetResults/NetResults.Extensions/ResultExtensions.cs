using NetResults.Core;
using System.ComponentModel.DataAnnotations;

namespace NetResults.Extensions
{
    public static class ResultExtensions
    {
        public static T GetValueOrThrow<T>(this Result result)
        {
            return result switch
            {
                Success<T> success => success.Value,
                NotFoundResult<T> notFound => throw new InvalidOperationException($"Not Found: {notFound.Error.Message}"),
                ValidationResult<T> validation => throw new ValidationException($"Validation Failed: {string.Join(", ", validation.Error.Errors.SelectMany(e => e.Value))}"),
                InternalErrorResult<T> serverError => throw new InvalidOperationException($"Server Error: {serverError.Error.Message}", serverError.Error.Exception),
                UserErrorResult<T> userError => throw new InvalidOperationException($"Business Error: {userError.Error.Message}"),

                _ => throw new InvalidOperationException("Unknown Error State")
            };
        }

        public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapper)
        {
            return result switch
            {
                Success<TIn> success => new Success<TOut>(mapper(success.Value)),
                NotFoundResult<TIn> notFound => new NotFoundResult<TOut>(notFound.Error),
                ValidationResult<TIn> validation => new ValidationResult<TOut>(validation.Error),
                InternalErrorResult<TIn> internalError => new InternalErrorResult<TOut>(internalError.Error),
                UserErrorResult<TIn> userError => new UserErrorResult<TOut>(userError.Error),
                _ => throw new NotSupportedException($"Unknown result type: {result.GetType().Name}")
            };
        }

        public static Result<TOut> AsTyped<TOut>(this Result result)
        {
            return result switch
            {
                NotFoundResult notFound => notFound.Error,
                Core.ValidationResult validation => validation.Error,
                InternalErrorResult internalError => internalError.Error,
                UserErrorResult userError => userError.Error,

                Success<TOut> success => success,
                NotFoundResult<TOut> notFound => notFound,
                ValidationResult<TOut> validation => validation,
                InternalErrorResult<TOut> internalError => internalError,
                UserErrorResult<TOut> userError => userError,
                _ => throw new NotSupportedException($"Unknown result type: {result.GetType().Name}")
            };
        }
    }
}
