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
    public class SUPACitasController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACitasController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACitas>>> GetSUPACitas()
        {
            return await _context.SUPACitas
                .Include(c => c.IdSUPANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACitas>> GetSUPACitas(int id)
        {
            var cita = await _context.SUPACitas
                .Include(c => c.IdSUPANavigation)
                .FirstOrDefaultAsync(c => c.IdCita == id);

            if (cita == null) return NotFound();
            return cita;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACitas>> PostSUPACitas(SUPACitasViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdSUPA", (object)viewModel.IdSUPA ?? DBNull.Value),
                    new SqlParameter("@FechaCita", viewModel.FechaCita),
                    new SqlParameter("@HoraInicio", viewModel.HoraInicio),
                    new SqlParameter("@HoraTermino", viewModel.HoraTermino),
                    new SqlParameter("@Estado", viewModel.Estado),
                    new SqlParameter("@Sala", viewModel.Sala),
                    new SqlParameter("@Lugar", viewModel.Lugar)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCitas @IdSUPA, @FechaCita, @HoraInicio, @HoraTermino, @Estado, @Sala, @Lugar",
                    parameters).FirstOrDefaultAsync();

                var cita = await _context.SUPACitas
                    .Include(c => c.IdSUPANavigation)
                    .FirstOrDefaultAsync(c => c.IdCita == result);

                return CreatedAtAction(nameof(GetSUPACitas), new { id = result }, cita);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la cita: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACitas(int id, SUPACitasViewModel viewModel)
        {
            if (viewModel.IdCita.HasValue && id != viewModel.IdCita.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCita", id),
                    new SqlParameter("@IdSUPA", (object)viewModel.IdSUPA ?? DBNull.Value),
                    new SqlParameter("@FechaCita", viewModel.FechaCita),
                    new SqlParameter("@HoraInicio", viewModel.HoraInicio),
                    new SqlParameter("@HoraTermino", viewModel.HoraTermino),
                    new SqlParameter("@Estado", viewModel.Estado),
                    new SqlParameter("@Sala", viewModel.Sala),
                    new SqlParameter("@Lugar", viewModel.Lugar)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCitas @IdCita, @IdSUPA, @FechaCita, @HoraInicio, @HoraTermino, @Estado, @Sala, @Lugar",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la cita: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACitas(int id)
        {
            var cita = await _context.SUPACitas.FindAsync(id);
            if (cita == null) return NotFound();

            _context.SUPACitas.Remove(cita);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
