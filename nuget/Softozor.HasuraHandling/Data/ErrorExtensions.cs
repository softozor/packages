namespace Softozor.HasuraHandling.Data;

using Newtonsoft.Json;

public class ErrorExtensions
{
    [JsonConstructor]
    public ErrorExtensions(string code)
    {
        this.Code = code;
    }

    [JsonProperty("code")]
    public string Code { get; }
}