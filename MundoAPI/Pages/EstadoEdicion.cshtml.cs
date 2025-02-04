using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Mundo.Models;
using Microsoft.EntityFrameworkCore;


namespace MundoAPI.Pages
{
    public class EstadoEdicionModel : PageModel
    {
        private readonly MundoContext _context;

        public EstadoEdicionModel(MundoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Estado Estado { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Estado = await _context.Estados
                .Include(e => e.IdContinenteNavigation)
                .Include(e => e.IdCapitalNavigation)
                .Include(e => e.IdTipoMonedaNavigation)
                .Include(e => e.IdTipoGobiernoNavigation)
                .Include(e => e.Provincia)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Estado == null)
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

            var estadoToUpdate = await _context.Estados.FindAsync(id);

            if (estadoToUpdate == null)
            {
                return NotFound();
            }

            // Actualizar los valores del estado
            estadoToUpdate.Nombre = Estado.Nombre;
            estadoToUpdate.Area = Estado.Area;
            estadoToUpdate.Iso = Estado.Iso;
            estadoToUpdate.IdCapitalNavigation = Estado.IdCapitalNavigation;

            // Guardar los cambios
            await _context.SaveChangesAsync();

            // Redirigir de vuelta a la vista de detalle
            return RedirectToPage("/EstadoDetalle", new { id = estadoToUpdate.Id });
        }
    }

}



