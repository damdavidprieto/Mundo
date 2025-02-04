using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;

namespace MundoAPI.Pages.TipoMoneda
{
    public class TipoMonedaModel : PageModel
    {
        private readonly MundoContext _context;

        public TipoMonedaModel(MundoContext context)
        {
            _context = context;
        }

        public IList<TiposMoneda> TiposMoneda { get; set; }

        public async Task OnGetAsync()
        {
            // Obtener todos los tipos de moneda
            TiposMoneda = await _context.TiposMonedas.ToListAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var tipoMoneda = await _context.TiposMonedas.FindAsync(id);
            if (tipoMoneda == null)
            {
                return NotFound();
            }

            _context.TiposMonedas.Remove(tipoMoneda);
            await _context.SaveChangesAsync();

            // Redirigir a la página de listado después de la eliminación
            return RedirectToPage();
        }
    }
}
