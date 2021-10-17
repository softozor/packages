namespace Softozor.HasuraHandling.Tests;

using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Softozor.HasuraHandling;
using Softozor.HasuraHandling.Tests.Fixtures;

public class SyncHasuraControllerTests
{
    private SyncTestController? sut;

    [SetUp]
    public void Setup()
    {
        var logger = Mock.Of<ILogger>();
        this.sut = new SyncTestController(logger);
    }

    [TestCase(typeof(UnableToHandleException), typeof(UnauthorizedObjectResult), 401)]
    [TestCase(typeof(GraphqlException), typeof(ObjectResult), 500)]
    [TestCase(typeof(FormatException), typeof(BadRequestObjectResult), 400)]
    [TestCase(typeof(Exception), typeof(ObjectResult), 500)]
    public void ShouldReturnResponseCorrespondingToException(
        Type handlerExceptionType,
        Type expectedResponseType,
        int expectedStatusCode)
    {
        // Given the callback throws some exception
        var exception = Activator.CreateInstance(handlerExceptionType) as Exception;
        Assume.That(exception, Is.Not.Null);
        Func<IActionResult> callback = () => throw exception!;

        // When the controller handles this callback
        var response = this.sut!.TestPost(callback);

        // Then we get the corresponding response
        response.Should().BeOfType(expectedResponseType);
        var actualStatusCode = expectedResponseType.GetProperty(nameof(ObjectResult.StatusCode))?.GetValue(response);
        actualStatusCode.Should().Be(expectedStatusCode);
    }
}