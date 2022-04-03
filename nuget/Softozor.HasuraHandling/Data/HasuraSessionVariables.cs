namespace Softozor.HasuraHandling.Data;

using System;
using System.Text.Json.Serialization;

public record HasuraSessionVariables(
    [property: JsonPropertyName("x-hasura-user-id")] Guid UserId,
    [property: JsonPropertyName("x-hasura-role")] string Role,
    [property: JsonPropertyName("x-hasura-admin-secret")] string AdminSecret);