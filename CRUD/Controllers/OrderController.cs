using System;
using CRUD.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(string filterByCustomerName)
        {
            var sqlQuery = @"
        SELECT o.*
        FROM Orders AS o
        INNER JOIN Customers AS c ON o.CustomerId = c.CustomerId
        WHERE c.Name LIKE {0}";

            var orders = await _context.Orders
                .FromSqlRaw(sqlQuery, $"%{filterByCustomerName}%")
                .ToListAsync();

            return Ok(orders);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrders(string filterByCustomerName)
        //{
        //    // Define your SQL query
        //    var sqlQuery = @"
        //SELECT o.order_id AS Id, o.OrderNumber, o.CustomerId, c.Name AS CustomerName
        //FROM orders AS o
        //INNER JOIN customers AS c ON o.CustomerId = c.customer_id
        //WHERE c.Name LIKE {0}";

        //    // Execute the SQL query with the filterByCustomerName parameter
        //    var orders = await _context.OrderViewModel
        //        .FromSqlRaw(sqlQuery, $"%{filterByCustomerName}%")
        //        .ToListAsync();

        //    return Ok(orders);
        //}

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}

