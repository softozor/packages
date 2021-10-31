namespace Softozor.GraphqlClient.Tests;

using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Softozor.GraphqlClient.Interfaces;
using Softozor.GraphqlClient.Tests.Fixtures;

public class GraphqlHandlerTest
{
    private IGraphQLClient? client;

    private IRequestBuilder? requestBuilder;

    private TestGraphqlHandler? sut;

    [SetUp]
    public void Setup()
    {
        this.client = Mock.Of<IGraphQLClient>();
        var logger = Mock.Of<ILogger>();
        this.requestBuilder = Mock.Of<IRequestBuilder>();
        this.sut = new TestGraphqlHandler(this.client, logger, this.requestBuilder);
    }

    [Test]
    public void ShouldThrowGraphqlExceptionWhenQueryResultsInGraphqlErrors()
    {
        // Given the graphql query returns an error
        var expectedResponse = new GraphQLResponse<TestOutput>
        {
            Errors = new[]
            {
                new GraphQLError
                {
                    Message = "The error message",
                    Locations = new[] { new GraphQLLocation { Column = 1, Line = 1 } }
                }
            }
        };
        var clientStub = Mock.Get(this.client);
        clientStub.Setup(
                c => c!.SendQueryAsync<TestOutput>(It.IsAny<GraphQLRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // When I perform the graphql query
        var req = Mock.Of<GraphQLRequest>();
        Func<Task> act = async () => await this.sut!.Query(req);

        // Then I get a graphql exception thrown
        act.Should().ThrowAsync<GraphqlClientException>();
    }

    [Test]
    public void ShouldThrowGraphqlExceptionWhenMutationResultsInGraphqlErrors()
    {
        // Given the graphql query returns an error
        var expectedResponse = new GraphQLResponse<TestOutput>
        {
            Errors = new[]
            {
                new GraphQLError
                {
                    Message = "The error message",
                    Locations = new[] { new GraphQLLocation { Column = 1, Line = 1 } }
                }
            }
        };
        var clientStub = Mock.Get(this.client);
        clientStub.Setup(
                c => c!.SendMutationAsync<TestOutput>(
                    It.IsAny<GraphQLRequest>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // When I perform the graphql query
        var req = Mock.Of<GraphQLRequest>();
        Func<Task> act = async () => await this.sut!.Mutate(req);

        // Then I get a graphql exception thrown
        act.Should().ThrowAsync<GraphqlClientException>();
    }
}