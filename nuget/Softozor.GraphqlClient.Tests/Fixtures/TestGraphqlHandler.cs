namespace Softozor.GraphqlClient.Tests.Fixtures;

using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.Extensions.Logging;
using Softozor.GraphqlClient.Interfaces;

public class TestOutput
{
}

public class TestGraphqlHandler : GraphqlHandlerBase
{
    public TestGraphqlHandler(IGraphQLClient graphqlClient, ILogger logger, IRequestBuilder requestBuilder)
        : base(graphqlClient, logger, requestBuilder)
    {
    }

    public async Task<TestOutput> Query(GraphQLRequest req)
    {
        return await SendQueryAsync<TestOutput>(req);
    }

    public async Task<TestOutput> Mutate(GraphQLRequest req)
    {
        return await SendMutationAsync<TestOutput>(req);
    }
}