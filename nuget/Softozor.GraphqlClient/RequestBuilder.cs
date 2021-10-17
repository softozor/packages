namespace Softozor.GraphqlClient;

using System;
using System.IO;
using System.Text.RegularExpressions;
using GraphQL;
using Softozor.GraphqlClient.Interfaces;

public class RequestBuilder : IRequestBuilder
{
    private const string PathToGraphqlQueries = @"./graphql";

    public GraphQLRequest Build(string operationName, dynamic? variables)
    {
        return new GraphQLRequest
        {
            Query = this.GetRequestFromOperationName(operationName),
            OperationName = operationName,
            Variables = variables
        };
    }

    private string GetRequestFromOperationName(string operationName)
    {
        var query = this.FromFile(operationName);
        if (!query.Contains(operationName, StringComparison.InvariantCulture))
        {
            throw new GraphqlClientException($"Operation {operationName} not found in graphql query.");
        }

        return query;
    }

    private string FromFile(string filenameWithoutExt)
    {
        var result = File.ReadAllText(this.PathToGraphqlFixture(filenameWithoutExt));
        return Regex.Replace(result, @"\n", "");
    }

    private string PathToGraphqlFixture(string filenameWithoutExt)
    {
        return Path.Combine(PathToGraphqlQueries, filenameWithoutExt + ".graphql");
    }
}