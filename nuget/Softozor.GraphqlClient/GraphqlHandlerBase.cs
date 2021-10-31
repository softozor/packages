namespace Softozor.GraphqlClient;

using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using HasuraHandling;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Softozor.GraphqlClient.Interfaces;

public class GraphqlHandlerBase
{
    protected GraphqlHandlerBase(IGraphQLClient graphqlClient, ILogger logger, IRequestBuilder requestBuilder)
    {
        this.GraphqlClient = graphqlClient;
        this.Logger = logger;
        this.RequestBuilder = requestBuilder;
    }

    protected IGraphQLClient GraphqlClient { get; }

    protected ILogger Logger { get; }

    protected IRequestBuilder RequestBuilder { get; }

    protected async Task<TOutput> SendMutationAsync<TOutput>(GraphQLRequest req)
    {
        var resp = await this.GraphqlClient.SendMutationAsync<TOutput>(req);
        this.CheckGraphqlErrors(resp, req);

        return resp.Data;
    }

    protected async Task<TOutput> SendQueryAsync<TOutput>(GraphQLRequest req)
    {
        var resp = await this.GraphqlClient.SendQueryAsync<TOutput>(req);
        this.CheckGraphqlErrors(resp, req);

        return resp.Data;
    }

    protected void CheckGraphqlErrors<TOutput>(GraphQLResponse<TOutput> resp, GraphQLRequest req)
    {
        if (resp.Errors?.Length > 0)
        {
            this.Logger.LogError($"Got {resp.Errors.Length} graphql errors");
            var firstError = JsonConvert.SerializeObject(resp.Errors.First());
            this.Logger.LogError($"First error: {firstError}");
            var serializedVariables = JsonConvert.SerializeObject(req.Variables);
            var errorMsg = $"Failed to execute query {req.OperationName} with variables {serializedVariables}";
            throw new GraphqlClientException(errorMsg, resp.Errors);
        }
    }
}