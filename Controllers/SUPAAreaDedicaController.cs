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
    public class SUPAAreaDedicaController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAAreaDedicaController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAAreaDedica>>> GetSUPAAreaDedica()
        {
            return await _context.SUPAAreaDedica
                .Include(a => a.IdCatAreaDedicaNavigation)
                .Include(a => a.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAAreaDedica>> GetSUPAAreaDedica(int id)
        {
            var area = await _context.SUPAAreaDedica
                .Include(a => a.IdCatAreaDedicaNavigation)
                .Include(a => a.IdSUPANavigation)
                .FirstOrDefaultAsync(a => a.IdAreaDedica == id);

            if (area == null) return NotFound();
            return area;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAAreaDedica>> PostSUPAAreaDedica(SUPAAreaDedicaViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatAreaDedica", viewModel.IdCatAreaDedica),
                    new SqlParameter("@FechaRegistro", (object)viewModel.FechaRegistro ?? DBNull.Value),
                    new SqlParameter("@UltimaAreaDedica", viewModel.UltimaAreaDedica)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertAreaDedica @IdSUPA, @IdCatAreaDedica, @FechaRegistro, @UltimaAreaDedica",
                    parameters).FirstOrDefaultAsync();

                var area = await _context.SUPAAreaDedica
                    .Include(a => a.IdCatAreaDedicaNavigation)
                    .Include(a => a.IdSUPANavigation)
                    .FirstOrDefaultAsync(a => a.IdAreaDedica == result);

                return CreatedAtAction(nameof(GetSUPAAreaDedica), new { id = result }, area);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el área dedicada: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAAreaDedica(int id, SUPAAreaDedicaViewModel viewModel)
        {
            if (viewModel.IdAreaDedica.HasValue && id != viewModel.IdAreaDedica.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdAreaDedica", id),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatAreaDedica", viewModel.IdCatAreaDedica),
                    new SqlParameter("@FechaRegistro", viewModel.FechaRegistro ?? DateTime.Now),
                    new SqlParameter("@UltimaAreaDedica", viewModel.UltimaAreaDedica)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateAreaDedica @IdAreaDedica, @IdSUPA, @IdCatAreaDedica, @FechaRegistro, @UltimaAreaDedica",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el área dedicada: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAAreaDedica(int id)
        {
            var area = await _context.SUPAAreaDedica.FindAsync(id);
            if (area == null) return NotFound();

            _context.SUPAAreaDedica.Remove(area);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
