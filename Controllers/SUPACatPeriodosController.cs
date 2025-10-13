using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatPeriodosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatPeriodosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatPeriodos>>> GetSUPACatPeriodos()
        {
            return await _context.SUPACatPeriodos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatPeriodos>> GetSUPACatPeriodos(int id)
        {
            var periodo = await _context.SUPACatPeriodos
                .FirstOrDefaultAsync(p => p.IdCatPeriodos == id);

            if (periodo == null) return NotFound();
            return periodo;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatPeriodos>> PostSUPACatPeriodos([FromBody] SUPACatPeriodosRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DescripcionPeriodo", (object)request.DescripcionPeriodo ?? DBNull.Value),
                    new SqlParameter("@FechaInicio", (object)request.FechaInicio ?? DBNull.Value),
                    new SqlParameter("@FechaTermino", (object)request.FechaTermino ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatPeriodos @DescripcionPeriodo, @FechaInicio, @FechaTermino",
                    parameters).FirstOrDefaultAsync();

                var periodo = await _context.SUPACatPeriodos
                    .FirstOrDefaultAsync(p => p.IdCatPeriodos == result);

                return CreatedAtAction(nameof(GetSUPACatPeriodos), new { id = result }, periodo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el periodo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatPeriodos(int id, [FromBody] SUPACatPeriodosRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatPeriodos", id),
                    new SqlParameter("@DescripcionPeriodo", (object)request.DescripcionPeriodo ?? DBNull.Value),
                    new SqlParameter("@FechaInicio", (object)request.FechaInicio ?? DBNull.Value),
                    new SqlParameter("@FechaTermino", (object)request.FechaTermino ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatPeriodos @IdCatPeriodos, @DescripcionPeriodo, @FechaInicio, @FechaTermino",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el periodo: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatPeriodos(int id)
        {
            var periodo = await _context.SUPACatPeriodos.FindAsync(id);
            if (periodo == null) return NotFound();

            _context.SUPACatPeriodos.Remove(periodo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatPeriodosRequest
    {
        public string? DescripcionPeriodo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
    }
}
