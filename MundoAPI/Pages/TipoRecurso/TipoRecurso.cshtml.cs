using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;

namespace MundoAPI.Pages.TipoRecurso
{
    public class TipoRecursoModel : PageModel
    {
        private readonly MundoContext _context;

        public TipoRecursoModel(MundoContext context)
        {
            _context = context;
        }

        public IList<RecursosNaturale> Recursos { get; set; }

        public async Task OnGetAsync()
        {
            Recursos = await _context.RecursosNaturales.ToListAsync();
        }

        // Acción de eliminar recurso
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var recurso = await _context.RecursosNaturales.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }

            _context.RecursosNaturales.Remove(recurso);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
