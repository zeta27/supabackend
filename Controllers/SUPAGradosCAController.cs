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
    public class SUPAGradosCAController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAGradosCAController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAGradosCA>>> GetSUPAGradosCA()
        {
            return await _context.SUPAGradosCA
                .Include(g => g.IdCANavigation)
                .Include(g => g.IdCatGradoCANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAGradosCA>> GetSUPAGradosCA(int id)
        {
            var grado = await _context.SUPAGradosCA
                .Include(g => g.IdCANavigation)
                .Include(g => g.IdCatGradoCANavigation)
                .FirstOrDefaultAsync(g => g.IdGradosCA == id);

            if (grado == null) return NotFound();
            return grado;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAGradosCA>> PostSUPAGradosCA(SUPAGradosCAViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@FechaObtencion", (object)viewModel.FechaObtencion ?? DBNull.Value),
                    new SqlParameter("@UltimoGradoCA", viewModel.UltimoGradoCA),
                    new SqlParameter("@IdCatGradoCA", viewModel.IdCatGradoCA)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertGradosCA @IdCA, @FechaObtencion, @UltimoGradoCA, @IdCatGradoCA",
                    parameters).FirstOrDefaultAsync();

                var grado = await _context.SUPAGradosCA
                    .Include(g => g.IdCANavigation)
                    .Include(g => g.IdCatGradoCANavigation)
                    .FirstOrDefaultAsync(g => g.IdGradosCA == result);

                return CreatedAtAction(nameof(GetSUPAGradosCA), new { id = result }, grado);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el grado CA: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAGradosCA(int id, SUPAGradosCAViewModel viewModel)
        {
            if (viewModel.IdGradosCA.HasValue && id != viewModel.IdGradosCA.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdGradosCA", id),
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@FechaObtencion", viewModel.FechaObtencion ?? DateTime.Now),
                    new SqlParameter("@UltimoGradoCA", viewModel.UltimoGradoCA),
                    new SqlParameter("@IdCatGradoCA", viewModel.IdCatGradoCA)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateGradosCA @IdGradosCA, @IdCA, @FechaObtencion, @UltimoGradoCA, @IdCatGradoCA",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el grado CA: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAGradosCA(int id)
        {
            var grado = await _context.SUPAGradosCA.FindAsync(id);
            if (grado == null) return NotFound();

            _context.SUPAGradosCA.Remove(grado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
