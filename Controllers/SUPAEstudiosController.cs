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
    public class SUPAEstudiosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAEstudiosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAEstudios>>> GetSUPAEstudios()
        {
            return await _context.SUPAEstudios
                .Include(e => e.IdCatNivelEstudiosNavigation)
                .Include(e => e.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAEstudios>> GetSUPAEstudios(int id)
        {
            var estudio = await _context.SUPAEstudios
                .Include(e => e.IdCatNivelEstudiosNavigation)
                .Include(e => e.IdSUPANavigation)
                .FirstOrDefaultAsync(e => e.IdEstudios == id);

            if (estudio == null) return NotFound();
            return estudio;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAEstudios>> PostSUPAEstudios(SUPAEstudiosViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Abreviatura", (object)viewModel.Abreviatura ?? DBNull.Value),
                    new SqlParameter("@EstudiosEn", (object)viewModel.EstudiosEn ?? DBNull.Value),
                    new SqlParameter("@Inicio", (object)viewModel.Inicio ?? DBNull.Value),
                    new SqlParameter("@Termino", (object)viewModel.Termino ?? DBNull.Value),
                    new SqlParameter("@FechaObtencion", (object)viewModel.FechaObtencion ?? DBNull.Value),
                    new SqlParameter("@Titulo", (object)viewModel.Titulo ?? DBNull.Value),
                    new SqlParameter("@Cedula", (object)viewModel.Cedula ?? DBNull.Value),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@UltimoGrado", viewModel.UltimoGrado),
                    new SqlParameter("@IdCatNivelEstudios", viewModel.IdCatNivelEstudios)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertEstudios @Abreviatura, @EstudiosEn, @Inicio, @Termino, @FechaObtencion, @Titulo, @Cedula, @IdSUPA, @UltimoGrado, @IdCatNivelEstudios",
                    parameters).FirstOrDefaultAsync();

                var estudio = await _context.SUPAEstudios
                    .Include(e => e.IdCatNivelEstudiosNavigation)
                    .Include(e => e.IdSUPANavigation)
                    .FirstOrDefaultAsync(e => e.IdEstudios == result);

                return CreatedAtAction(nameof(GetSUPAEstudios), new { id = result }, estudio);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el estudio: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAEstudios(int id, SUPAEstudiosViewModel viewModel)
        {
            if (viewModel.IdEstudios.HasValue && id != viewModel.IdEstudios.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdEstudios", id),
                    new SqlParameter("@Abreviatura", (object)viewModel.Abreviatura ?? DBNull.Value),
                    new SqlParameter("@EstudiosEn", (object)viewModel.EstudiosEn ?? DBNull.Value),
                    new SqlParameter("@Inicio", (object)viewModel.Inicio ?? DBNull.Value),
                    new SqlParameter("@Termino", (object)viewModel.Termino ?? DBNull.Value),
                    new SqlParameter("@FechaObtencion", (object)viewModel.FechaObtencion ?? DBNull.Value),
                    new SqlParameter("@Titulo", (object)viewModel.Titulo ?? DBNull.Value),
                    new SqlParameter("@Cedula", (object)viewModel.Cedula ?? DBNull.Value),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@UltimoGrado", viewModel.UltimoGrado),
                    new SqlParameter("@IdCatNivelEstudios", viewModel.IdCatNivelEstudios)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateEstudios @IdEstudios, @Abreviatura, @EstudiosEn, @Inicio, @Termino, @FechaObtencion, @Titulo, @Cedula, @IdSUPA, @UltimoGrado, @IdCatNivelEstudios",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el estudio: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAEstudios(int id)
        {
            var estudio = await _context.SUPAEstudios.FindAsync(id);
            if (estudio == null) return NotFound();

            _context.SUPAEstudios.Remove(estudio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
