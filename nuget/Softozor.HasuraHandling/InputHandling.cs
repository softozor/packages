namespace Softozor.HasuraHandling;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Softozor.HasuraHandling.Data;
using Softozor.HasuraHandling.Properties;

public static class InputHandling
{
    public static async Task<TInput> ExtractActionRequestPayloadFrom<TInput>(HttpContext http)
        where TInput : class
    {
        var payload = await http.Request.ReadFromJsonAsync<ActionRequestPayload<TInput>>();

        var input = payload?.Input ?? throw new HasuraFunctionException(Resources.InvalidInputError);

        return input;
    }
}