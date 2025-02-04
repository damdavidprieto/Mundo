using Mundo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MundoAPI.Pages.TipoRecurso
{
    public class EditarTipoRecursoModel : PageModel
    {
        private readonly MundoContext _context;

        public EditarTipoRecursoModel(MundoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RecursosNaturale Recurso { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recurso = await _context.RecursosNaturales
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Recurso == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recursoToUpdate = await _context.RecursosNaturales
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recursoToUpdate == null)
            {
                return NotFound();
            }

            // Solo se actualizan los campos permitidos
            recursoToUpdate.Abreviatura = Recurso.Abreviatura;
            recursoToUpdate.Renovable = Recurso.Renovable;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecursosNaturaleExists(recursoToUpdate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/TipoRecurso/TipoRecurso"); // Redirigir a la lista de recursos
        }

        private bool RecursosNaturaleExists(int id)
        {
            return _context.RecursosNaturales.Any(e => e.Id == id);
        }
    }
}
