using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using supa.Data;
using supa.Models;
using supa.Models.ViewModels;

namespace supa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SUPACatAreaDedicaController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatAreaDedicaController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            try
            {
                var areasDedica = await _context.SUPACatAreaDedica.ToListAsync();
                return Ok(areasDedica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListadoporId(int id)
        {
            try
            {
                var areaDedica = await _context.SUPACatAreaDedica.FindAsync(id);
                if (areaDedica == null)
                {
                    return NotFound(new { message = $"No se encontró el área de dedicación con ID {id}" });
                }
                return Ok(areaDedica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SUPACatAreaDedicaViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var parameters = new[]
                {
                    new SqlParameter("@DAreaDedica", viewModel.DAreaDedica)
                };

                // Usar ExecuteSqlRaw para obtener el ID correcto
                var result = await _context.Database
                    .SqlQueryRaw<int>("EXEC SPSUPA_InsertCatAreaDedica @DAreaDedica", parameters)
                    .ToListAsync();

                var newId = result.FirstOrDefault();

                // Obtener el área de dedicación creada para retornarla
                var areaDedicaCreada = await _context.SUPACatAreaDedica
                    .FirstOrDefaultAsync(a => a.IdCatAreaDedica == newId);

                return CreatedAtAction(nameof(ListadoporId), new { id = newId }, areaDedicaCreada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el área de dedicación", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SUPACatAreaDedicaViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var parameters = new[]
                {
                    new SqlParameter("@IdCatAreaDedica", id),
                    new SqlParameter("@DAreaDedica", viewModel.DAreaDedica)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC SPSUPA_UpdateCatAreaDedica @IdCatAreaDedica, @DAreaDedica", parameters);

                return Ok(new { message = "Área de dedicación actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el área de dedicación", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var areaDedica = await _context.SUPACatAreaDedica.FindAsync(id);
                if (areaDedica == null)
                {
                    return NotFound(new { message = $"No se encontró el área de dedicación con ID {id}" });
                }

                _context.SUPACatAreaDedica.Remove(areaDedica);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Área dedicada eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el área dedicación", error = ex.Message });
            }
        }
    }
}