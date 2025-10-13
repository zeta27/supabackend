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
    public class SUPACatEntidadesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatEntidadesController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatEntidades>>> GetSUPACatEntidades()
        {
            return await _context.SUPACatEntidades
                .Include(e => e.IdCatAreasNavigation)
                .Include(e => e.IdCatRegionNavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatEntidades>> GetSUPACatEntidades(int id)
        {
            var entidad = await _context.SUPACatEntidades
                .Include(e => e.IdCatAreasNavigation)
                .Include(e => e.IdCatRegionNavigation)
                .FirstOrDefaultAsync(e => e.IdCatEntidades == id);

            if (entidad == null) return NotFound();
            return entidad;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatEntidades>> PostSUPACatEntidades(SUPACatEntidadesViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Dentidad", viewModel.Dentidad),
                    new SqlParameter("@IdCatAreas", viewModel.IdCatAreas),
                    new SqlParameter("@IdCatRegion", viewModel.IdCatRegion),
                    new SqlParameter("@IdentidadUV", viewModel.IdentidadUV)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatEntidades @Dentidad, @IdCatAreas, @IdCatRegion, @IdentidadUV",
                    parameters).FirstOrDefaultAsync();

                var entidad = await _context.SUPACatEntidades
                    .Include(e => e.IdCatAreasNavigation)
                    .Include(e => e.IdCatRegionNavigation)
                    .FirstOrDefaultAsync(e => e.IdCatEntidades == result);

                return CreatedAtAction(nameof(GetSUPACatEntidades), new { id = result }, entidad);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la entidad: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatEntidades(int id, SUPACatEntidadesViewModel viewModel)
        {
            if (viewModel.IdCatEntidades.HasValue && id != viewModel.IdCatEntidades.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatEntidades", id),
                    new SqlParameter("@Dentidad", viewModel.Dentidad),
                    new SqlParameter("@IdCatAreas", viewModel.IdCatAreas),
                    new SqlParameter("@IdCatRegion", viewModel.IdCatRegion),
                    new SqlParameter("@IdentidadUV", viewModel.IdentidadUV)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatEntidades @IdCatEntidades, @Dentidad, @IdCatAreas, @IdCatRegion, @IdentidadUV",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la entidad: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatEntidades(int id)
        {
            var entidad = await _context.SUPACatEntidades.FindAsync(id);
            if (entidad == null) return NotFound();

            _context.SUPACatEntidades.Remove(entidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
