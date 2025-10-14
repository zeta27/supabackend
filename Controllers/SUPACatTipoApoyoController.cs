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
    public class SUPACatTipoApoyoController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatTipoApoyoController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatTipoApoyo>>> GetSUPACatTipoApoyo()
        {
            return await _context.SUPACatTipoApoyo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatTipoApoyo>> GetSUPACatTipoApoyo(int id)
        {
            var tipoApoyo = await _context.SUPACatTipoApoyo
                .FirstOrDefaultAsync(t => t.IdCatTipoApoyo == id);

            if (tipoApoyo == null) return NotFound();
            return tipoApoyo;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatTipoApoyo>> PostSUPACatTipoApoyo(SUPACatTipoApoyoViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DTipoApoyo", viewModel.DTipoApoyo),
                    new SqlParameter("@FedInter", viewModel.FedInter)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatTipoApoyo @DTipoApoyo, @FedInter",
                    parameters).FirstOrDefaultAsync();

                var tipoApoyo = await _context.SUPACatTipoApoyo
                    .FirstOrDefaultAsync(t => t.IdCatTipoApoyo == result);

                return CreatedAtAction(nameof(GetSUPACatTipoApoyo), new { id = result }, tipoApoyo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el tipo de apoyo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatTipoApoyo(int id, SUPACatTipoApoyoViewModel viewModel)
        {
            if (viewModel.IdCatTipoApoyo.HasValue && id != viewModel.IdCatTipoApoyo.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatTipoApoyo", id),
                    new SqlParameter("@DTipoApoyo", viewModel.DTipoApoyo),
                    new SqlParameter("@FedInter", viewModel.FedInter)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatTipoApoyo @IdCatTipoApoyo, @DTipoApoyo, @FedInter",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el tipo de apoyo: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatTipoApoyo(int id)
        {
            var tipoApoyo = await _context.SUPACatTipoApoyo.FindAsync(id);
            if (tipoApoyo == null) return NotFound();

            _context.SUPACatTipoApoyo.Remove(tipoApoyo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}