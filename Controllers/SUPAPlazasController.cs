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
    public class SUPAPlazasController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAPlazasController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAPlazas>>> GetSUPAPlazas()
        {
            return await _context.SUPAPlazas
                .Include(p => p.IdAreaPlazaNavigation)
                .Include(p => p.IdCatMotivosNavigation)
                .Include(p => p.IdRegionPlazaNavigation)
                .Include(p => p.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAPlazas>> GetSUPAPlazas(int id)
        {
            var plaza = await _context.SUPAPlazas
                .Include(p => p.IdAreaPlazaNavigation)
                .Include(p => p.IdCatMotivosNavigation)
                .Include(p => p.IdRegionPlazaNavigation)
                .Include(p => p.IdSUPANavigation)
                .FirstOrDefaultAsync(p => p.IdPlaza == id);

            if (plaza == null) return NotFound();
            return plaza;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAPlazas>> PostSUPAPlazas(SUPAPlazasViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@ClavePlaza", viewModel.ClavePlaza),
                    new SqlParameter("@InicioPlaza", viewModel.InicioPlaza),
                    new SqlParameter("@TerminoPlaza", viewModel.TerminoPlaza),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@UltimoOcupante", viewModel.UltimoOcupante),
                    new SqlParameter("@BajaPlaza", viewModel.BajaPlaza),
                    new SqlParameter("@FechaBajaPlaza", (object)viewModel.FechaBajaPlaza ?? DBNull.Value),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos),
                    new SqlParameter("@IdAreaPlaza", viewModel.IdAreaPlaza),
                    new SqlParameter("@IdRegionPlaza", viewModel.IdRegionPlaza)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertPlazas @ClavePlaza, @InicioPlaza, @TerminoPlaza, @IdSUPA, @UltimoOcupante, @BajaPlaza, @FechaBajaPlaza, @IdCatMotivos, @IdAreaPlaza, @IdRegionPlaza",
                    parameters).FirstOrDefaultAsync();

                var plaza = await _context.SUPAPlazas
                    .Include(p => p.IdAreaPlazaNavigation)
                    .Include(p => p.IdCatMotivosNavigation)
                    .Include(p => p.IdRegionPlazaNavigation)
                    .Include(p => p.IdSUPANavigation)
                    .FirstOrDefaultAsync(p => p.IdPlaza == result);

                return CreatedAtAction(nameof(GetSUPAPlazas), new { id = result }, plaza);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la plaza: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAPlazas(int id, SUPAPlazasViewModel viewModel)
        {
            if (viewModel.IdPlaza.HasValue && id != viewModel.IdPlaza.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdPlaza", id),
                    new SqlParameter("@ClavePlaza", viewModel.ClavePlaza),
                    new SqlParameter("@InicioPlaza", viewModel.InicioPlaza),
                    new SqlParameter("@TerminoPlaza", viewModel.TerminoPlaza),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@UltimoOcupante", viewModel.UltimoOcupante),
                    new SqlParameter("@BajaPlaza", viewModel.BajaPlaza),
                    new SqlParameter("@FechaBajaPlaza", (object)viewModel.FechaBajaPlaza ?? DBNull.Value),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos),
                    new SqlParameter("@IdAreaPlaza", viewModel.IdAreaPlaza),
                    new SqlParameter("@IdRegionPlaza", viewModel.IdRegionPlaza)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdatePlazas @IdPlaza, @ClavePlaza, @InicioPlaza, @TerminoPlaza, @IdSUPA, @UltimoOcupante, @BajaPlaza, @FechaBajaPlaza, @IdCatMotivos, @IdAreaPlaza, @IdRegionPlaza",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la plaza: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAPlazas(int id)
        {
            var plaza = await _context.SUPAPlazas.FindAsync(id);
            if (plaza == null) return NotFound();

            _context.SUPAPlazas.Remove(plaza);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
