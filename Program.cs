using Microsoft.EntityFrameworkCore;
using TicketTrackerApi.Data;
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//FOR SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//AND AUTH
builder.Services.AddAuthorization();
//ADN CONTROLLERS
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // added this two since when i ran the dotnet i couldnt test it
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();