namespace Softozor.HasuraHandling.Data;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class HasuraClaims
{
    [JsonConstructor]
    public HasuraClaims(IEnumerable<string> roles, string defaultRole, Guid userId)
    {
        this.Roles = roles;
        this.DefaultRole = defaultRole;
        this.UserId = userId;
    }

    [JsonProperty("x-hasura-allowed-roles")]
    public IEnumerable<string> Roles { get; }

    [JsonProperty("x-hasura-default-role")]
    public string DefaultRole { get; }

    [JsonProperty("x-hasura-user-id")]
    public Guid UserId { get; }
}