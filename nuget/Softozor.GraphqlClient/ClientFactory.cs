namespace Softozor.GraphqlClient;

using System;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using HasuraHandling.Interfaces;
using Softozor.GraphqlClient.Interfaces;

public class ClientFactory : IClientFactory
{
    private readonly string adminSecret;

    private readonly string graphqlEndpoint;

    public ClientFactory(ISecretReader secretReader)
    {
        this.adminSecret = secretReader.GetSecret(FaasKeys.HasuraSecret);

        this.graphqlEndpoint = Environment.GetEnvironmentVariable("GRAPHQL_API") ??
                               throw new GraphqlClientException("GRAPHQL_API variable not defined");
    }

    public IGraphQLClient CreateAdminClient()
    {
        var client = new GraphQLHttpClient(this.graphqlEndpoint, new NewtonsoftJsonSerializer());
        client.HttpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", this.adminSecret);
        return client;
    }

    public IGraphQLClient CreateAuthenticatedClient(string jwt)
    {
        var client = new GraphQLHttpClient(this.graphqlEndpoint, new NewtonsoftJsonSerializer());
        client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {jwt}");
        return client;
    }

    public IGraphQLClient CreateUnauthenticatedClient()
    {
        return new GraphQLHttpClient(this.graphqlEndpoint, new NewtonsoftJsonSerializer());
    }
}