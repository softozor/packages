namespace Softozor.HasuraHandling.Data;

using System.Globalization;
using Newtonsoft.Json;

public class ActionErrorResponse
{
    [JsonConstructor]
    public ActionErrorResponse(string message)
    {
        this.Message = message;
    }

    [JsonConstructor]
    public ActionErrorResponse(string message, int statusCode)
        : this(message, new ErrorExtensions(statusCode.ToString(CultureInfo.InvariantCulture)))
    {
    }

    [JsonConstructor]
    public ActionErrorResponse(string message, ErrorExtensions extensions)
    {
        this.Message = message;
        this.Extensions = extensions;
    }

    [JsonProperty("message")]
    public string Message { get; }

    [JsonProperty("extensions")]
    public ErrorExtensions? Extensions { get; }
}