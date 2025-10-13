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
    public class SUPAVigenciaCuerpoController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAVigenciaCuerpoController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAVigenciaCuerpo>>> GetSUPAVigenciaCuerpo()
        {
            return await _context.SUPAVigenciaCuerpo
                .Include(v => v.IdCANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAVigenciaCuerpo>> GetSUPAVigenciaCuerpo(int id)
        {
            var vigencia = await _context.SUPAVigenciaCuerpo
                .Include(v => v.IdCANavigation)
                .FirstOrDefaultAsync(v => v.IdVigenciaCuerpo == id);

            if (vigencia == null) return NotFound();
            return vigencia;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAVigenciaCuerpo>> PostSUPAVigenciaCuerpo(SUPAVigenciaCuerpoViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@Inicio", viewModel.Inicio),
                    new SqlParameter("@Termino", viewModel.Termino),
                    new SqlParameter("@UltimaVigencia", viewModel.UltimaVigencia)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertVigenciaCuerpo @IdCA, @Inicio, @Termino, @UltimaVigencia",
                    parameters).FirstOrDefaultAsync();

                var vigencia = await _context.SUPAVigenciaCuerpo
                    .Include(v => v.IdCANavigation)
                    .FirstOrDefaultAsync(v => v.IdVigenciaCuerpo == result);

                return CreatedAtAction(nameof(GetSUPAVigenciaCuerpo), new { id = result }, vigencia);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la vigencia del cuerpo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAVigenciaCuerpo(int id, SUPAVigenciaCuerpoViewModel viewModel)
        {
            if (viewModel.IdVigenciaCuerpo.HasValue && id != viewModel.IdVigenciaCuerpo.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdVigenciaCuerpo", id),
                    new SqlParameter("@IdCA", viewModel.IdCA),
                    new SqlParameter("@Inicio", viewModel.Inicio),
                    new SqlParameter("@Termino", viewModel.Termino),
                    new SqlParameter("@UltimaVigencia", (object)viewModel.UltimaVigencia ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateVigenciaCuerpo @IdVigenciaCuerpo, @IdCA, @Inicio, @Termino, @UltimaVigencia",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la vigencia del cuerpo: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAVigenciaCuerpo(int id)
        {
            var vigencia = await _context.SUPAVigenciaCuerpo.FindAsync(id);
            if (vigencia == null) return NotFound();

            _context.SUPAVigenciaCuerpo.Remove(vigencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
