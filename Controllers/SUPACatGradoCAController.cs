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
    public class SUPACatGradoCAController : ControllerBase
    {
        private readonly SUPADbContext _context;

        public SUPACatGradoCAController(SUPADbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SUPACatGradoCA>>> GetSUPACatGradoCA()
        {
            return await _context.SUPACatGradoCA.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SUPACatGradoCA>> GetSUPACatGradoCA(int id)
        {
            var gradoCA = await _context.SUPACatGradoCA
                .FirstOrDefaultAsync(g => g.IdCatGradoCA == id);

            if (gradoCA == null) return NotFound();
            return gradoCA;
        }

        [HttpPost]
        public async Task<ActionResult<SUPACatGradoCA>> PostSUPACatGradoCA(SUPACatGradoCAViewModel viewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@DescripcionGrado", viewModel.DescripcionGrado),
                    new SqlParameter("@Abreviatura", viewModel.Abreviatura)
                };

                var result = await _context.Database.SqlQueryRaw<int>(
                    "EXEC SPSUPA_InsertCatGradoCA @DescripcionGrado, @Abreviatura",
                    parameters).FirstOrDefaultAsync();

                var gradoCA = await _context.SUPACatGradoCA
                    .FirstOrDefaultAsync(g => g.IdCatGradoCA == result);

                return CreatedAtAction(nameof(GetSUPACatGradoCA), new { id = result }, gradoCA);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el grado CA: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSUPACatGradoCA(int id, SUPACatGradoCAViewModel viewModel)
        {
            if (viewModel.IdCatGradoCA.HasValue && id != viewModel.IdCatGradoCA.Value)
                return BadRequest("El ID no coincide con el modelo");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@IdCatGradoCA", id),
                    new SqlParameter("@DescripcionGrado", viewModel.DescripcionGrado),
                    new SqlParameter("@Abreviatura", viewModel.Abreviatura)
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SPSUPA_UpdateCatGradoCA @IdCatGradoCA, @DescripcionGrado, @Abreviatura",
                    parameters);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el grado CA: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSUPACatGradoCA(int id)
        {
            var gradoCA = await _context.SUPACatGradoCA.FindAsync(id);
            if (gradoCA == null) return NotFound();

            _context.SUPACatGradoCA.Remove(gradoCA);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}