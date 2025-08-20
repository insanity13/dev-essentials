using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetResults.Core;
using NetResults.Core.Errors;

namespace NetResults.AspNetCore
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return Results.Ok(result.Value);

            return ConvertError(result.Error!);
        }

        public static IResult ToHttpResult(this Result result)
        {
            if (result.IsSuccess)
                return Results.Ok();

            return ConvertError(result.Error!);
        }

        private static IResult ConvertError(ErrorBase error)
        {
            return error switch
            {
                NotFound notFound => Results.NotFound(CreateProblemDetails(
                    notFound.ErrorMessage,
                    StatusCodes.Status404NotFound,
                    "https://tools.ietf.org/html/rfc7231#section-6.5.4")),

                InternalError serverError => Results.Problem(
                    detail: serverError.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: serverError.ErrorMessage,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1"),

                ValidationError validationError => Results.BadRequest(CreateProblemDetails(
                    "Validation error",
                    StatusCodes.Status422UnprocessableEntity,
                    "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    validationError.Errors)),

                UserError userError => Results.Problem(
                    detail: userError.ErrorMessage,
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad request",
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.1"),
                _ => throw new NotSupportedException("Unknown result type")
            };
        }

        private static ProblemDetails CreateProblemDetails(
            string title,
            int status,
            string type,
            IReadOnlyDictionary<string, string[]>? errors = null)
        {
            return new ProblemDetails
            {
                Title = title,
                Status = status,
                Type = type,
                Extensions = errors != null ? new Dictionary<string, object> { ["errors"] = errors } : null!
            };
        }
    }
}
