namespace Softozor.HasuraHandling.Exceptions;

using System;
using System.Runtime.Serialization;

[Serializable]
public class UnableToHandleException : Exception
{
    public UnableToHandleException()
    {
    }

    public UnableToHandleException(string message)
        : base(message)
    {
    }

    public UnableToHandleException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected UnableToHandleException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public int StatusCode { get; set; }
}