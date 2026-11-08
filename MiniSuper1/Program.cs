//Usando las librerías necesarias para el proyecto
using Scalar.AspNetCore;

// Aca se crea la aplicación web y se configura el entorno de desarrollo, los servicios y los controladores para manejar las solicitudes HTTP.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Agregar Servicios
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Agregamos el servicio para generar la documentación de la API en formato OpenAPI (Swagger)
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//Agregamos el middleware para redirigir las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

//Mapeamos las rutas para que reconozcan los controladores y sus acciones 
app.MapControllers();

//Iniciamos la aplicación
app.Run();
