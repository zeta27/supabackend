using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatTipoContratacionesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatTipoContratacionesController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatTipoContrataciones>>> GetSUPACatTipoContrataciones()
        {
            return await _context.SUPACatTipoContrataciones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatTipoContrataciones>> GetSUPACatTipoContrataciones(int id)
        {
            var tipoContratacion = await _context.SUPACatTipoContrataciones
                .FirstOrDefaultAsync(t => t.IdCatTipoContratacion == id);

            if (tipoContratacion == null) return NotFound();
            return tipoContratacion;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatTipoContrataciones>> PostSUPACatTipoContrataciones([FromBody] SUPACatTipoContratacionesRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DTipoContratacion))
                return BadRequest("La descripción del tipo de contratación es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DTipoContratacion", request.DTipoContratacion)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatTipoContrataciones @DTipoContratacion",
                    parameters).FirstOrDefaultAsync();

                var tipoContratacion = await _context.SUPACatTipoContrataciones
                    .FirstOrDefaultAsync(t => t.IdCatTipoContratacion == result);

                return CreatedAtAction(nameof(GetSUPACatTipoContrataciones), new { id = result }, tipoContratacion);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el tipo de contratación: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatTipoContrataciones(int id, [FromBody] SUPACatTipoContratacionesRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DTipoContratacion))
                return BadRequest("La descripción del tipo de contratación es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatTipoContratacion", id),
                    new SqlParameter("@DTipoContratacion", request.DTipoContratacion)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatTipoContrataciones @IdCatTipoContratacion, @DTipoContratacion",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el tipo de contratación: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatTipoContrataciones(int id)
        {
            var tipoContratacion = await _context.SUPACatTipoContrataciones.FindAsync(id);
            if (tipoContratacion == null) return NotFound();

            _context.SUPACatTipoContrataciones.Remove(tipoContratacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatTipoContratacionesRequest
    {
        public string DTipoContratacion { get; set; } = null!;
    }
}
