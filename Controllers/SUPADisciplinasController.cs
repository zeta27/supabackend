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
    public class SUPADisciplinasController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPADisciplinasController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPADisciplinas>>> GetSUPADisciplinas()
        {
            return await _context.SUPADisciplinas
                .Include(d => d.IdCatDisciplinasNavigation)
                .Include(d => d.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPADisciplinas>> GetSUPADisciplinas(int id)
        {
            var disciplina = await _context.SUPADisciplinas
                .Include(d => d.IdCatDisciplinasNavigation)
                .Include(d => d.IdSUPANavigation)
                .FirstOrDefaultAsync(d => d.IdDisciplinas == id);

            if (disciplina == null) return NotFound();
            return disciplina;
        }

        [HttpPost]
        public async Task<ActionResult<SUPADisciplinas>> PostSUPADisciplinas(SUPADisciplinasViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatDisciplinas", viewModel.IdCatDisciplinas),
                    new SqlParameter("@FechaRegistro", (object)viewModel.FechaRegistro ?? DBNull.Value),
                    new SqlParameter("@UltimaDisciplina", viewModel.UltimaDisciplina)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertDisciplinas @IdSUPA, @IdCatDisciplinas, @FechaRegistro, @UltimaDisciplina",
                    parameters).FirstOrDefaultAsync();

                var disciplina = await _context.SUPADisciplinas
                    .Include(d => d.IdCatDisciplinasNavigation)
                    .Include(d => d.IdSUPANavigation)
                    .FirstOrDefaultAsync(d => d.IdDisciplinas == result);

                return CreatedAtAction(nameof(GetSUPADisciplinas), new { id = result }, disciplina);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la disciplina: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPADisciplinas(int id, SUPADisciplinasViewModel viewModel)
        {
            if (viewModel.IdDisciplinas.HasValue && id != viewModel.IdDisciplinas.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdDisciplinas", id),
                    new SqlParameter("@IdSUPA", viewModel.IdSUPA),
                    new SqlParameter("@IdCatDisciplinas", viewModel.IdCatDisciplinas),
                    new SqlParameter("@FechaRegistro", viewModel.FechaRegistro ?? DateTime.Now),
                    new SqlParameter("@UltimaDisciplina", viewModel.UltimaDisciplina)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateDisciplinas @IdDisciplinas, @IdSUPA, @IdCatDisciplinas, @FechaRegistro, @UltimaDisciplina",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la disciplina: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPADisciplinas(int id)
        {
            var disciplina = await _context.SUPADisciplinas.FindAsync(id);
            if (disciplina == null) return NotFound();

            _context.SUPADisciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
