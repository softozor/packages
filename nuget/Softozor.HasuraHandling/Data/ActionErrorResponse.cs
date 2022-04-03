namespace Softozor.HasuraHandling.Data;

using System.Globalization;
using System.Text.Json.Serialization;

public record ActionErrorResponse([property: JsonPropertyName("message")] string Message)
{
    public ActionErrorResponse(string message, int statusCode)
        : this(message, new ErrorExtensions(statusCode.ToString(CultureInfo.InvariantCulture)))
    {
    }

    public ActionErrorResponse(string message, ErrorExtensions extensions)
        : this(message)
    {
        this.Extensions = extensions;
    }

    [JsonPropertyName("extensions")]
    public ErrorExtensions? Extensions { get; }
}