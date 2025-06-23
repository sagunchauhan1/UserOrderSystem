using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserOrderSystem.Controllers;
using UserOrderSystem.Data;
using UserOrderSystem.Models;
using UserOrderSystem.Services;
using Xunit;

namespace UserOrderSystem.Tests.Controllers;

public class PaymentControllerTests
{
    private static OrderDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new OrderDbContext(options);
    }

    [Fact]
    public void CreateOrder_AddsOrderAndReturnsOk()
    {
        using var dbContext = GetInMemoryDbContext();
        var paymentService = new PaymentService();
        var controller = new PaymentController(dbContext, paymentService);
        var order = new Order { Name = "Test Order", IsPaid = false };

        var result = controller.CreateOrder(order);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedOrder = Assert.IsType<Order>(okResult.Value);
        Assert.Equal("Test Order", returnedOrder.Name);
        Assert.Single(dbContext.Orders);
    }

    [Fact]
    public void GetOrder_ReturnsOrder_WhenExists()
    {
        using var dbContext = GetInMemoryDbContext();
        var paymentService = new PaymentService();
        var controller = new PaymentController(dbContext, paymentService);
        var order = new Order { Name = "Test Order", IsPaid = false };
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();

        var result = controller.GetOrder(order.Id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedOrder = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(order.Name, returnedOrder.Name);
    }

    [Fact]
    public void GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        using var dbContext = GetInMemoryDbContext();
        var paymentService = new PaymentService();
        var controller = new PaymentController(dbContext, paymentService);

        var result = controller.GetOrder(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Process_ReturnsOk_WhenPaymentSuccessful()
    {
        using var dbContext = GetInMemoryDbContext();
        var paymentService = new PaymentService();
        var controller = new PaymentController(dbContext, paymentService);
        var order = new Order { Name = "Test Order", IsPaid = false };
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();

        var result = controller.Process(order.Id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedOrder = Assert.IsType<Order>(okResult.Value);
        Assert.True(returnedOrder.IsPaid);
    }

    [Fact]
    public void Process_ReturnsBadRequest_WhenOrderIsNull()
    {
        using var dbContext = GetInMemoryDbContext();
        var paymentService = new PaymentService();
        var controller = new PaymentController(dbContext, paymentService);

        var result = controller.Process(123); // No such order

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Order cannot be null", badRequest.Value.ToString());
    }

    [Fact]
    public void Process_ReturnsBadRequest_WhenOrderAlreadyPaid()
    {
        using var dbContext = GetInMemoryDbContext();
        var paymentService = new PaymentService();
        var controller = new PaymentController(dbContext, paymentService);
        var order = new Order { Name = "Paid Order", IsPaid = true };
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();

        var result = controller.Process(order.Id);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("already paid", badRequest.Value.ToString(), StringComparison.OrdinalIgnoreCase);
    }
}
