namespace Softozor.HasuraHandling.Exceptions;

using System;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

[Serializable]
public class HasuraFunctionException : Exception
{
    public HasuraFunctionException()
    {
    }

    public HasuraFunctionException(string message) : base(message)
    {
    }

    public HasuraFunctionException(string message, int errorCode)
        : base(message)
    {
        this.ErrorCode = errorCode;
    }

    public HasuraFunctionException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public HasuraFunctionException(string message, int errorCode, Exception inner)
        : base(message, inner)
    {
        this.ErrorCode = errorCode;
    }

    protected HasuraFunctionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public int ErrorCode { get; } = StatusCodes.Status500InternalServerError;
}