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
    public class SUPACatAreasController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatAreasController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            try
            {
                var areas = await _context.SUPACatAreas.ToListAsync();
                return Ok(areas);
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
                var area = await _context.SUPACatAreas.FindAsync(id);
                if (area == null)
                {
                    return NotFound(new { message = $"No se encontró el área con ID {id}" });
                }
                return Ok(area);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SUPACatAreasViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var parameters = new[]
                {
                    new SqlParameter("@Darea", viewModel.Darea)
                };

                // Usar ExecuteSqlRaw para obtener el ID correcto
                var result = await _context.Database
                    .SqlQueryRaw<int>("EXEC SPSUPA_InsertCatAreas @Darea", parameters)
                    .ToListAsync();

                var newId = result.FirstOrDefault();

                // Obtener el área creada para retornarla
                var areaCreada = await _context.SUPACatAreas
                    .FirstOrDefaultAsync(a => a.IdCatAreas == newId);

                return CreatedAtAction(nameof(ListadoporId), new { id = newId }, areaCreada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el área", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SUPACatAreasViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var parameters = new[]
                {
                    new SqlParameter("@IdCatAreas", id),
                    new SqlParameter("@Darea", viewModel.Darea)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC SPSUPA_UpdateCatAreas @IdCatAreas, @Darea", parameters);

                return Ok(new { message = "Área actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el área", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var area = await _context.SUPACatAreas.FindAsync(id);
                if (area == null)
                {
                    return NotFound(new { message = $"No se encontró el área con ID {id}" });
                }

                _context.SUPACatAreas.Remove(area);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Área eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el área", error = ex.Message });
            }
        }
    }
}