
using Microsoft.EntityFrameworkCore;
using MoveisMedeiros.Data;
using MoveisMedeiros.Services;

var builder = WebApplication.CreateBuilder(args);

// Conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers();
builder.Services.AddScoped<TokenService>();

// Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// CORS - Permitir conexão com o frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberarFrontend", policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:5500",
            "http://localhost:5500"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Permitir comunicação com o frontend
app.UseCors("LiberarFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
