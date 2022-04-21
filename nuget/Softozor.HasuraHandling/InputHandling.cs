namespace Softozor.HasuraHandling;

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Softozor.HasuraHandling.Data;
using Softozor.HasuraHandling.Properties;

public static class InputHandling
{
    private const string DefaultAccessTokenAuthHeaderPrefix = "Bearer";

    public static async Task<TInput> ExtractActionRequestPayloadFrom<TInput>(HttpContext http)
        where TInput : class
    {
        var payload = await http.Request.ReadFromJsonAsync<ActionRequestPayload<TInput>>();

        var input = payload?.Input ?? throw new HasuraFunctionException(Resources.InvalidInputError);

        return input;
    }

    public static string ExtractJwt(HttpContext http)
        {
            var authorization = http.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                throw new HasuraFunctionException(
                    "The request contains no authorization header",
                    StatusCodes.Status400BadRequest);
            }

            if (!authorization.StartsWith(DefaultAccessTokenAuthHeaderPrefix, StringComparison.OrdinalIgnoreCase))
            {
                throw new HasuraFunctionException(
                    "The authorization header does not contain a Bearer token",
                    StatusCodes.Status400BadRequest);
            }

            var token = authorization.Substring(DefaultAccessTokenAuthHeaderPrefix.Length).Trim();

            return token;
        }
}