using UserOrderSystem.Models;
using UserOrderSystem.Services;
using Xunit;

namespace UserOrderSystem.Tests.ServicesTests;

public class PaymentServiceTests
{
    [Fact]
    public void ProcessPayment_ThrowsInvalidOperationException_WhenOrderIsNull()
    {
        var paymentService = new PaymentService();

        Assert.Throws<InvalidOperationException>(() => paymentService.ProcessPayment(null));
    }

    [Fact]
    public void ProcessPayment_ThrowsInvalidOperationException_WhenOrderIsAlreadyPaid()
    {
        var paymentService = new PaymentService();
        var order = new Order { Id = 1, IsPaid = true };

        Assert.Throws<InvalidOperationException>(() => paymentService.ProcessPayment(order));
    }
}