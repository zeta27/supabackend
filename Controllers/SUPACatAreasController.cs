using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatAreasController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatAreasController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatAreas>>> GetSUPACatAreas()
        {
            return await _context.SUPACatAreas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatAreas>> GetSUPACatAreas(int id)
        {
            var area = await _context.SUPACatAreas
                .FirstOrDefaultAsync(a => a.IdCatAreas == id);

            if (area == null) return NotFound();
            return area;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatAreas>> PostSUPACatAreas([FromBody] SUPACatAreasRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Darea))
                return BadRequest("La descripción del área es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Darea", request.Darea)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatAreas @Darea",
                    parameters).FirstOrDefaultAsync();

                var area = await _context.SUPACatAreas
                    .FirstOrDefaultAsync(a => a.IdCatAreas == result);

                return CreatedAtAction(nameof(GetSUPACatAreas), new { id = result }, area);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el área: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatAreas(int id, [FromBody] SUPACatAreasRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Darea))
                return BadRequest("La descripción del área es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatAreas", id),
                    new SqlParameter("@Darea", request.Darea)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatAreas @IdCatAreas, @Darea",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el área: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatAreas(int id)
        {
            var area = await _context.SUPACatAreas.FindAsync(id);
            if (area == null) return NotFound();

            _context.SUPACatAreas.Remove(area);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatAreasRequest
    {
        public string Darea { get; set; } = null!;
    }
}
