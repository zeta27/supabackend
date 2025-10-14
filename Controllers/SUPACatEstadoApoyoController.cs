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
    public class SUPACatEstadoApoyoController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatEstadoApoyoController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatEstadoApoyo>>> GetSUPACatEstadoApoyo()
        {
            return await _context.SUPACatEstadoApoyo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatEstadoApoyo>> GetSUPACatEstadoApoyo(int id)
        {
            var estadoApoyo = await _context.SUPACatEstadoApoyo
                .FirstOrDefaultAsync(e => e.IdCatEstadoApoyo == id);

            if (estadoApoyo == null) return NotFound();
            return estadoApoyo;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatEstadoApoyo>> PostSUPACatEstadoApoyo(SUPACatEstadoApoyoViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DEstadoApoyo", viewModel.DEstadoApoyo)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatEstadoApoyo @DEstadoApoyo",
                    parameters).FirstOrDefaultAsync();

                var estadoApoyo = await _context.SUPACatEstadoApoyo
                    .FirstOrDefaultAsync(e => e.IdCatEstadoApoyo == result);

                return CreatedAtAction(nameof(GetSUPACatEstadoApoyo), new { id = result }, estadoApoyo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el estado de apoyo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatEstadoApoyo(int id, SUPACatEstadoApoyoViewModel viewModel)
        {
            if (viewModel.IdCatEstadoApoyo.HasValue && id != viewModel.IdCatEstadoApoyo.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatEstadoApoyo", id),
                    new SqlParameter("@DEstadoApoyo", viewModel.DEstadoApoyo)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatEstadoApoyo @IdCatEstadoApoyo, @DEstadoApoyo",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el estado de apoyo: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatEstadoApoyo(int id)
        {
            var estadoApoyo = await _context.SUPACatEstadoApoyo.FindAsync(id);
            if (estadoApoyo == null) return NotFound();

            _context.SUPACatEstadoApoyo.Remove(estadoApoyo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}