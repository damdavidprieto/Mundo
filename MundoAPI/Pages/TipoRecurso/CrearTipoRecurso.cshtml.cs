using Mundo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MundoAPI.Pages.TipoRecurso
{
    public class CrearTipoRecursoModel : PageModel
    {
        private readonly MundoContext _context;

        public CrearTipoRecursoModel(MundoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RecursosNaturale Recurso { get; set; }

        public void OnGet()
        {
            // Se podría poner lógica aquí si es necesario para la página de carga
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RecursosNaturales.Add(Recurso);
            await _context.SaveChangesAsync();

            return RedirectToPage("/TipoRecurso/TipoRecurso"); // Redirigir a la lista de recursos
        }
    }
}

