using NetResults.Core;
using NetResults.Core.Errors;
using System.ComponentModel.DataAnnotations;

namespace NetResults.Extensions
{
    public static class ResultExtensions
    {
        public static T GetValueOrThrow<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return result.Value!;

            return result.Error switch
            {
                NotFound notFound => throw new InvalidOperationException($"Not Found: {notFound.ErrorMessage}"),
                ValidationError validation => throw new ValidationException($"Validation Failed: {string.Join(", ", validation.Errors.SelectMany(e => e.Value))}"),
                InternalError serverError => throw new InvalidOperationException($"Server Error: {serverError.ErrorMessage}", serverError.Exception),
                UserError userError => throw new InvalidOperationException($"Business Error: {userError.ErrorMessage}"),

                _ => throw new InvalidOperationException("Unknown Error State")
            };
        }

        public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapper)
        {
            if (result.IsSuccess)
                return mapper(result.Value!);

            return result.Error!;
        }
    }
}
