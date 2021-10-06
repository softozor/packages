namespace Softozor.HasuraHandling.Data;

using Newtonsoft.Json;

public class ActionErrorResponse
{
    [JsonConstructor]
    public ActionErrorResponse(string message, string code)
    {
        this.Message = message;
        this.Code = code;
    }

    [JsonProperty("message")]
    public string Message { get; }

    [JsonProperty("code")]
    public string Code { get; }
}