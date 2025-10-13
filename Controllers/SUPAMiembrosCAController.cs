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
    public class SUPAMiembrosCAController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAMiembrosCAController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAMiembrosCA>>> GetSUPAMiembrosCA()
        {
            return await _context.SUPAMiembrosCA
                .Include(m => m.IdCANavigation)
                .Include(m => m.IdCatMotivosNavigation)
                .Include(m => m.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAMiembrosCA>> GetSUPAMiembrosCA(int id)
        {
            var miembro = await _context.SUPAMiembrosCA
                .Include(m => m.IdCANavigation)
                .Include(m => m.IdCatMotivosNavigation)
                .Include(m => m.IdSUPANavigation)
                .FirstOrDefaultAsync(m => m.IdMiembrosCA == id);

            if (miembro == null) return NotFound();
            return miembro;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAMiembrosCA>> PostSUPAMiembrosCA(SUPAMiembrosCAViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdMiembrosCA", viewModel.IdMiembrosCA),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@FechaAlta", (object)viewModel.FechaAlta ?? DBNull.Value),
                    new SqlParameter("@FechaBaja", (object)viewModel.FechaBaja ?? DBNull.Value),
                    new SqlParameter("@UltimoRegistro", viewModel.UltimoRegistro),
                    new SqlParameter("@Baja", viewModel.Baja),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos),
                    new SqlParameter("@ObservacionesBaja", (object)viewModel.ObservacionesBaja ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertMiembrosCA @IdMiembrosCA, @IdSUPA, @IdCA, @FechaAlta, @FechaBaja, @UltimoRegistro, @Baja, @IdCatMotivos, @ObservacionesBaja",
                    parameters).FirstOrDefaultAsync();

                var miembro = await _context.SUPAMiembrosCA
                    .Include(m => m.IdCANavigation)
                    .Include(m => m.IdCatMotivosNavigation)
                    .Include(m => m.IdSUPANavigation)
                    .FirstOrDefaultAsync(m => m.IdMiembrosCA == result);

                return CreatedAtAction(nameof(GetSUPAMiembrosCA), new { id = result }, miembro);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el miembro CA: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAMiembrosCA(int id, SUPAMiembrosCAViewModel viewModel)
        {
            if (id != viewModel.IdMiembrosCA)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdMiembrosCA", id),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@FechaAlta", (object)viewModel.FechaAlta ?? DBNull.Value),
                    new SqlParameter("@FechaBaja", (object)viewModel.FechaBaja ?? DBNull.Value),
                    new SqlParameter("@UltimoRegistro", viewModel.UltimoRegistro),
                    new SqlParameter("@Baja", viewModel.Baja),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos),
                    new SqlParameter("@ObservacionesBaja", (object)viewModel.ObservacionesBaja ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateMiembrosCA @IdMiembrosCA, @IdSUPA, @IdCA, @FechaAlta, @FechaBaja, @UltimoRegistro, @Baja, @IdCatMotivos, @ObservacionesBaja",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el miembro CA: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAMiembrosCA(int id)
        {
            var miembro = await _context.SUPAMiembrosCA.FindAsync(id);
            if (miembro == null) return NotFound();

            _context.SUPAMiembrosCA.Remove(miembro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
