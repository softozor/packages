namespace Softozor.HasuraHandling.Exceptions;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GraphQL;

[Serializable]
public class GraphqlException : Exception
{
    public GraphqlException()
    {
    }

    public GraphqlException(string message)
        : base(message)
    {
    }

    public GraphqlException(string message, IEnumerable<GraphQLError> errors)
        : base(message)
    {
        this.Errors = errors;
    }

    public GraphqlException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public GraphqlException(string message, IEnumerable<GraphQLError> errors, Exception inner)
        : base(message, inner)
    {
        this.Errors = errors;
    }

    protected GraphqlException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public IEnumerable<GraphQLError>? Errors { get; }
}