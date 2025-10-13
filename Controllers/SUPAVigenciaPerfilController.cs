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
    public class SUPAVigenciaPerfilController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAVigenciaPerfilController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAVigenciaPerfil>>> GetSUPAVigenciaPerfil()
        {
            return await _context.SUPAVigenciaPerfil
                .Include(v => v.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAVigenciaPerfil>> GetSUPAVigenciaPerfil(int id)
        {
            var vigencia = await _context.SUPAVigenciaPerfil
                .Include(v => v.IdSUPANavigation)
                .FirstOrDefaultAsync(v => v.IdVigenciaPerfil == id);

            if (vigencia == null) return NotFound();
            return vigencia;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAVigenciaPerfil>> PostSUPAVigenciaPerfil(SUPAVigenciaPerfilViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", (object)viewModel.IdSUPA ?? DBNull.Value),
                    new SqlParameter("@Inicio", viewModel.Inicio),
                    new SqlParameter("@Termino", viewModel.Termino),
                    new SqlParameter("@UltimaVigencia", viewModel.UltimaVigencia)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertVigenciaPerfil @IdSUPA, @Inicio, @Termino, @UltimaVigencia",
                    parameters).FirstOrDefaultAsync();

                var vigencia = await _context.SUPAVigenciaPerfil
                    .Include(v => v.IdSUPANavigation)
                    .FirstOrDefaultAsync(v => v.IdVigenciaPerfil == result);

                return CreatedAtAction(nameof(GetSUPAVigenciaPerfil), new { id = result }, vigencia);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la vigencia del perfil: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAVigenciaPerfil(int id, SUPAVigenciaPerfilViewModel viewModel)
        {
            if (viewModel.IdVigenciaPerfil.HasValue && id != viewModel.IdVigenciaPerfil.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdVigenciaPerfil", id),
                    new SqlParameter("@IdSUPA", (object)viewModel.IdSUPA ?? DBNull.Value),
                    new SqlParameter("@Inicio", viewModel.Inicio),
                    new SqlParameter("@Termino", viewModel.Termino),
                    new SqlParameter("@UltimaVigencia", viewModel.UltimaVigencia)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateVigenciaPerfil @IdVigenciaPerfil, @IdSUPA, @Inicio, @Termino, @UltimaVigencia",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la vigencia del perfil: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAVigenciaPerfil(int id)
        {
            var vigencia = await _context.SUPAVigenciaPerfil.FindAsync(id);
            if (vigencia == null) return NotFound();

            _context.SUPAVigenciaPerfil.Remove(vigencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
