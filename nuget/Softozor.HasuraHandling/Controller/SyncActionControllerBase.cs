namespace Softozor.HasuraHandling.Controller;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softozor.HasuraHandling.Data;
using Softozor.HasuraHandling.Interfaces;

public abstract class SyncActionControllerBase<TInputType, TOutputType> : SyncHasuraControllerBase
    where TInputType : class
    where TOutputType : class
{
    protected ISyncActionHandler<TInputType, TOutputType> Handler { get; }

    protected SyncActionControllerBase(ISyncActionHandler<TInputType, TOutputType> handler, ILogger logger)
        : base(logger)
    {
        this.Handler = handler;
    }

    [HttpPost]
    [Consumes("application/json")]
    public IActionResult Post([FromBody] ActionRequestPayload<TInputType> input)
    {
        return this.DoPost(this.Handle, input.Input);
    }

    protected IActionResult DoPost(Func<TInputType, TOutputType> handlerCallback, TInputType input)
    {
        return this.TryToHandle(
            () =>
            {
                var result = handlerCallback(input);
                return this.Ok(result);
            });
    }

    protected TOutputType Handle(TInputType input)
    {
        return this.Handler.Handle(input);
    }
}