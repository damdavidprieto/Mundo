using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mundo.Models;

namespace MundoAPI.Pages.TipoMoneda
{
    public class CrearTipoMonedaModel : PageModel
    {
        private readonly MundoContext _context;

        public CrearTipoMonedaModel(MundoContext context)
        {
            _context = context;
        }

        // Propiedad para recibir el tipo de moneda desde el formulario
        [BindProperty]
        public TiposMoneda NuevoTipoMoneda { get; set; }

        // Acción GET para cargar la página
        public void OnGet()
        {
        }

        // Acción POST para guardar el nuevo tipo de moneda
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Asignamos la fecha de creación como la fecha actual
                NuevoTipoMoneda.FechaCreacion = DateTime.Now;

                // Guardamos el nuevo tipo de moneda en la base de datos
                _context.TiposMonedas.Add(NuevoTipoMoneda);
                await _context.SaveChangesAsync();

                // Redirigir al listado de tipos de moneda después de guardar
                return RedirectToPage("/TipoMoneda/TipoMoneda");
            }

            // Si algo falla, regresa a la página de creación
            return Page();
        }
    }
}
