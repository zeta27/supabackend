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
    public class SUPAApoyosEcoCAController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAApoyosEcoCAController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAApoyosEcoCA>>> GetSUPAApoyosEcoCA()
        {
            return await _context.SUPAApoyosEcoCA
                .Include(a => a.IdCANavigation)
                .Include(a => a.IdCatEstadoApoyoNavigation)
                .Include(a => a.IdCatTipoApoyoNavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAApoyosEcoCA>> GetSUPAApoyosEcoCA(int id)
        {
            var apoyo = await _context.SUPAApoyosEcoCA
                .Include(a => a.IdCANavigation)
                .Include(a => a.IdCatEstadoApoyoNavigation)
                .Include(a => a.IdCatTipoApoyoNavigation)
                .FirstOrDefaultAsync(a => a.IdApoyosEcoCA == id);

            if (apoyo == null) return NotFound();
            return apoyo;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAApoyosEcoCA>> PostSUPAApoyosEcoCA(SUPAApoyosEcoCAViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@InicioApoyo", viewModel.InicioApoyo),
                    new SqlParameter("@FinApoyo", viewModel.FinApoyo),
                    new SqlParameter("@IdCatEstadoApoyo", viewModel.IdCatEstadoApoyo),
                    new SqlParameter("@IdCatTipoApoyo", viewModel.IdCatTipoApoyo),
                    new SqlParameter("@MontoApoyo", viewModel.MontoApoyo),
                    new SqlParameter("@ObservacionesApoyo", (object)viewModel.ObservacionesApoyo ?? DBNull.Value),
                    new SqlParameter("@MontoEjercido", (object)viewModel.MontoEjercido ?? DBNull.Value),
                    new SqlParameter("@MontoComprobado", (object)viewModel.MontoComprobado ?? DBNull.Value),
                    new SqlParameter("@MontoDevuelto", (object)viewModel.MontoDevuelto ?? DBNull.Value),
                    new SqlParameter("@OficioConcFin", (object)viewModel.OficioConcFin ?? DBNull.Value),
                    new SqlParameter("@OficioConcAcad", (object)viewModel.OficioConcAcad ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertApoyosEcoCA @IdCA, @InicioApoyo, @FinApoyo, @IdCatEstadoApoyo, @IdCatTipoApoyo, @MontoApoyo, @ObservacionesApoyo, @MontoEjercido, @MontoComprobado, @MontoDevuelto, @OficioConcFin, @OficioConcAcad",
                    parameters).FirstOrDefaultAsync();

                var apoyo = await _context.SUPAApoyosEcoCA
                    .Include(a => a.IdCANavigation)
                    .Include(a => a.IdCatEstadoApoyoNavigation)
                    .Include(a => a.IdCatTipoApoyoNavigation)
                    .FirstOrDefaultAsync(a => a.IdApoyosEcoCA == result);

                return CreatedAtAction(nameof(GetSUPAApoyosEcoCA), new { id = result }, apoyo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el apoyo económico CA: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAApoyosEcoCA(int id, SUPAApoyosEcoCAViewModel viewModel)
        {
            if (viewModel.IdApoyosEcoCA.HasValue && id != viewModel.IdApoyosEcoCA.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdApoyosEcoCA", id),
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@InicioApoyo", viewModel.InicioApoyo),
                    new SqlParameter("@FinApoyo", viewModel.FinApoyo),
                    new SqlParameter("@IdCatEstadoApoyo", viewModel.IdCatEstadoApoyo),
                    new SqlParameter("@IdCatTipoApoyo", viewModel.IdCatTipoApoyo),
                    new SqlParameter("@MontoApoyo", viewModel.MontoApoyo),
                    new SqlParameter("@ObservacionesApoyo", (object)viewModel.ObservacionesApoyo ?? DBNull.Value),
                    new SqlParameter("@MontoEjercido", (object)viewModel.MontoEjercido ?? DBNull.Value),
                    new SqlParameter("@MontoComprobado", (object)viewModel.MontoComprobado ?? DBNull.Value),
                    new SqlParameter("@MontoDevuelto", (object)viewModel.MontoDevuelto ?? DBNull.Value),
                    new SqlParameter("@OficioConcFin", (object)viewModel.OficioConcFin ?? DBNull.Value),
                    new SqlParameter("@OficioConcAcad", (object)viewModel.OficioConcAcad ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateApoyosEcoCA @IdApoyosEcoCA, @IdCA, @InicioApoyo, @FinApoyo, @IdCatEstadoApoyo, @IdCatTipoApoyo, @MontoApoyo, @ObservacionesApoyo, @MontoEjercido, @MontoComprobado, @MontoDevuelto, @OficioConcFin, @OficioConcAcad",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el apoyo económico CA: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAApoyosEcoCA(int id)
        {
            var apoyo = await _context.SUPAApoyosEcoCA.FindAsync(id);
            if (apoyo == null) return NotFound();

            _context.SUPAApoyosEcoCA.Remove(apoyo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
