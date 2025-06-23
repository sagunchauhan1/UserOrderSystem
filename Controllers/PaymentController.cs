using Microsoft.AspNetCore.Mvc;
using UserOrderSystem.Data;
using UserOrderSystem.Models;
using UserOrderSystem.Services;

namespace UserOrderSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(OrderDbContext dbContext, PaymentService paymentService) : ControllerBase
{
    [HttpPost("process/{id}")]
    public IActionResult Process(int id)
    {
        var order = dbContext.Orders.Find(id);
        try
        {
            paymentService.ProcessPayment(order);
            dbContext.SaveChanges();
            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create")]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
        return Ok(order);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        var order = dbContext.Orders.Find(id);
        return order == null ? NotFound() : Ok(order);
    }
}
