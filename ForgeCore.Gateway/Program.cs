using ForgeCore.Infrastructure.Persistence;
using ForgeCore.Infrastructure.Repositories;
using ForgeCore.Players.Application;
using ForgeCore.Players.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ForgeCoreDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("ForgeCoreDb")
    )
);

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
