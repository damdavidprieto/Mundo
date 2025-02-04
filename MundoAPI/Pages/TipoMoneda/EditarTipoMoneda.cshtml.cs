using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;

namespace MundoAPI.Pages.TipoMoneda
{
    public class EditarTipoMonedaModel : PageModel
    {
        private readonly MundoContext _context;

        public EditarTipoMonedaModel(MundoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TiposMoneda TipoMoneda { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TipoMoneda = await _context.TiposMonedas.FindAsync(id);

            if (TipoMoneda == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TipoMoneda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMonedaExists(TipoMoneda.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/TipoMoneda/TipoMoneda");
        }

        private bool TipoMonedaExists(int id)
        {
            return _context.TiposMonedas.Any(e => e.Id == id);
        }
    }
}
