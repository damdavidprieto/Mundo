using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;
using MundoAPI.Pages.TipoMoneda;

namespace MundoAPI.Tests;

public class TipoMonedaTests
{
    [Fact]
    public async Task Create_saves_the_currency_with_a_creation_date_and_redirects()
    {
        using var context = TestSupport.CreateContext();
        var page = new CrearTipoMonedaModel(context).WithPageContext();
        page.NuevoTipoMoneda = new TiposMoneda { Moneda = "Euro", Iso = "EUR", Simbolo = "EUR" };
        var before = DateTime.Now.AddMinutes(-1);

        var result = await page.OnPostAsync();

        var redirect = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/TipoMoneda/TipoMoneda", redirect.PageName);

        var saved = await context.TiposMonedas.SingleAsync();
        Assert.Equal("EUR", saved.Iso);
        Assert.True(saved.FechaCreacion >= before);
    }

    [Fact]
    public async Task Create_with_invalid_model_saves_nothing_and_redisplays_the_page()
    {
        using var context = TestSupport.CreateContext();
        var page = new CrearTipoMonedaModel(context).WithPageContext();
        page.NuevoTipoMoneda = new TiposMoneda { Iso = "EUR" };
        page.ModelState.AddModelError("NuevoTipoMoneda.Moneda", "required");

        var result = await page.OnPostAsync();

        Assert.IsType<PageResult>(result);
        Assert.Empty(context.TiposMonedas);
    }

    [Fact]
    public async Task List_returns_all_currencies()
    {
        using var context = TestSupport.CreateContext();
        context.TiposMonedas.AddRange(
            new TiposMoneda { Moneda = "Euro", Iso = "EUR" },
            new TiposMoneda { Moneda = "Yen", Iso = "JPY" });
        await context.SaveChangesAsync();

        var page = new TipoMonedaModel(context);
        await page.OnGetAsync();

        Assert.Equal(2, page.TiposMoneda.Count);
    }

    [Fact]
    public async Task Delete_removes_an_existing_currency()
    {
        using var context = TestSupport.CreateContext();
        var moneda = new TiposMoneda { Moneda = "Euro", Iso = "EUR" };
        context.TiposMonedas.Add(moneda);
        await context.SaveChangesAsync();

        var page = new TipoMonedaModel(context).WithPageContext();
        var result = await page.OnPostDeleteAsync(moneda.Id);

        Assert.IsType<RedirectToPageResult>(result);
        Assert.Empty(context.TiposMonedas);
    }

    [Fact]
    public async Task Delete_of_an_unknown_currency_returns_not_found()
    {
        using var context = TestSupport.CreateContext();
        var page = new TipoMonedaModel(context).WithPageContext();

        var result = await page.OnPostDeleteAsync(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
