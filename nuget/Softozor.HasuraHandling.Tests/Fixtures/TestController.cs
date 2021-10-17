namespace Softozor.HasuraHandling.Tests.Fixtures;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Softozor.HasuraHandling.Controller;

public class TestController : HasuraControllerBase
{
    public TestController(ILogger logger)
        : base(logger)
    {
    }

    public async Task<IActionResult> TestPost(Func<Task<IActionResult>> callback)
    {
        return await this.TryToHandle(
            async () =>
            {
                var result = await callback();
                return this.Ok(result);
            });
    }
}