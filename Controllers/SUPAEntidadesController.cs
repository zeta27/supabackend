using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using supa.Models.ViewModels;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPAEntidadesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAEntidadesController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAEntidades>>> GetSUPAEntidades()
        {
            return await _context.SUPAEntidades
                .Include(e => e.IdCatEntidadesNavigation)
                .Include(e => e.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAEntidades>> GetSUPAEntidades(int id)
        {
            var entidad = await _context.SUPAEntidades
                .Include(e => e.IdCatEntidadesNavigation)
                .Include(e => e.IdSUPANavigation)
                .FirstOrDefaultAsync(e => e.IdEntidades == id);

            if (entidad == null) return NotFound();
            return entidad;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAEntidades>> PostSUPAEntidades(SUPAEntidadesViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatEntidades", viewModel.IdCatEntidades),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@FechaRegistro", (object)viewModel.FechaRegistro ?? DBNull.Value),
                    new SqlParameter("@UltimaEntidad", viewModel.UltimaEntidad)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertEntidades @IdCatEntidades, @IdSUPA, @FechaRegistro, @UltimaEntidad",
                    parameters).FirstOrDefaultAsync();

                var entidad = await _context.SUPAEntidades
                    .Include(e => e.IdCatEntidadesNavigation)
                    .Include(e => e.IdSUPANavigation)
                    .FirstOrDefaultAsync(e => e.IdEntidades == result);

                return CreatedAtAction(nameof(GetSUPAEntidades), new { id = result }, entidad);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la entidad: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAEntidades(int id, SUPAEntidadesViewModel viewModel)
        {
            if (viewModel.IdEntidades.HasValue && id != viewModel.IdEntidades.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdEntidades", id),
                    new SqlParameter("@IdCatEntidades", viewModel.IdCatEntidades),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@FechaRegistro", viewModel.FechaRegistro ?? DateTime.Now),
                    new SqlParameter("@UltimaEntidad", viewModel.UltimaEntidad)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateEntidades @IdEntidades, @IdCatEntidades, @IdSUPA, @FechaRegistro, @UltimaEntidad",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la entidad: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAEntidades(int id)
        {
            var entidad = await _context.SUPAEntidades.FindAsync(id);
            if (entidad == null) return NotFound();

            _context.SUPAEntidades.Remove(entidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
