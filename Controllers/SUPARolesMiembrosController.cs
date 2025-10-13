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
    public class SUPARolesMiembrosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPARolesMiembrosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPARolesMiembros>>> GetSUPARolesMiembros()
        {
            return await _context.SUPARolesMiembros
                .Include(r => r.IdCatRolNavigation)
                .Include(r => r.IdMiembrosCANavigation)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPARolesMiembros>> GetSUPARolesMiembros(int id)
        {
            var rol = await _context.SUPARolesMiembros
                .Include(r => r.IdCatRolNavigation)
                .Include(r => r.IdMiembrosCANavigation)
                .FirstOrDefaultAsync(r => r.IdRolesMiembros == id);

            if (rol == null) return NotFound();
            return rol;
        }

        [HttpPost]
        public async Task<ActionResult<SUPARolesMiembros>> PostSUPARolesMiembros(SUPARolesMiembrosViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@FechaAsignacion", viewModel.FechaAsignacion),
                    new SqlParameter("@UltimoRol", viewModel.UltimoRol),
                    new SqlParameter("@IdCatRol", viewModel.IdCatRol),
                    new SqlParameter("@IdMiembrosCA", viewModel.IdMiembrosCA)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertRolesMiembros @FechaAsignacion, @UltimoRol, @IdCatRol, @IdMiembrosCA",
                    parameters).FirstOrDefaultAsync();

                var rol = await _context.SUPARolesMiembros
                    .Include(r => r.IdCatRolNavigation)
                    .Include(r => r.IdMiembrosCANavigation)
                    .FirstOrDefaultAsync(r => r.IdRolesMiembros == result);

                return CreatedAtAction(nameof(GetSUPARolesMiembros), new { id = result }, rol);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el rol de miembro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPARolesMiembros(int id, SUPARolesMiembrosViewModel viewModel)
        {
            if (viewModel.IdRolesMiembros.HasValue && id != viewModel.IdRolesMiembros.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdRolesMiembros", id),
                    new SqlParameter("@FechaAsignacion", viewModel.FechaAsignacion),
                    new SqlParameter("@UltimoRol", viewModel.UltimoRol),
                    new SqlParameter("@IdCatRol", viewModel.IdCatRol),
                    new SqlParameter("@IdMiembrosCA", viewModel.IdMiembrosCA)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateRolesMiembros @IdRolesMiembros, @FechaAsignacion, @UltimoRol, @IdCatRol, @IdMiembrosCA",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el rol de miembro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPARolesMiembros(int id)
        {
            var rol = await _context.SUPARolesMiembros.FindAsync(id);
            if (rol == null) return NotFound();

            _context.SUPARolesMiembros.Remove(rol);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
