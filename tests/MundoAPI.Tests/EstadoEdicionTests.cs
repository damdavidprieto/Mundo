using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;
using MundoAPI.Pages;

namespace MundoAPI.Tests;

public class EstadoEdicionTests
{
    private static async Task<int> SeedEstadoAsync(MundoContext context)
    {
        var estado = new Estado { Nombre = "Francia", Iso = "FR", Area = 551695m, IdContinente = 1 };
        context.Estados.Add(estado);
        await context.SaveChangesAsync();
        return estado.Id;
    }

    [Fact]
    public async Task OnGet_returns_not_found_for_an_unknown_state()
    {
        using var context = TestSupport.CreateContext();
        var page = new EstadoEdicionModel(context).WithPageContext();

        var result = await page.OnGetAsync(42);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnGet_loads_the_requested_state()
    {
        using var context = TestSupport.CreateContext();
        var id = await SeedEstadoAsync(context);
        var page = new EstadoEdicionModel(context).WithPageContext();

        var result = await page.OnGetAsync(id);

        Assert.IsType<PageResult>(result);
        Assert.Equal("Francia", page.Estado.Nombre);
    }

    [Fact]
    public async Task OnPost_updates_name_area_and_iso_and_redirects_to_the_detail()
    {
        using var context = TestSupport.CreateContext();
        var id = await SeedEstadoAsync(context);
        var page = new EstadoEdicionModel(context).WithPageContext();
        page.Estado = new Estado { Nombre = "Republica Francesa", Area = 643801m, Iso = "FRA" };

        var result = await page.OnPostAsync(id);

        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/EstadoDetalle", redirect.PageName);
        Assert.Equal(id, redirect.RouteValues["id"]);

        var saved = await context.Estados.SingleAsync();
        Assert.Equal("Republica Francesa", saved.Nombre);
        Assert.Equal(643801m, saved.Area);
        Assert.Equal("FRA", saved.Iso);
    }

    [Fact]
    public async Task OnPost_returns_not_found_for_an_unknown_state()
    {
        using var context = TestSupport.CreateContext();
        var page = new EstadoEdicionModel(context).WithPageContext();
        page.Estado = new Estado { Nombre = "Nadie" };

        var result = await page.OnPostAsync(42);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task OnPost_with_invalid_model_does_not_modify_the_state()
    {
        using var context = TestSupport.CreateContext();
        var id = await SeedEstadoAsync(context);
        var page = new EstadoEdicionModel(context).WithPageContext();
        page.Estado = new Estado { Nombre = "Cambiado" };
        page.ModelState.AddModelError("Estado.Iso", "required");

        var result = await page.OnPostAsync(id);

        Assert.IsType<PageResult>(result);
        Assert.Equal("Francia", (await context.Estados.SingleAsync()).Nombre);
    }
}
