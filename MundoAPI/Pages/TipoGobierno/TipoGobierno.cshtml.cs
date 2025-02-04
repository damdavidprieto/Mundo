using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;

namespace MundoAPI.Pages.TipoGobierno
{
    public class TipoGobiernoModel : PageModel
    {
        private readonly MundoContext _context;

        public TipoGobiernoModel(MundoContext context)
        {
            _context = context;
        }

        public IList<TiposGobierno> TiposGobiernos { get; set; }

        public async Task OnGetAsync()
        {
            TiposGobiernos = await _context.TiposGobiernos.ToListAsync();
        }
        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var tipoGobierno = await _context.TiposGobiernos.FindAsync(id);

            if (tipoGobierno != null)
            {
                _context.TiposGobiernos.Remove(tipoGobierno);
                await _context.SaveChangesAsync();
            }

            // Después de eliminar, redirigimos de nuevo a la página principal
            return RedirectToPage("/TipoGobierno/TipoGobierno");
        }
    }

}
