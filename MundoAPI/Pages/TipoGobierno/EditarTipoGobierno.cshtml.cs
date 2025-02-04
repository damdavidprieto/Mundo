using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Mundo.Models;

namespace MundoAPI.Pages.TipoGobierno
{
    public class EditarTipoGobiernoModel : PageModel
    {
        private readonly MundoContext _context;

        // Constructor para inyectar el DbContext
        public EditarTipoGobiernoModel(MundoContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TiposGobierno TipoGobierno { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TipoGobierno = await _context.TiposGobiernos.FindAsync(id);

            if (TipoGobierno == null)
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

            var tipoGobiernoToUpdate = await _context.TiposGobiernos.FindAsync(id);

            if (tipoGobiernoToUpdate == null)
            {
                return NotFound();
            }

            tipoGobiernoToUpdate.Descripcion = TipoGobierno.Descripcion;

            await _context.SaveChangesAsync();

            return RedirectToPage("/TipoGobierno/TipoGobierno"); // Redirigir a la lista o página principal
        }
    }
}
