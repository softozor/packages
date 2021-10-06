namespace Softozor.HasuraHandling.Data;

using System;
using Newtonsoft.Json;

public class HasuraSessionVariables
{
    [JsonConstructor]
    public HasuraSessionVariables(Guid userId, string role, string adminSecret)
    {
        this.UserId = userId;
        this.Role = role;
        this.AdminSecret = adminSecret;
    }

    [JsonProperty("x-hasura-user-id")]
    public Guid UserId { get; }

    [JsonProperty("x-hasura-role")]
    public string Role { get; }

    [JsonProperty("x-hasura-admin-secret")]
    public string AdminSecret { get; }
}