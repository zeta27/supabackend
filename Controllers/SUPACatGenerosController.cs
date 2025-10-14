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
    public class SUPACatGenerosController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatGenerosController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            try
            {
                var generos = await _context.SUPACatGeneros.ToListAsync();
                return Ok(generos);
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
                var genero = await _context.SUPACatGeneros.FindAsync(id);
                if (genero == null)
                {
                    return NotFound(new { message = $"No se encontró el género con ID {id}" });
                }
                return Ok(genero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SUPACatGenerosViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var parameters = new[]
                {
                    new SqlParameter("@DGenero", viewModel.DGenero)
                };

                var result = await _context.Database
                    .SqlQueryRaw<int>("EXEC SPSUPA_InsertCatGeneros @DGenero", parameters)
                    .ToListAsync();

                var newId = result.FirstOrDefault();

                var generoCreado = await _context.SUPACatGeneros
                    .FirstOrDefaultAsync(g => g.IdCatGeneros == newId);

                return CreatedAtAction(nameof(ListadoporId), new { id = newId }, generoCreado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el género", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SUPACatGenerosViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var parameters = new[]
                {
                    new SqlParameter("@IdCatGeneros", id),
                    new SqlParameter("@DGenero", viewModel.DGenero)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC SPSUPA_UpdateCatGeneros @IdCatGeneros, @DGenero", parameters);

                return Ok(new { message = "Género actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el género", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var genero = await _context.SUPACatGeneros.FindAsync(id);
                if (genero == null)
                {
                    return NotFound(new { message = $"No se encontró el género con ID {id}" });
                }

                _context.SUPACatGeneros.Remove(genero);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Género eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el género", error = ex.Message });
            }
        }
    }
}