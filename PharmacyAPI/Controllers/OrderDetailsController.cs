using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Data;
using PharmacyAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly PharmacyDbContext _context;

        public OrderDetailsController(PharmacyDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails(int? page, int? pageSize)
        {
            var query = _context.OrderDetails
                                .Include(od => od.Order)
                                .Include(od => od.Product)
                                .AsQueryable();

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            var results = await query.ToListAsync();
            return Ok(new { data = results });
        }


        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails
                .Include(od => od.Order)    // Include Order relation (if needed)
                .Include(od => od.Product) // Include Product relation (if needed)
                .FirstOrDefaultAsync(od => od.Id == id);
            if (orderDetail == null)
            {
                return NotFound(new { message = $"OrderDetail with ID {id} not found." });
            }

            return Ok(new { data = orderDetail });

        }

        // PUT: api/OrderDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound(new { message = $"OrderDetail with ID {id} not found for update." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { message = "An error occurred while updating the record." });
                }
            }


            return NoContent();
        }

        // POST: api/OrderDetails
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            if (!_context.Orders.Any(o => o.Id == orderDetail.OrderId))
            {
                return BadRequest(new { message = $"Order with ID {orderDetail.OrderId} does not exist." });
            }

            if (!_context.Products.Any(p => p.Id == orderDetail.ProductId))
            {
                return BadRequest(new { message = $"Product with ID {orderDetail.ProductId} does not exist." });
            }

            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.Id }, orderDetail);
        }


        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails
                                            .Include(od => od.Order)
                                            .Include(od => od.Product)
                                            .FirstOrDefaultAsync(od => od.Id == id);

            if (orderDetail == null)
            {
                return NotFound(new { message = $"OrderDetail with ID {id} not found." });
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return Ok(new { message = "OrderDetail deleted successfully." });
        }


        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> SearchOrderDetails(int? orderId, int? productId)
        {
            var query = _context.OrderDetails.AsQueryable();

            if (orderId.HasValue)
            {
                query = query.Where(od => od.OrderId == orderId);
            }

            if (productId.HasValue)
            {
                query = query.Where(od => od.ProductId == productId);
            }

            var results = await query.Include(od => od.Order)
                                      .Include(od => od.Product)
                                      .ToListAsync();

            return Ok(new { data = results });
        }

    }
}
