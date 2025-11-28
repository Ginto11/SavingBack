using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SavingBack.Controllers;
using SavingBack.Database;
using SavingBack.Services;
using SavingBack.Utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<Utilidad>();
builder.Services.AddScoped<MetaAhorroService>();
builder.Services.AddScoped<AhorroService>();
builder.Services.AddScoped<CorreoService>();

builder.Services.AddLogging();      //PERMITE AGREGAR LOGS (MENSAJE EN TIEMPO DE EJECUSION EN LA CONSOLA)
builder.Logging.ClearProviders();   //LIMPIA LOS LOGS POR DEFECTO QUE VIENEN 
builder.Logging.AddConsole();       //AGREGA LOGS DE CONSOLA PARA VER TEMAS DE AUTENTICACION VALIDADOA

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones =>
{
    opciones.RequireHttpsMetadata = false;
    opciones.SaveToken = true;
    opciones.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});


builder.Services.AddDbContext<AppDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("Todos", app =>
    {
        app.WithOrigins("https://saving-front.vercel.app", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Todos");



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
