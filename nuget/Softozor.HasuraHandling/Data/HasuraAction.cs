namespace Softozor.HasuraHandling.Data;

using System.Text.Json.Serialization;

public record HasuraAction([property: JsonPropertyName("name")] string Name);