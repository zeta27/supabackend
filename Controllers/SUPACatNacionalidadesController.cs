using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatNacionalidadesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatNacionalidadesController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatNacionalidades>>> GetSUPACatNacionalidades()
        {
            return await _context.SUPACatNacionalidades.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatNacionalidades>> GetSUPACatNacionalidades(int id)
        {
            var nacionalidad = await _context.SUPACatNacionalidades
                .FirstOrDefaultAsync(n => n.IdCatNacionalidad == id);

            if (nacionalidad == null) return NotFound();
            return nacionalidad;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatNacionalidades>> PostSUPACatNacionalidades([FromBody] SUPACatNacionalidadesRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatNacionalidad", request.IdCatNacionalidad),
                    new SqlParameter("@DNacionalidad", (object)request.DNacionalidad ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatNacionalidades @IdCatNacionalidad, @DNacionalidad",
                    parameters).FirstOrDefaultAsync();

                var nacionalidad = await _context.SUPACatNacionalidades
                    .FirstOrDefaultAsync(n => n.IdCatNacionalidad == result);

                return CreatedAtAction(nameof(GetSUPACatNacionalidades), new { id = result }, nacionalidad);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la nacionalidad: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatNacionalidades(int id, [FromBody] SUPACatNacionalidadesRequest request)
        {
            if (id != request.IdCatNacionalidad)
                return BadRequest("El ID no coincide con el modelo");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatNacionalidad", id),
                    new SqlParameter("@DNacionalidad", (object)request.DNacionalidad ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatNacionalidades @IdCatNacionalidad, @DNacionalidad",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la nacionalidad: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatNacionalidades(int id)
        {
            var nacionalidad = await _context.SUPACatNacionalidades.FindAsync(id);
            if (nacionalidad == null) return NotFound();

            _context.SUPACatNacionalidades.Remove(nacionalidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatNacionalidadesRequest
    {
        public int IdCatNacionalidad { get; set; }
        public string? DNacionalidad { get; set; }
    }
}

