using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatDisciplinasController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatDisciplinasController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatDisciplinas>>> GetSUPACatDisciplinas()
        {
            return await _context.SUPACatDisciplinas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatDisciplinas>> GetSUPACatDisciplinas(int id)
        {
            var disciplina = await _context.SUPACatDisciplinas
                .FirstOrDefaultAsync(d => d.IdCatDisciplinas == id);

            if (disciplina == null) return NotFound();
            return disciplina;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatDisciplinas>> PostSUPACatDisciplinas([FromBody] SUPACatDisciplinasRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Ddisciplina", (object)request.Ddisciplina ?? DBNull.Value)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatDisciplinas @Ddisciplina",
                    parameters).FirstOrDefaultAsync();

                var disciplina = await _context.SUPACatDisciplinas
                    .FirstOrDefaultAsync(d => d.IdCatDisciplinas == result);

                return CreatedAtAction(nameof(GetSUPACatDisciplinas), new { id = result }, disciplina);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la disciplina: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatDisciplinas(int id, [FromBody] SUPACatDisciplinasRequest request)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatDisciplinas", id),
                    new SqlParameter("@Ddisciplina", (object)request.Ddisciplina ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatDisciplinas @IdCatDisciplinas, @Ddisciplina",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la disciplina: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatDisciplinas(int id)
        {
            var disciplina = await _context.SUPACatDisciplinas.FindAsync(id);
            if (disciplina == null) return NotFound();

            _context.SUPACatDisciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatDisciplinasRequest
    {
        public string? Ddisciplina { get; set; }
    }
}
