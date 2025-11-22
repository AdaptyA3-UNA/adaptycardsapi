using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Adapty.API.Data;
// using Adapty.API.Data; // Você vai descomentar isso quando criar a classe AppDbContext

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do Banco de Dados (MySQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Configura o Entity Framework para usar MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Adicionar Controllers
builder.Services.AddControllers();

// 3. Configurar Swagger (Documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Configurar Pipeline de Requisição
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();