namespace Softozor.HasuraHandling.Controller;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softozor.HasuraHandling.Data;

public abstract class HasuraControllerBase : ControllerBase
{
    protected HasuraControllerBase(ILogger logger)
    {
        this.Logger = logger;
    }

    protected ILogger Logger { get; }

    [SuppressMessage(
        "Design",
        "CA1031:Do not catch general exception types",
        Justification =
            "This is the last point before we reach out to the user, therefore we need to capture all possible exceptions")]
    protected async Task<IActionResult> TryToHandle(Func<Task<IActionResult>> callback)
    {
        try
        {
            return await callback();
        }
        catch (UnableToHandleException ex)
        {
            this.Logger.LogWarning($"Caught UnableToLoginException: {ex}");

            return this.Unauthorized(
                new ActionErrorResponse(
                    "Unauthorized access",
                    StatusCodes.Status401Unauthorized.ToString(CultureInfo.InvariantCulture)));
        }
        catch (GraphqlException ex)
        {
            this.Logger.LogWarning($"Caught GraphqlException: {ex}");

            return this.StatusCode(
                StatusCodes.Status500InternalServerError,
                new ActionErrorResponse(
                    ex.Message,
                    StatusCodes.Status500InternalServerError.ToString(CultureInfo.InvariantCulture)));
        }
        catch (FormatException ex)
        {
            this.Logger.LogWarning($"Caught FormatException: {ex}");

            return this.BadRequest(
                new ActionErrorResponse(
                    "Unauthorized access",
                    StatusCodes.Status400BadRequest.ToString(CultureInfo.InvariantCulture)));
        }
        catch (Exception ex)
        {
            this.Logger.LogError($"Caught generic Exception: {ex}");

            return this.StatusCode(
                StatusCodes.Status500InternalServerError,
                new ActionErrorResponse(
                    ex.Message,
                    StatusCodes.Status500InternalServerError.ToString(CultureInfo.InvariantCulture)));
        }
    }
}