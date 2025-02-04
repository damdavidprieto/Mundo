using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mundo.Models;

namespace MundoAPI.Pages.TipoGobierno
{
    public class CrearTioGobiernoModel : PageModel
    {
        private readonly MundoContext _context;

        // Constructor para inyectar el DbContext
        public CrearTioGobiernoModel(MundoContext context)
        {
            _context = context;
        }

        // Propiedad que será usada en la vista
        [BindProperty]
        public TiposGobierno NuevoTipoGobierno { get; set; }

        public void OnGet()
        {
            // Este método se usa para mostrar la página inicial, puedes agregar algo de lógica si es necesario
        }

        // Método para manejar la solicitud POST cuando el formulario es enviado
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Agregar el nuevo tipo de gobierno al contexto
            _context.TiposGobiernos.Add(NuevoTipoGobierno);
            _context.SaveChanges();

            // Redirigir a una página de éxito o lista
            return RedirectToPage("/TipoGobierno/TipoGobierno"); // O cualquier página a la que desees redirigir
        }
    }
}
