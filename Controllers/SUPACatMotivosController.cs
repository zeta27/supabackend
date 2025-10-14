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
    public class SUPACatMotivosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatMotivosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatMotivos>>> GetSUPACatMotivos()
        {
            return await _context.SUPACatMotivos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatMotivos>> GetSUPACatMotivos(int id)
        {
            var motivo = await _context.SUPACatMotivos
                .FirstOrDefaultAsync(m => m.IdCatMotivos == id);

            if (motivo == null) return NotFound();
            return motivo;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatMotivos>> PostSUPACatMotivos(SUPACatMotivosViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DMotivos", viewModel.DMotivos)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatMotivos @DMotivos",
                    parameters).FirstOrDefaultAsync();

                var motivo = await _context.SUPACatMotivos
                    .FirstOrDefaultAsync(m => m.IdCatMotivos == result);

                return CreatedAtAction(nameof(GetSUPACatMotivos), new { id = result }, motivo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el motivo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatMotivos(int id, SUPACatMotivosViewModel viewModel)
        {
            if (viewModel.IdCatMotivos.HasValue && id != viewModel.IdCatMotivos.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatMotivos", id),
                    new SqlParameter("@DMotivos", viewModel.DMotivos)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatMotivos @IdCatMotivos, @DMotivos",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el motivo: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatMotivos(int id)
        {
            var motivo = await _context.SUPACatMotivos.FindAsync(id);
            if (motivo == null) return NotFound();

            _context.SUPACatMotivos.Remove(motivo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}