using Microsoft.EntityFrameworkCore;
using UserOrderSystem.Data;
using UserOrderSystem.Interfaces;
using UserOrderSystem.Models;
using UserOrderSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Register InMemory DB
builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseInMemoryDatabase("OrdersDB"));

// Register Services and Repositories
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddSingleton<IUserRepository, DummyUserRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();

public class DummyUserRepository : IUserRepository
{
    private readonly List<User> _users = [
        new() { Id = 1, Email = "sagunchauhan@harakirimail.com", Password = "Sagun1" },
        new() { Id = 2, Email = "sagunchauhan@yopmail.com", Password = "Sagun1234" }
    ];

    public User? GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);
    public List<User> GetAllUsers() => _users;
}