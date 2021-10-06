namespace Softozor.HasuraHandlingTests.Fixtures;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softozor.HasuraHandling.Controller;

public class SyncTestController : SyncHasuraControllerBase
{
    public SyncTestController(ILogger logger)
        : base(logger)
    {
    }

    public IActionResult TestPost(Func<IActionResult> callback)
    {
        return this.TryToHandle(
            () =>
            {
                var result = callback();
                return this.Ok(result);
            });
    }
}