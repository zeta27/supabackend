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
    public class SUPACatRegionController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatRegionController(SUPADbContext context)
        {
            _context = context;
        }

        // GET: api/SUPACatRegion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatRegion>>> GetSUPACatRegion()
        {
            return await _context.SUPACatRegion.ToListAsync();
        }

        // GET: api/SUPACatRegion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatRegion>> GetSUPACatRegion(int id)
        {
            var region = await _context.SUPACatRegion
                .FirstOrDefaultAsync(r => r.IdCatRegion == id);

            if (region == null) return NotFound();
            return region;
        }

        // POST: api/SUPACatRegion
        [HttpPost]
        public async Task<ActionResult<SUPACatRegion>> PostSUPACatRegion(
            SUPACatRegionViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Dregion", (object)viewModel.Dregion ?? DBNull.Value)
                };

                // ✅ CORREGIDO: Solo se pasa @Dregion, no @IdCatRegion
                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatRegion @Dregion",
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

        // PUT: api/SUPACatRegion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatRegion(
            int id,
            SUPACatRegionViewModel viewModel)
        {
            if (viewModel.IdCatRegion.HasValue && id != viewModel.IdCatRegion.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatRegion", id),
                    new SqlParameter("@Dregion", (object)viewModel.Dregion ?? DBNull.Value)
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

        // DELETE: api/SUPACatRegion/5
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
}