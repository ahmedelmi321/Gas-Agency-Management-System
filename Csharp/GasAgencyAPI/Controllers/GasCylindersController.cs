using GasAgencyAPI.Data;
using GasAgencyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasAgencyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GasCylindersController : ControllerBase
    {
        private readonly GasAgencyDbContext _context;

        public GasCylindersController(GasAgencyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GasCylinder>>> GetAll()
        {
            return await _context.GasCylinders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GasCylinder>> GetById(int id)
        {
            var cylinder = await _context.GasCylinders.FindAsync(id);
            if (cylinder == null) return NotFound();
            return cylinder;
        }

        [HttpPost]
        public async Task<ActionResult<GasCylinder>> Create(GasCylinder cylinder)
        {
            _context.GasCylinders.Add(cylinder);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cylinder.CylinderID }, cylinder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GasCylinder cylinder)
        {
            if (id != cylinder.CylinderID) return BadRequest();
            _context.Entry(cylinder).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.GasCylinders.Any(g => g.CylinderID == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cylinder = await _context.GasCylinders.FindAsync(id);
            if (cylinder == null) return NotFound();
            _context.GasCylinders.Remove(cylinder);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
