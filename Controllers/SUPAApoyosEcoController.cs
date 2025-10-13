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
    public class SUPAApoyosEcoController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAApoyosEcoController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAApoyosEco>>> GetSUPAApoyosEco()
        {
            return await _context.SUPAApoyosEco
                .Include(a => a.IdCatEstadoApoyoNavigation)
                .Include(a => a.IdCatTipoApoyoNavigation)
                .Include(a => a.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAApoyosEco>> GetSUPAApoyosEco(int id)
        {
            var apoyo = await _context.SUPAApoyosEco
                .Include(a => a.IdCatEstadoApoyoNavigation)
                .Include(a => a.IdCatTipoApoyoNavigation)
                .Include(a => a.IdSUPANavigation)
                .FirstOrDefaultAsync(a => a.IdApoyosEco == id);

            if (apoyo == null) return NotFound();
            return apoyo;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAApoyosEco>> PostSUPAApoyosEco(SUPAApoyosEcoViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@InicioApoyo", viewModel.InicioApoyo),
                    new SqlParameter("@FinApoyo", viewModel.FinApoyo),
                    new SqlParameter("@IdCatTipoApoyo", viewModel.IdCatTipoApoyo),
                    new SqlParameter("@MontoApoyo", viewModel.MontoApoyo),
                    new SqlParameter("@ObservacionesApoyo", (object)viewModel.ObservacionesApoyo ?? DBNull.Value),
                    new SqlParameter("@IdCatEstadoApoyo", viewModel.IdCatEstadoApoyo),
                    new SqlParameter("@MontoEjercido", (object)viewModel.MontoEjercido ?? DBNull.Value),
                    new SqlParameter("@MontoComprobado", (object)viewModel.MontoComprobado ?? DBNull.Value),
                    new SqlParameter("@MontoDevuelto", (object)viewModel.MontoDevuelto ?? DBNull.Value),
                    new SqlParameter("@OficioConcFIn", (object)viewModel.OficioConcFIn ?? DBNull.Value),
                    new SqlParameter("@OficioFinAcad", (object)viewModel.OficioFinAcad ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertApoyosEco @IdSUPA, @InicioApoyo, @FinApoyo, @IdCatTipoApoyo, @MontoApoyo, @ObservacionesApoyo, @IdCatEstadoApoyo, @MontoEjercido, @MontoComprobado, @MontoDevuelto, @OficioConcFIn, @OficioFinAcad",
                    parameters).FirstOrDefaultAsync();

                var apoyo = await _context.SUPAApoyosEco
                    .Include(a => a.IdCatEstadoApoyoNavigation)
                    .Include(a => a.IdCatTipoApoyoNavigation)
                    .Include(a => a.IdSUPANavigation)
                    .FirstOrDefaultAsync(a => a.IdApoyosEco == result);

                return CreatedAtAction(nameof(GetSUPAApoyosEco), new { id = result }, apoyo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el apoyo económico: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAApoyosEco(int id, SUPAApoyosEcoViewModel viewModel)
        {
            if (viewModel.IdApoyosEco.HasValue && id != viewModel.IdApoyosEco.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdApoyosEco", id),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@InicioApoyo", viewModel.InicioApoyo),
                    new SqlParameter("@FinApoyo", viewModel.FinApoyo),
                    new SqlParameter("@IdCatTipoApoyo", viewModel.IdCatTipoApoyo),
                    new SqlParameter("@MontoApoyo", viewModel.MontoApoyo),
                    new SqlParameter("@ObservacionesApoyo", (object)viewModel.ObservacionesApoyo ?? DBNull.Value),
                    new SqlParameter("@IdCatEstadoApoyo", viewModel.IdCatEstadoApoyo),
                    new SqlParameter("@MontoEjercido", (object)viewModel.MontoEjercido ?? DBNull.Value),
                    new SqlParameter("@MontoComprobado", (object)viewModel.MontoComprobado ?? DBNull.Value),
                    new SqlParameter("@MontoDevuelto", (object)viewModel.MontoDevuelto ?? DBNull.Value),
                    new SqlParameter("@OficioConcFIn", (object)viewModel.OficioConcFIn ?? DBNull.Value),
                    new SqlParameter("@OficioFinAcad", (object)viewModel.OficioFinAcad ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateApoyosEco @IdApoyosEco, @IdSUPA, @InicioApoyo, @FinApoyo, @IdCatTipoApoyo, @MontoApoyo, @ObservacionesApoyo, @IdCatEstadoApoyo, @MontoEjercido, @MontoComprobado, @MontoDevuelto, @OficioConcFIn, @OficioFinAcad",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el apoyo económico: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAApoyosEco(int id)
        {
            var apoyo = await _context.SUPAApoyosEco.FindAsync(id);
            if (apoyo == null) return NotFound();

            _context.SUPAApoyosEco.Remove(apoyo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
