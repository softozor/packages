namespace Softozor.GraphqlClient.Interfaces;

using GraphQL.Client.Abstractions;

public interface IClientFactory
{
    IGraphQLClient CreateAdminClient();

    IGraphQLClient CreateAuthenticatedClient(string jwt);

    IGraphQLClient CreateUnauthenticatedClient();
}