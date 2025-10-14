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
    public class SUPACatRolesController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatRolesController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatRoles>>> GetSUPACatRoles()
        {
            return await _context.SUPACatRoles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatRoles>> GetSUPACatRoles(int id)
        {
            var rol = await _context.SUPACatRoles
                .FirstOrDefaultAsync(r => r.IdCatRol == id);

            if (rol == null) return NotFound();
            return rol;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatRoles>> PostSUPACatRoles(SUPACatRolesViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DRol", viewModel.DRol)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatRoles @DRol",
                    parameters).FirstOrDefaultAsync();

                var rol = await _context.SUPACatRoles
                    .FirstOrDefaultAsync(r => r.IdCatRol == result);

                return CreatedAtAction(nameof(GetSUPACatRoles), new { id = result }, rol);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el rol: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatRoles(int id, SUPACatRolesViewModel viewModel)
        {
            if (viewModel.IdCatRol.HasValue && id != viewModel.IdCatRol.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatRol", id),
                    new SqlParameter("@DRol", viewModel.DRol)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatRoles @IdCatRol, @DRol",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el rol: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatRoles(int id)
        {
            var rol = await _context.SUPACatRoles.FindAsync(id);
            if (rol == null) return NotFound();

            _context.SUPACatRoles.Remove(rol);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}