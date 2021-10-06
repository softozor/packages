namespace Softozor.HasuraHandling.Controller;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softozor.HasuraHandling.Data;
using Softozor.HasuraHandling.Interfaces;

public abstract class EventControllerBase<TInputType, TOutputType> : HasuraControllerBase
    where TInputType : class
    where TOutputType : class
{
    protected IEventHandler<TInputType, TOutputType> Handler { get; }

    protected EventControllerBase(IEventHandler<TInputType, TOutputType> handler, ILogger logger)
        : base(logger)
    {
        this.Handler = handler;
    }

    // here we await because the controller is synchronous
    // when we know how to do asynchronous hasura actions, then this might change
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> Post([FromBody] EventRequestPayload<TInputType> input)
    {
        return await this.DoPost(this.Handle, input.Event.Data.Old, input.Event.Data.New);
    }

    protected async Task<IActionResult> DoPost(
        Func<TInputType, TInputType, Task<TOutputType>> handlerCallback,
        TInputType oldRow,
        TInputType newRow)
    {
        return await this.TryToHandle(
            async () =>
            {
                var result = await handlerCallback(oldRow, newRow);
                return this.Ok(result);
            });
    }

    protected Task<TOutputType> Handle(TInputType oldRow, TInputType newRow)
    {
        return this.Handler.Handle(oldRow, newRow);
    }
}