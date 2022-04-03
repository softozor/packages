namespace Softozor.HasuraHandling.Data;

using System.Text.Json.Serialization;

public record ActionRequestPayload<TInputType>(
    [property: JsonPropertyName("action")] HasuraAction Action,
    [property: JsonPropertyName("input")] TInputType Input,
    [property: JsonPropertyName("session_variables")] HasuraSessionVariables SessionVariables)
    where TInputType : class;