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
    public class SUPACatNivelEstudiosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatNivelEstudiosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatNivelEstudios>>> GetSUPACatNivelEstudios()
        {
            return await _context.SUPACatNivelEstudios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatNivelEstudios>> GetSUPACatNivelEstudios(int id)
        {
            var nivelEstudios = await _context.SUPACatNivelEstudios
                .FirstOrDefaultAsync(n => n.IdCatNivelEstudios == id);

            if (nivelEstudios == null) return NotFound();
            return nivelEstudios;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatNivelEstudios>> PostSUPACatNivelEstudios(SUPACatNivelEstudiosViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DescripcionNivelEstudios", viewModel.DescripcionNivelEstudios)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatNivelEstudios @DescripcionNivelEstudios",
                    parameters).FirstOrDefaultAsync();

                var nivelEstudios = await _context.SUPACatNivelEstudios
                    .FirstOrDefaultAsync(n => n.IdCatNivelEstudios == result);

                return CreatedAtAction(nameof(GetSUPACatNivelEstudios), new { id = result }, nivelEstudios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el nivel de estudios: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatNivelEstudios(int id, SUPACatNivelEstudiosViewModel viewModel)
        {
            if (viewModel.IdCatNivelEstudios.HasValue && id != viewModel.IdCatNivelEstudios.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatNivelEstudios", id),
                    new SqlParameter("@DescripcionNivelEstudios", viewModel.DescripcionNivelEstudios)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatNivelEstudios @IdCatNivelEstudios, @DescripcionNivelEstudios",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el nivel de estudios: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatNivelEstudios(int id)
        {
            var nivelEstudios = await _context.SUPACatNivelEstudios.FindAsync(id);
            if (nivelEstudios == null) return NotFound();

            _context.SUPACatNivelEstudios.Remove(nivelEstudios);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}