namespace Softozor.HasuraHandling.Data;

using System.Globalization;
using System.Text.Json.Serialization;

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

    [JsonPropertyName("message")]
    public string Message { get; }

    [JsonPropertyName("extensions")]
    public ErrorExtensions? Extensions { get; }
}