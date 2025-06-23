using Microsoft.EntityFrameworkCore;
using UserOrderSystem.Data;
using UserOrderSystem.Models;
using Xunit;

namespace UserOrderSystem.Tests.Data;

public class OrderDbContextTests
{
    [Fact]
    public void AddOrder_SavesAndRetrievesOrder()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>()
            .UseInMemoryDatabase(databaseName: "TestOrdersDb")
            .Options;

        // Use a new context instance to add the order
        using (var context = new OrderDbContext(options))
        {
            var order = new Order { IsPaid = false };
            context.Orders.Add(order);
            context.SaveChanges();
        }

        // Use a separate context instance to verify retrieval
        using (var context = new OrderDbContext(options))
        {
            var order = context.Orders.FirstOrDefault();
            Assert.NotNull(order);
            Assert.False(order.IsPaid);
        }
    }
}