using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;

namespace MundoAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MundoContext _context;

        public IndexModel(MundoContext context)
        {
            _context = context;
        }
        public IList<Estado> Estados { get; set; }
        public void OnGet()
        {
            Estados = _context.Estados
                      .Include(e=>e.Provincia)
                        .ThenInclude(e => e.ProvinciaResursos)
                        .ThenInclude(e=>e.IdRecursoNaturalNavigation)
                      .Include(e => e.IdTipoMonedaNavigation)
                      .Include(e => e.IdTipoGobiernoNavigation)
                      .Include(e => e.IdCapitalNavigation) // Asegúrate de incluir la navegación de capital
                      .Include(e => e.IdContinenteNavigation)
                        .ThenInclude(e=>e.IdHemisferioNavigation)
                      .ToList();
        }
    }
}
