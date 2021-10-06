namespace Softozor.HasuraHandling.Controller;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softozor.HasuraHandling.Data;
using Softozor.HasuraHandling.Interfaces;

public abstract class ActionControllerBase<TInputType, TOutputType> : HasuraControllerBase
    where TInputType : class
    where TOutputType : class
{
    protected IActionHandler<TInputType, TOutputType> Handler { get; }

    protected ActionControllerBase(IActionHandler<TInputType, TOutputType> handler, ILogger logger)
        : base(logger)
    {
        this.Handler = handler;
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> Post([FromBody] ActionRequestPayload<TInputType> input)
    {
        return await this.DoPost(this.Handle, input.Input);
    }

    protected async Task<IActionResult> DoPost(Func<TInputType, Task<TOutputType>> handlerCallback, TInputType input)
    {
        return await this.TryToHandle(
            async () =>
            {
                var result = await handlerCallback(input);
                return this.Ok(result);
            });
    }

    protected Task<TOutputType> Handle(TInputType input)
    {
        return this.Handler.Handle(input);
    }
}