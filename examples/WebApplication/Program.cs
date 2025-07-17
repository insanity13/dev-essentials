using Application;
using Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using NetResults.AspNetCore;
using NetResults.Core;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using WebApplication;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

// Register services
builder.Services.AddInfrastructure().AddApplication();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
        context.ProblemDetails.Extensions.TryAdd("build", ApplicationInfo.Build.Value);
    };
});

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseTodoEndpoints();

app.Use(async (context, next) => // TODO: Exception Handler.
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        Result result = ex switch
        {
            ValidationException => new UserErrorResult(new UserError(ex.Message)),
            _ => new InternalErrorResult(new InternalError("An unexpected error occurred", ex))
        };

        await result.ToHttpResult().ExecuteAsync(context);
    }
});

app.Run();