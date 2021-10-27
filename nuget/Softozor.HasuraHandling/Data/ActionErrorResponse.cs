namespace Softozor.HasuraHandling.Data;

using Newtonsoft.Json;

public class ActionErrorResponse
{
    [JsonConstructor]
    public ActionErrorResponse(string message, ErrorExtensions? extensions)
    {
        this.Message = message;
        this.Extensions = extensions;
    }

    [JsonProperty("message")]
    public string Message { get; }

    [JsonProperty("extensions")]
    public ErrorExtensions? Extensions { get; }
}