namespace Softozor.HasuraHandling.Data;

using Newtonsoft.Json;

public class HasuraAction
{
    [JsonConstructor]
    public HasuraAction(string name)
    {
        this.Name = name;
    }

    [JsonProperty("name")]
    public string Name { get; }
}