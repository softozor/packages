namespace Softozor.GraphqlClient;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GraphQL;

[Serializable]
public class GraphqlClientException : Exception
{
    public GraphqlClientException()
    {
    }

    public GraphqlClientException(string message)
        : base(message)
    {
    }

    public GraphqlClientException(string message, IEnumerable<GraphQLError> errors)
        : base(message)
    {
        this.Errors = errors;
    }

    public GraphqlClientException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public GraphqlClientException(string message, IEnumerable<GraphQLError> errors, Exception inner)
        : base(message, inner)
    {
        this.Errors = errors;
    }

    protected GraphqlClientException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public IEnumerable<GraphQLError>? Errors { get; }
}
