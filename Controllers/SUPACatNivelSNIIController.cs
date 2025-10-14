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
    public class SUPACatNivelSNIIController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatNivelSNIIController(SUPADbContext context)
        {
            _context = context;
        }

        // GET: api/SUPACatNivelSNII
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatNivelSNII>>> GetSUPACatNivelSNII()
        {
            return await _context.SUPACatNivelSNII.ToListAsync();
        }

        // GET: api/SUPACatNivelSNII/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatNivelSNII>> GetSUPACatNivelSNII(int id)
        {
            var nivelSNII = await _context.SUPACatNivelSNII
                .FirstOrDefaultAsync(n => n.IdCatNivelSNII == id);

            if (nivelSNII == null) return NotFound();
            return nivelSNII;
        }

        // POST: api/SUPACatNivelSNII
        [HttpPost]
        public async Task<ActionResult<SUPACatNivelSNII>> PostSUPACatNivelSNII(
            SUPACatNivelSNIIViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DNivelSNII", (object)viewModel.DNivelSNII ?? DBNull.Value)
                };

                // ✅ CORREGIDO: Solo se pasa @DNivelSNII, no @IdCatNivelSNII
                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatNivelSNII @DNivelSNII",
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

        // PUT: api/SUPACatNivelSNII/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatNivelSNII(
            int id,
            SUPACatNivelSNIIViewModel viewModel)
        {
            if (viewModel.IdCatNivelSNII.HasValue && id != viewModel.IdCatNivelSNII.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatNivelSNII", id),
                    new SqlParameter("@DNivelSNII", (object)viewModel.DNivelSNII ?? DBNull.Value)
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

        // DELETE: api/SUPACatNivelSNII/5
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
}