namespace Softozor.HasuraHandling.Data;

using System.Text.Json.Serialization;

public record ErrorExtensions(
    [property: JsonPropertyName("code")] string Code);