namespace Softozor.GraphqlClient.Interfaces;

using GraphQL;

public interface IRequestBuilder
{
    GraphQLRequest Build(string operationName, dynamic? variables = null);
}