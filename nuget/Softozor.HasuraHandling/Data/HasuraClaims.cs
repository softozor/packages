namespace Softozor.HasuraHandling.Data;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public record HasuraClaims(
    [property: JsonPropertyName("x-hasura-allowed-roles")] IEnumerable<string> Roles,
    [property: JsonPropertyName("x-hasura-default-role")] string DefaultRole,
    [property: JsonPropertyName("x-hasura-user-id")] Guid UserId);