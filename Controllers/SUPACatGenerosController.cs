using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using Microsoft.Data.SqlClient;

namespace supa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SUPACatGenerosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatGenerosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatGeneros>>> GetSUPACatGeneros()
        {
            return await _context.SUPACatGeneros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatGeneros>> GetSUPACatGeneros(int id)
        {
            var genero = await _context.SUPACatGeneros
                .FirstOrDefaultAsync(g => g.IdCatGeneros == id);

            if (genero == null) return NotFound();
            return genero;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatGeneros>> PostSUPACatGeneros([FromBody] SUPACatGenerosRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DGenero))
                return BadRequest("La descripción del género es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DGenero", request.DGenero)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatGeneros @DGenero",
                    parameters).FirstOrDefaultAsync();

                var genero = await _context.SUPACatGeneros
                    .FirstOrDefaultAsync(g => g.IdCatGeneros == result);

                return CreatedAtAction(nameof(GetSUPACatGeneros), new { id = result }, genero);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el género: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatGeneros(int id, [FromBody] SUPACatGenerosRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DGenero))
                return BadRequest("La descripción del género es requerida");

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatGeneros", id),
                    new SqlParameter("@DGenero", request.DGenero)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatGeneros @IdCatGeneros, @DGenero",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el género: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatGeneros(int id)
        {
            var genero = await _context.SUPACatGeneros.FindAsync(id);
            if (genero == null) return NotFound();

            _context.SUPACatGeneros.Remove(genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class SUPACatGenerosRequest
    {
        public string DGenero { get; set; } = null!;
    }
}
