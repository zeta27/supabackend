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
    public class SUPAContratacionesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAContratacionesController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAContrataciones>>> GetSUPAContrataciones()
        {
            return await _context.SUPAContrataciones
                .Include(c => c.IdCatTempContratacionNavigation)
                .Include(c => c.IdCatTipoContratacionNavigation)
                .Include(c => c.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAContrataciones>> GetSUPAContrataciones(int id)
        {
            var contratacion = await _context.SUPAContrataciones
                .Include(c => c.IdCatTempContratacionNavigation)
                .Include(c => c.IdCatTipoContratacionNavigation)
                .Include(c => c.IdSUPANavigation)
                .FirstOrDefaultAsync(c => c.IdContrataciones == id);

            if (contratacion == null) return NotFound();
            return contratacion;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAContrataciones>> PostSUPAContrataciones(SUPAContratacionesViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatTipoContratacion", viewModel.IdCatTipoContratacion),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatTempContratacion", viewModel.IdCatTempContratacion),
                    new SqlParameter("@InicioContratacion", viewModel.InicioContratacion),
                    new SqlParameter("@TerminoContratacion", (object)viewModel.TerminoContratacion ?? DBNull.Value),
                    new SqlParameter("@DocSoporte", (object)viewModel.DocSoporte ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertContrataciones @IdCatTipoContratacion, @IdSUPA, @IdCatTempContratacion, @InicioContratacion, @TerminoContratacion, @DocSoporte",
                    parameters).FirstOrDefaultAsync();

                var contratacion = await _context.SUPAContrataciones
                    .Include(c => c.IdCatTempContratacionNavigation)
                    .Include(c => c.IdCatTipoContratacionNavigation)
                    .Include(c => c.IdSUPANavigation)
                    .FirstOrDefaultAsync(c => c.IdContrataciones == result);

                return CreatedAtAction(nameof(GetSUPAContrataciones), new { id = result }, contratacion);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la contratación: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAContrataciones(int id, SUPAContratacionesViewModel viewModel)
        {
            if (viewModel.IdContrataciones.HasValue && id != viewModel.IdContrataciones.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdContrataciones", id),
                    new SqlParameter("@IdCatTipoContratacion", viewModel.IdCatTipoContratacion),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatTempContratacion", viewModel.IdCatTempContratacion),
                    new SqlParameter("@InicioContratacion", viewModel.InicioContratacion),
                    new SqlParameter("@TerminoContratacion", (object)viewModel.TerminoContratacion ?? DBNull.Value),
                    new SqlParameter("@DocSoporte", (object)viewModel.DocSoporte ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateContrataciones @IdContrataciones, @IdCatTipoContratacion, @IdSUPA, @IdCatTempContratacion, @InicioContratacion, @TerminoContratacion, @DocSoporte",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la contratación: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAContrataciones(int id)
        {
            var contratacion = await _context.SUPAContrataciones.FindAsync(id);
            if (contratacion == null) return NotFound();

            _context.SUPAContrataciones.Remove(contratacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
