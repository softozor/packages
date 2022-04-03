namespace Softozor.HasuraHandling.Data;

using System;
using System.Text.Json.Serialization;

public record EventRequestPayload<TInputType>(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("trigger")] Trigger Trigger,
    [property: JsonPropertyName("table")] Table Table,
    [property: JsonPropertyName("event")] HasuraEvent<TInputType> Event)
    where TInputType : class;

public record Trigger([property: JsonPropertyName("name")] string Name);

public record Table(
    [property: JsonPropertyName("schema")] string Schema,
    [property: JsonPropertyName("name")] string Name);

public record HasuraEvent<TInputType>(
    [property: JsonPropertyName("session_variables")] HasuraSessionVariables SessionVariables,
    [property: JsonPropertyName("op")] [property: JsonConverter(typeof(OpConverter))] Op Op,
    [property: JsonPropertyName("data")] EventData<TInputType> Data)
    where TInputType : class;

public record EventData<TInputType>(
    [property: JsonPropertyName("old")] TInputType Old,
    [property: JsonPropertyName("new")] TInputType New)
    where TInputType : class;