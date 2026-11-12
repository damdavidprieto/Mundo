using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;

namespace MundoAPI.Tests;

internal static class TestSupport
{
    /// <summary>Creates an isolated in-memory database per test.</summary>
    public static MundoContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MundoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new MundoContext(options);
    }

    /// <summary>Gives a PageModel the minimal context it needs (ModelState, HttpContext).</summary>
    public static T WithPageContext<T>(this T page) where T : PageModel
    {
        page.PageContext = new PageContext(
            new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()));
        return page;
    }
}
