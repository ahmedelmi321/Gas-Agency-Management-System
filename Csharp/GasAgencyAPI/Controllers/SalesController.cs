using GasAgencyAPI.Data;
using GasAgencyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasAgencyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly GasAgencyDbContext _context;

        public SalesController(GasAgencyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.GasCylinder)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetById(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.GasCylinder)
                .FirstOrDefaultAsync(s => s.SaleID == id);
            if (sale == null) return NotFound();
            return sale;
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> Create(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = sale.SaleID }, sale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sale sale)
        {
            if (id != sale.SaleID) return BadRequest();
            _context.Entry(sale).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sales.Any(s => s.SaleID == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return NotFound();
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
