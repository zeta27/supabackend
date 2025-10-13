using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatRegionController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatRegionController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatRegion>>> GetSUPACatRegion()
        {
            return await _context.SUPACatRegion.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatRegion>> GetSUPACatRegion(int id)
        {
            var region = await _context.SUPACatRegion
                .FirstOrDefaultAsync(r => r.IdCatRegion == id);

            if (region == null) return NotFound();
            return region;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatRegion>> PostSUPACatRegion([FromBody] SUPACatRegionRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatRegion", request.IdCatRegion),
                    new SqlParameter("@Dregion", (object)request.Dregion ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatRegion @IdCatRegion, @Dregion",
                    parameters).FirstOrDefaultAsync();

                var region = await _context.SUPACatRegion
                    .FirstOrDefaultAsync(r => r.IdCatRegion == result);

                return CreatedAtAction(nameof(GetSUPACatRegion), new { id = result }, region);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la región: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatRegion(int id, [FromBody] SUPACatRegionRequest request)
        {
            if (id != request.IdCatRegion)
                return BadRequest("El ID no coincide con el modelo");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatRegion", id),
                    new SqlParameter("@Dregion", (object)request.Dregion ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatRegion @IdCatRegion, @Dregion",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la región: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatRegion(int id)
        {
            var region = await _context.SUPACatRegion.FindAsync(id);
            if (region == null) return NotFound();

            _context.SUPACatRegion.Remove(region);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatRegionRequest
    {
        public int IdCatRegion { get; set; }
        public string? Dregion { get; set; }
    }
}
