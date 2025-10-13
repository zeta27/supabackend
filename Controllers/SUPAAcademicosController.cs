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
    public class SUPAAcademicosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPAAcademicosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPAAcademicos>>> GetSUPAAcademicos()
        {
            return await _context.SUPAAcademicos
                .Include(a => a.IdCatGenerosNavigation)
                .Include(a => a.IdCatMotivosNavigation)
                .Include(a => a.IdCatNacionalidadNavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPAAcademicos>> GetSUPAAcademicos(int id)
        {
            var academico = await _context.SUPAAcademicos
                .Include(a => a.IdCatGenerosNavigation)
                .Include(a => a.IdCatMotivosNavigation)
                .Include(a => a.IdCatNacionalidadNavigation)
                .FirstOrDefaultAsync(a => a.IdSUPA == id);

            if (academico == null) return NotFound();
            return academico;
        }

        [HttpPost]
        public async Task<ActionResult<SUPAAcademicos>> PostSUPAAcademicos(SUPAAcademicosViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@CURP", viewModel.CURP),
                    new SqlParameter("@NP", viewModel.NP),
                    new SqlParameter("@Paterno", (object)viewModel.Paterno ?? DBNull.Value),
                    new SqlParameter("@Materno", (object)viewModel.Materno ?? DBNull.Value),
                    new SqlParameter("@Nombre", viewModel.Nombre),
                    new SqlParameter("@IdCatGeneros", viewModel.IdCatGeneros),
                    new SqlParameter("@IdCatNacionalidad", viewModel.IdCatNacionalidad),
                    new SqlParameter("@Institucion", viewModel.Institucion),
                    new SqlParameter("@IdPRODEP", viewModel.IdPRODEP),
                    new SqlParameter("@CuentaUV", (object)viewModel.CuentaUV ?? DBNull.Value),
                    new SqlParameter("@Baja", viewModel.Baja),
                    new SqlParameter("@FechaBaja", (object)viewModel.FechaBaja ?? DBNull.Value),
                    new SqlParameter("@Observaciones", (object)viewModel.Observaciones ?? DBNull.Value),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertAcademicos @CURP, @NP, @Paterno, @Materno, @Nombre, @IdCatGeneros, @IdCatNacionalidad, @Institucion, @IdPRODEP, @CuentaUV, @Baja, @FechaBaja, @Observaciones, @IdCatMotivos",
                    parameters).FirstOrDefaultAsync();

                var academico = await _context.SUPAAcademicos
                    .Include(a => a.IdCatGenerosNavigation)
                    .Include(a => a.IdCatMotivosNavigation)
                    .Include(a => a.IdCatNacionalidadNavigation)
                    .FirstOrDefaultAsync(a => a.IdSUPA == result);

                return CreatedAtAction(nameof(GetSUPAAcademicos), new { id = result }, academico);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el académico: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPAAcademicos(int id, SUPAAcademicosViewModel viewModel)
        {
            if (viewModel.IdSUPA.HasValue && id != viewModel.IdSUPA.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", id),
                    new SqlParameter("@CURP", viewModel.CURP),
                    new SqlParameter("@NP", viewModel.NP),
                    new SqlParameter("@Paterno", (object)viewModel.Paterno ?? DBNull.Value),
                    new SqlParameter("@Materno", (object)viewModel.Materno ?? DBNull.Value),
                    new SqlParameter("@Nombre", viewModel.Nombre),
                    new SqlParameter("@IdCatGeneros", viewModel.IdCatGeneros),
                    new SqlParameter("@IdCatNacionalidad", viewModel.IdCatNacionalidad),
                    new SqlParameter("@Institucion", viewModel.Institucion),
                    new SqlParameter("@IdPRODEP", viewModel.IdPRODEP),
                    new SqlParameter("@CuentaUV", (object)viewModel.CuentaUV ?? DBNull.Value),
                    new SqlParameter("@Baja", viewModel.Baja),
                    new SqlParameter("@FechaBaja", (object)viewModel.FechaBaja ?? DBNull.Value),
                    new SqlParameter("@Observaciones", (object)viewModel.Observaciones ?? DBNull.Value),
                    new SqlParameter("@IdCatMotivos", viewModel.IdCatMotivos)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateAcademicos @IdSUPA, @CURP, @NP, @Paterno, @Materno, @Nombre, @IdCatGeneros, @IdCatNacionalidad, @Institucion, @IdPRODEP, @CuentaUV, @Baja, @FechaBaja, @Observaciones, @IdCatMotivos",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el académico: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPAAcademicos(int id)
        {
            var academico = await _context.SUPAAcademicos.FindAsync(id);
            if (academico == null) return NotFound();

            _context.SUPAAcademicos.Remove(academico);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
