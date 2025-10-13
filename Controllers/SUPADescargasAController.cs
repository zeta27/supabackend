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
    public class SUPADescargasAController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPADescargasAController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPADescargasA>>> GetSUPADescargasA()
        {
            return await _context.SUPADescargasA
                .Include(d => d.IdAreaDescargaNavigation)
                .Include(d => d.IdCatPeriodosNavigation)
                .Include(d => d.IdRegionDescargaNavigation)
                .Include(d => d.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPADescargasA>> GetSUPADescargasA(int id)
        {
            var descarga = await _context.SUPADescargasA
                .Include(d => d.IdAreaDescargaNavigation)
                .Include(d => d.IdCatPeriodosNavigation)
                .Include(d => d.IdRegionDescargaNavigation)
                .Include(d => d.IdSUPANavigation)
                .FirstOrDefaultAsync(d => d.IdDescargaA == id);

            if (descarga == null) return NotFound();
            return descarga;
        }

        [HttpPost]
        public async Task<ActionResult<SUPADescargasA>> PostSUPADescargasA(SUPADescargasAViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatPeriodos", viewModel.IdCatPeriodos),
                    new SqlParameter("@IdAreaDescarga", viewModel.IdAreaDescarga),
                    new SqlParameter("@IdRegionDescarga", viewModel.IdRegionDescarga),
                    new SqlParameter("@NombreEstudios", viewModel.NombreEstudios),
                    new SqlParameter("@InicioEstudios", viewModel.InicioEstudios),
                    new SqlParameter("@FinEstudios", viewModel.FinEstudios),
                    new SqlParameter("@Entrego", viewModel.Entrego),
                    new SqlParameter("@InstitucionEstudios", viewModel.InstitucionEstudios)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertDescargasA @IdSUPA, @IdCatPeriodos, @IdAreaDescarga, @IdRegionDescarga, @NombreEstudios, @InicioEstudios, @FinEstudios, @Entrego, @InstitucionEstudios",
                    parameters).FirstOrDefaultAsync();

                var descarga = await _context.SUPADescargasA
                    .Include(d => d.IdAreaDescargaNavigation)
                    .Include(d => d.IdCatPeriodosNavigation)
                    .Include(d => d.IdRegionDescargaNavigation)
                    .Include(d => d.IdSUPANavigation)
                    .FirstOrDefaultAsync(d => d.IdDescargaA == result);

                return CreatedAtAction(nameof(GetSUPADescargasA), new { id = result }, descarga);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la descarga académica: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPADescargasA(int id, SUPADescargasAViewModel viewModel)
        {
            if (viewModel.IdDescargaA.HasValue && id != viewModel.IdDescargaA.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdDescargaA", id),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatPeriodos", viewModel.IdCatPeriodos),
                    new SqlParameter("@IdAreaDescarga", viewModel.IdAreaDescarga),
                    new SqlParameter("@IdRegionDescarga", viewModel.IdRegionDescarga),
                    new SqlParameter("@NombreEstudios", viewModel.NombreEstudios),
                    new SqlParameter("@InicioEstudios", viewModel.InicioEstudios),
                    new SqlParameter("@FinEstudios", viewModel.FinEstudios),
                    new SqlParameter("@Entrego", viewModel.Entrego),
                    new SqlParameter("@InstitucionEstudios", viewModel.InstitucionEstudios)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateDescargasA @IdDescargaA, @IdSUPA, @IdCatPeriodos, @IdAreaDescarga, @IdRegionDescarga, @NombreEstudios, @InicioEstudios, @FinEstudios, @Entrego, @InstitucionEstudios",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la descarga académica: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPADescargasA(int id)
        {
            var descarga = await _context.SUPADescargasA.FindAsync(id);
            if (descarga == null) return NotFound();

            _context.SUPADescargasA.Remove(descarga);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
