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
    public class SUPACuerpoAcademicosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACuerpoAcademicosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACuerpoAcademicos>>> GetSUPACuerpoAcademicos()
        {
            return await _context.SUPACuerpoAcademicos
                .Include(c => c.IdCatMotivosNavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACuerpoAcademicos>> GetSUPACuerpoAcademicos(int id)
        {
            var cuerpo = await _context.SUPACuerpoAcademicos
                .Include(c => c.IdCatMotivosNavigation)
                .FirstOrDefaultAsync(c => c.IdCA == id);

            if (cuerpo == null) return NotFound();
            return cuerpo;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACuerpoAcademicos>> PostSUPACuerpoAcademicos(SUPACuerpoAcademicosViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Clave", viewModel.Clave),
                    new SqlParameter("@NombreCuerpoAcademico", viewModel.NombreCuerpoAcademico),
                    new SqlParameter("@FechaRegistro", (object)viewModel.FechaRegistro ?? DBNull.Value),
                    new SqlParameter("@UltimoRegistro", viewModel.UltimoRegistro),
                    new SqlParameter("@Baja", viewModel.Baja),
                    new SqlParameter("@FechaBaja", (object)viewModel.FechaBaja ?? DBNull.Value),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos),
                    new SqlParameter("@ObservacionesBaja", (object)viewModel.ObservacionesBaja ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCuerpoAcademicos @Clave, @NombreCuerpoAcademico, @FechaRegistro, @UltimoRegistro, @Baja, @FechaBaja, @IdCatMotivos, @ObservacionesBaja",
                    parameters).FirstOrDefaultAsync();

                var cuerpo = await _context.SUPACuerpoAcademicos
                    .Include(c => c.IdCatMotivosNavigation)
                    .FirstOrDefaultAsync(c => c.IdCA == result);

                return CreatedAtAction(nameof(GetSUPACuerpoAcademicos), new { id = result }, cuerpo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el cuerpo académico: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACuerpoAcademicos(int id, SUPACuerpoAcademicosViewModel viewModel)
        {
            if (viewModel.IdCA.HasValue && id != viewModel.IdCA.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCA", id),
                    new SqlParameter("@Clave", viewModel.Clave),
                    new SqlParameter("@NombreCuerpoAcademico", viewModel.NombreCuerpoAcademico),
                    new SqlParameter("@FechaRegistro", viewModel.FechaRegistro ?? DateTime.Now),
                    new SqlParameter("@UltimoRegistro", viewModel.UltimoRegistro),
                    new SqlParameter("@Baja", viewModel.Baja),
                    new SqlParameter("@FechaBaja", (object)viewModel.FechaBaja ?? DBNull.Value),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos),
                    new SqlParameter("@ObservacionesBaja", (object)viewModel.ObservacionesBaja ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCuerpoAcademicos @IdCA, @Clave, @NombreCuerpoAcademico, @FechaRegistro, @UltimoRegistro, @Baja, @FechaBaja, @IdCatMotivos, @ObservacionesBaja",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el cuerpo académico: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACuerpoAcademicos(int id)
        {
            var cuerpo = await _context.SUPACuerpoAcademicos.FindAsync(id);
            if (cuerpo == null) return NotFound();

            _context.SUPACuerpoAcademicos.Remove(cuerpo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
