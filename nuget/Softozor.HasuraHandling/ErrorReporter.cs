namespace Softozor.HasuraHandling;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Softozor.HasuraHandling.Data;
using Softozor.HasuraHandling.Properties;

public static class ErrorReporter
{
    public static async Task ReportError(HttpContext http, HasuraFunctionException ex)
    {
        var error = new ActionErrorResponse(ex.Message, ex.ErrorCode);
        http.Response.StatusCode = ex.ErrorCode;
        await http.Response.WriteAsJsonAsync(error);
    }

    public static async Task ReportUnexpectedError(HttpContext http, Exception ex)
    {
        var error = new ActionErrorResponse(ex.Message, StatusCodes.Status500InternalServerError);
        http.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await http.Response.WriteAsJsonAsync(error);
    }
}