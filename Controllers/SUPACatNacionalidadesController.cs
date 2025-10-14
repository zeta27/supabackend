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
    public class SUPACatNacionalidadesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatNacionalidadesController(SUPADbContext context)
        {
            _context = context;
        }

        // GET: api/SUPACatNacionalidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatNacionalidades>>> GetSUPACatNacionalidades()
        {
            return await _context.SUPACatNacionalidades.ToListAsync();
        }

        // GET: api/SUPACatNacionalidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatNacionalidades>> GetSUPACatNacionalidades(int id)
        {
            var nacionalidad = await _context.SUPACatNacionalidades
                .FirstOrDefaultAsync(n => n.IdCatNacionalidad == id);

            if (nacionalidad == null) return NotFound();
            return nacionalidad;
        }

        // POST: api/SUPACatNacionalidades
        [HttpPost]
        public async Task<ActionResult<SUPACatNacionalidades>> PostSUPACatNacionalidades(
            SUPACatNacionalidadesViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DNacionalidad", (object)viewModel.DNacionalidad ?? DBNull.Value)
                };

                // ✅ CORREGIDO: Solo se pasa @DNacionalidad, no @IdCatNacionalidad
                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatNacionalidades @DNacionalidad",
                    parameters).FirstOrDefaultAsync();

                var nacionalidad = await _context.SUPACatNacionalidades
                    .FirstOrDefaultAsync(n => n.IdCatNacionalidad == result);

                return CreatedAtAction(nameof(GetSUPACatNacionalidades), new { id = result }, nacionalidad);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la nacionalidad: {ex.Message}");
            }
        }

        // PUT: api/SUPACatNacionalidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatNacionalidades(
            int id,
            SUPACatNacionalidadesViewModel viewModel)
        {
            if (viewModel.IdCatNacionalidad.HasValue && id != viewModel.IdCatNacionalidad.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatNacionalidad", id),
                    new SqlParameter("@DNacionalidad", (object)viewModel.DNacionalidad ?? DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatNacionalidades @IdCatNacionalidad, @DNacionalidad",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la nacionalidad: {ex.Message}");
            }
        }

        // DELETE: api/SUPACatNacionalidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatNacionalidades(int id)
        {
            var nacionalidad = await _context.SUPACatNacionalidades.FindAsync(id);
            if (nacionalidad == null) return NotFound();

            _context.SUPACatNacionalidades.Remove(nacionalidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}