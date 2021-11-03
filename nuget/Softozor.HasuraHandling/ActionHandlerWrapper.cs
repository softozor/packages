namespace Softozor.HasuraHandling;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Softozor.HasuraHandling.Data;
using Softozor.HasuraHandling.Exceptions;
using Softozor.HasuraHandling.Properties;

public static class ActionHandlerWrapper
{
    [SuppressMessage(
        "Design",
        "CA1031: Do not catch general exception types",
        Justification =
            "That's the last step before outputting to the user, therefore we need to catch everything and return a useful error message")]
    public static async Task HandleSync<TInput, TOutput>(HttpContext http, Func<TInput, TOutput> handle)
        where TInput : class
    {
        var input = await ExtractInput<TInput>(http);

        try
        {
            var output = handle(input);
            await http.Response.WriteAsJsonAsync(output);
        }
        catch (HasuraFunctionException ex)
        {
            await IssueError(http, ex);
        }
        catch (Exception ex)
        {
            await IssueInternalServerError(http, ex);
        }
    }

    [SuppressMessage(
        "Design",
        "CA1031: Do not catch general exception types",
        Justification =
            "That's the last step before outputting to the user, therefore we need to catch everything and return a useful error message")]
    public static async Task HandleAsync<TInput, TOutput>(HttpContext http, Func<TInput, Task<TOutput>> handle)
        where TInput : class
    {
        var input = await ExtractInput<TInput>(http);

        try
        {
            var output = await handle(input);
            await http.Response.WriteAsJsonAsync(output);
        }
        catch (HasuraFunctionException ex)
        {
            await IssueError(http, ex);
        }
        catch (Exception ex)
        {
            await IssueInternalServerError(http, ex);
        }
    }

    public static async Task<TInput> ExtractInput<TInput>(HttpContext http)
        where TInput : class
    {
        var payload = await http.Request.ReadFromJsonAsync<ActionRequestPayload<TInput>>();

        var input = payload?.Input ?? throw new HasuraFunctionException(Resources.InvalidInputError);

        return input;
    }

    public static async Task IssueError(HttpContext http, HasuraFunctionException ex)
    {
        var error = new ActionErrorResponse(ex.Message);
        http.Response.StatusCode = ex.ErrorCode;
        await http.Response.WriteAsJsonAsync(error);
    }

    public static async Task IssueInternalServerError(HttpContext http, Exception ex)
    {
        var error = new ActionErrorResponse(ex.Message);
        http.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await http.Response.WriteAsJsonAsync(error);
    }
}