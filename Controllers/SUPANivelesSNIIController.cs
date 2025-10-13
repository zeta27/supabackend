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
    public class SUPANivelesSNIIController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPANivelesSNIIController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPANivelesSNII>>> GetSUPANivelesSNII()
        {
            return await _context.SUPANivelesSNII
                .Include(n => n.IdCatNivelSNIINavigation)
                .Include(n => n.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPANivelesSNII>> GetSUPANivelesSNII(int id)
        {
            var nivel = await _context.SUPANivelesSNII
                .Include(n => n.IdCatNivelSNIINavigation)
                .Include(n => n.IdSUPANavigation)
                .FirstOrDefaultAsync(n => n.IdNivelesSNII == id);

            if (nivel == null) return NotFound();
            return nivel;
        }

        [HttpPost]
        public async Task<ActionResult<SUPANivelesSNII>> PostSUPANivelesSNII(SUPANivelesSNIIViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@FechaObtencion", (object)viewModel.FechaObtencion ?? DBNull.Value),
                    new SqlParameter("@UltimoNivel", viewModel.UltimoNivel),
                    new SqlParameter("@IdCatNivelSNII", viewModel.IdCatNivelSNII)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertNivelesSNII @IdSUPA, @FechaObtencion, @UltimoNivel, @IdCatNivelSNII",
                    parameters).FirstOrDefaultAsync();

                var nivel = await _context.SUPANivelesSNII
                    .Include(n => n.IdCatNivelSNIINavigation)
                    .Include(n => n.IdSUPANavigation)
                    .FirstOrDefaultAsync(n => n.IdNivelesSNII == result);

                return CreatedAtAction(nameof(GetSUPANivelesSNII), new { id = result }, nivel);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el nivel SNII: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPANivelesSNII(int id, SUPANivelesSNIIViewModel viewModel)
        {
            if (viewModel.IdNivelesSNII.HasValue && id != viewModel.IdNivelesSNII.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdNivelesSNII", id),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@FechaObtencion", (object)viewModel.FechaObtencion ?? DBNull.Value),
                    new SqlParameter("@UltimoNivel", viewModel.UltimoNivel),
                    new SqlParameter("@IdCatNivelSNII", viewModel.IdCatNivelSNII)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateNivelesSNII @IdNivelesSNII, @IdSUPA, @FechaObtencion, @UltimoNivel, @IdCatNivelSNII",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el nivel SNII: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPANivelesSNII(int id)
        {
            var nivel = await _context.SUPANivelesSNII.FindAsync(id);
            if (nivel == null) return NotFound();

            _context.SUPANivelesSNII.Remove(nivel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
