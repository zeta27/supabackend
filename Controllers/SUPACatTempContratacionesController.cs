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
    public class SUPACatTempContratacionesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatTempContratacionesController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatTempContrataciones>>> GetSUPACatTempContrataciones()
        {
            return await _context.SUPACatTempContrataciones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatTempContrataciones>> GetSUPACatTempContrataciones(int id)
        {
            var tempContratacion = await _context.SUPACatTempContrataciones
                .FirstOrDefaultAsync(t => t.IdCatTempContratacion == id);

            if (tempContratacion == null) return NotFound();
            return tempContratacion;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatTempContrataciones>> PostSUPACatTempContrataciones(SUPACatTempContratacionesViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DTempContratacion", viewModel.DTempContratacion)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatTempContrataciones @DTempContratacion",
                    parameters).FirstOrDefaultAsync();

                var tempContratacion = await _context.SUPACatTempContrataciones
                    .FirstOrDefaultAsync(t => t.IdCatTempContratacion == result);

                return CreatedAtAction(nameof(GetSUPACatTempContrataciones), new { id = result }, tempContratacion);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la temporalidad de contratación: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatTempContrataciones(int id, SUPACatTempContratacionesViewModel viewModel)
        {
            if (viewModel.IdCatTempContratacion.HasValue && id != viewModel.IdCatTempContratacion.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatTempContratacion", id),
                    new SqlParameter("@DTempContratacion", viewModel.DTempContratacion)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatTempContrataciones @IdCatTempContratacion, @DTempContratacion",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la temporalidad de contratación: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatTempContrataciones(int id)
        {
            var tempContratacion = await _context.SUPACatTempContrataciones.FindAsync(id);
            if (tempContratacion == null) return NotFound();

            _context.SUPACatTempContrataciones.Remove(tempContratacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}