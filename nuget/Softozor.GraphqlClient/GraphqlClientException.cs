namespace Softozor.GraphqlClient;

using System;
using System.Runtime.Serialization;

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

    public GraphqlClientException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected GraphqlClientException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}