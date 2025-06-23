using Microsoft.EntityFrameworkCore;
using UserOrderSystem.Models;

namespace UserOrderSystem.Data;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
}