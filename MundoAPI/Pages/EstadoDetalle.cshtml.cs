using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mundo.Models;

namespace MundoAPI.Pages
{
    public class EstadoDetalleModel : PageModel
    {
        private readonly MundoContext _context;

        public EstadoDetalleModel(MundoContext context)
        {
            _context = context;
        }

        // Propiedad para almacenar el estado cargado
        public Estado Estado { get; set; }

        // Método OnGet para obtener el estado por ID
        public void OnGet(int id)
        {
            Estado = _context.Estados
                              .Include(e => e.IdiomasEstados)
                                .ThenInclude(e=>e.IdIdiomaNavigation)
                              .Include(e => e.IdTipoMonedaNavigation)
                              .Include(e => e.IdTipoGobiernoNavigation)
                              .Include(e => e.IdCapitalNavigation)
                              .Include(e => e.IdContinenteNavigation)
                                .ThenInclude(e => e.IdHemisferioNavigation)
                              .Include(e => e.Provincia)
                                .ThenInclude(e=>e.ProvinciaResursos)
                                .ThenInclude(e=>e.IdRecursoNaturalNavigation)
                            .Include(e=>e.Provincia)
                                .ThenInclude(e => e.IdCapitalProvinciaNavigation)
                              .FirstOrDefault(e => e.Id == id); // Obtenemos el estado por su ID

            // Si no se encuentra el estado, se retorna la página con un mensaje de error
            if (Estado == null)
            {
                // Podrías redirigir a una página de error o mostrar un mensaje de que no se encontró el estado
                // return NotFound();
            }
        }
    }
}
