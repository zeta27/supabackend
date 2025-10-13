using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatNivelSNIIController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatNivelSNIIController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatNivelSNII>>> GetSUPACatNivelSNII()
        {
            return await _context.SUPACatNivelSNII.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatNivelSNII>> GetSUPACatNivelSNII(int id)
        {
            var nivelSNII = await _context.SUPACatNivelSNII
                .FirstOrDefaultAsync(n => n.IdCatNivelSNII == id);

            if (nivelSNII == null) return NotFound();
            return nivelSNII;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatNivelSNII>> PostSUPACatNivelSNII([FromBody] SUPACatNivelSNIIRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatNivelSNII", request.IdCatNivelSNII),
                    new SqlParameter("@DNivelSNII", (object)request.DNivelSNII ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatNivelSNII @IdCatNivelSNII, @DNivelSNII",
                    parameters).FirstOrDefaultAsync();

                var nivelSNII = await _context.SUPACatNivelSNII
                    .FirstOrDefaultAsync(n => n.IdCatNivelSNII == result);

                return CreatedAtAction(nameof(GetSUPACatNivelSNII), new { id = result }, nivelSNII);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el nivel SNII: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatNivelSNII(int id, [FromBody] SUPACatNivelSNIIRequest request)
        {
            if (id != request.IdCatNivelSNII)
                return BadRequest("El ID no coincide con el modelo");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatNivelSNII", id),
                    new SqlParameter("@DNivelSNII", (object)request.DNivelSNII ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatNivelSNII @IdCatNivelSNII, @DNivelSNII",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el nivel SNII: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatNivelSNII(int id)
        {
            var nivelSNII = await _context.SUPACatNivelSNII.FindAsync(id);
            if (nivelSNII == null) return NotFound();

            _context.SUPACatNivelSNII.Remove(nivelSNII);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatNivelSNIIRequest
    {
        public int IdCatNivelSNII { get; set; }
        public string? DNivelSNII { get; set; }
    }
}
