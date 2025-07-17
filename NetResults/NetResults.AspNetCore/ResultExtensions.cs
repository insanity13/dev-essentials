using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetResults.Core;

namespace NetResults.AspNetCore
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResult<T>(this Result<T> result)
        {
            return result switch
            {
                Success<T> success => Results.Ok(success.Value),

                NotFoundResult<T> notFound => Results.NotFound(CreateProblemDetails(
                    notFound.Error.Message,
                    StatusCodes.Status404NotFound,
                    "https://tools.ietf.org/html/rfc7231#section-6.5.4")),

                InternalErrorResult<T> serverError => Results.Problem(
                    detail: serverError.Error.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: serverError.Message,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1"),

                ValidationResult<T> validation => Results.BadRequest(CreateProblemDetails(
                    validation.Message ?? "Validation error",
                    StatusCodes.Status400BadRequest,
                    "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    validation.Error.Errors)),

                UserErrorResult<T> userError => Results.Problem(
                    detail: userError.Error.Message,
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad request",
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.1"),
                _ => throw new NotSupportedException("Unknown result type")
            };
        }

        public static IResult ToHttpResult(this Result result)
        {
            return result switch
            {
                Success success => Results.Ok(),

                Core.NotFoundResult notFound => Results.NotFound(CreateProblemDetails(
                    notFound.Error.Message,
                    StatusCodes.Status404NotFound,
                    "https://tools.ietf.org/html/rfc7231#section-6.5.4")),

                InternalErrorResult serverError => Results.Problem(
                    detail: serverError.Error.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: serverError.Message,
                    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1"),

                Core.ValidationResult validation => Results.BadRequest(CreateProblemDetails(
                    validation.Message ?? "Validation error",
                    StatusCodes.Status400BadRequest,
                    "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    validation.Error.Errors)),

                UserErrorResult userError => Results.Problem(
                    detail: userError.Error.Message,
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
