using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatAreaDedicaController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatAreaDedicaController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatAreaDedica>>> GetSUPACatAreaDedica()
        {
            return await _context.SUPACatAreaDedica.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatAreaDedica>> GetSUPACatAreaDedica(int id)
        {
            var areaDedica = await _context.SUPACatAreaDedica
                .FirstOrDefaultAsync(a => a.IdCatAreaDedica == id);

            if (areaDedica == null) return NotFound();
            return areaDedica;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatAreaDedica>> PostSUPACatAreaDedica([FromBody] SUPACatAreaDedicaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DAreaDedica))
                return BadRequest("La descripción del área dedicada es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DAreaDedica", request.DAreaDedica)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatAreaDedica @DAreaDedica",
                    parameters).FirstOrDefaultAsync();

                var areaDedica = await _context.SUPACatAreaDedica
                    .FirstOrDefaultAsync(a => a.IdCatAreaDedica == result);

                return CreatedAtAction(nameof(GetSUPACatAreaDedica), new { id = result }, areaDedica);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el área dedicada: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatAreaDedica(int id, [FromBody] SUPACatAreaDedicaRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DAreaDedica))
                return BadRequest("La descripción del área dedicada es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatAreaDedica", id),
                    new SqlParameter("@DAreaDedica", request.DAreaDedica)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatAreaDedica @IdCatAreaDedica, @DAreaDedica",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el área dedicada: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatAreaDedica(int id)
        {
            var areaDedica = await _context.SUPACatAreaDedica.FindAsync(id);
            if (areaDedica == null) return NotFound();

            _context.SUPACatAreaDedica.Remove(areaDedica);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatAreaDedicaRequest
    {
        public string DAreaDedica { get; set; } = null!;
    }
}
