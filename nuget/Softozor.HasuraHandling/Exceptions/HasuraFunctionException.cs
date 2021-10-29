namespace Softozor.HasuraHandling.Exceptions;

using System;
using System.Runtime.Serialization;

[Serializable]
public class HasuraFunctionException : Exception
{
    public HasuraFunctionException()
    {
    }

    public HasuraFunctionException(string message)
        : base(message)
    {
    }

    public HasuraFunctionException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected HasuraFunctionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}