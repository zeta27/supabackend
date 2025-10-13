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
    public class SUPACALGCSController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACALGCSController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACALGCS>>> GetSUPACALGCS()
        {
            return await _context.SUPACALGCS
                .Include(c => c.IdCANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACALGCS>> GetSUPACALGCS(int id)
        {
            var calgcs = await _context.SUPACALGCS
                .Include(c => c.IdCANavigation)
                .FirstOrDefaultAsync(c => c.IdCALGCS == id);

            if (calgcs == null) return NotFound();
            return calgcs;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACALGCS>> PostSUPACALGCS(SUPACALGCSViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@LGC1", viewModel.LGC1),
                    new SqlParameter("@LGC2", (object)viewModel.LGC2 ?? DBNull.Value),
                    new SqlParameter("@LGC3", (object)viewModel.LGC3 ?? DBNull.Value),
                    new SqlParameter("@LGC4", (object)viewModel.LGC4 ?? DBNull.Value),
                    new SqlParameter("@LGC5", (object)viewModel.LGC5 ?? DBNull.Value),
                    new SqlParameter("@LGC6", (object)viewModel.LGC6 ?? DBNull.Value),
                    new SqlParameter("@FechaRegistro", (object)viewModel.FechaRegistro ?? DBNull.Value),
                    new SqlParameter("@UltimasLineas", viewModel.UltimasLineas)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCALGCS @IdCA, @LGC1, @LGC2, @LGC3, @LGC4, @LGC5, @LGC6, @FechaRegistro, @UltimasLineas",
                    parameters).FirstOrDefaultAsync();

                var calgcs = await _context.SUPACALGCS
                    .Include(c => c.IdCANavigation)
                    .FirstOrDefaultAsync(c => c.IdCALGCS == result);

                return CreatedAtAction(nameof(GetSUPACALGCS), new { id = result }, calgcs);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el CALGCS: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACALGCS(int id, SUPACALGCSViewModel viewModel)
        {
            if (viewModel.IdCALGCS.HasValue && id != viewModel.IdCALGCS.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCALGCS", id),
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@LGC1", viewModel.LGC1),
                    new SqlParameter("@LGC2", (object)viewModel.LGC2 ?? DBNull.Value),
                    new SqlParameter("@LGC3", (object)viewModel.LGC3 ?? DBNull.Value),
                    new SqlParameter("@LGC4", (object)viewModel.LGC4 ?? DBNull.Value),
                    new SqlParameter("@LGC5", (object)viewModel.LGC5 ?? DBNull.Value),
                    new SqlParameter("@LGC6", (object)viewModel.LGC6 ?? DBNull.Value),
                    new SqlParameter("@FechaRegistro", viewModel.FechaRegistro ?? DateTime.Now),
                    new SqlParameter("@UltimasLineas", viewModel.UltimasLineas)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCALGCS @IdCALGCS, @IdCA, @LGC1, @LGC2, @LGC3, @LGC4, @LGC5, @LGC6, @FechaRegistro, @UltimasLineas",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el CALGCS: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACALGCS(int id)
        {
            var calgcs = await _context.SUPACALGCS.FindAsync(id);
            if (calgcs == null) return NotFound();

            _context.SUPACALGCS.Remove(calgcs);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
