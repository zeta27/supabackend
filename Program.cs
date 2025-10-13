using Microsoft.EntityFrameworkCore;
using supa.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<SUPADbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSUPA")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//HABILITAR CORS para Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "http://148.226.168.138"
            )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// APLICAR CORS aquí
app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
