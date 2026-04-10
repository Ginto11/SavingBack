using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SavingBack.Configuration.Swagger;
using SavingBack.Database;
using SavingBack.Services;
using SavingBack.Utilities;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//CODIGO PARA SACAR LA KEY
//var key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

//SERVICIOS
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<Utilidad>();
builder.Services.AddScoped<MetaAhorroService>();
builder.Services.AddScoped<AhorroService>();
builder.Services.AddScoped<CorreoService>();
builder.Services.AddScoped<IngresoService>();
builder.Services.AddScoped<EgresoService>();
builder.Services.AddScoped<CategoriaGastoService>();
builder.Services.AddScoped<GraficaService>();
builder.Services.AddScoped<ReporteService>();

builder.Services.AddLogging();      //PERMITE AGREGAR LOGS (MENSAJE EN TIEMPO DE EJECUSION EN LA CONSOLA)
builder.Logging.ClearProviders();   //LIMPIA LOS LOGS POR DEFECTO QUE VIENEN 
builder.Logging.AddConsole();       //AGREGA LOGS DE CONSOLA PARA VER TEMAS DE AUTENTICACION VALIDADOA

//CONFIGURACION PARA AGREGAR LA AUTENTICACION MEDIANTE JWTBEARER
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

//CONFIGURACION PARA MANEJAR VERSIONAMIENTO DE APIS
builder.Services.AddApiVersioning(opciones =>
{
    opciones.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    opciones.AssumeDefaultVersionWhenUnspecified = true;
    opciones.ReportApiVersions = true;
    opciones.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(opciones =>
{
    opciones.GroupNameFormat = "'v'VVV";
    opciones.SubstituteApiVersionInUrl = true;
});

//CONFIGURACION PARA CONFIGURAR LA CONNECTIONSTRING CON EL DBCONTEXT
builder.Services.AddDbContext<AppDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

//CONFIGURACION PARA AGREGAR LOS CORS
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

//AGREGA LA CONFIFURACION DE SWAGGER
builder.Services.ConfigureOptions<ConfiguracionSwagger>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //TRAE LOS PROVEEDORES DE VERSIONES
    var providers = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(opciones =>
    {
        //POR CADA UNA DE LAS DESCRIONES SE CREA EL SWAGGER.JSON
        foreach (var descripcion in providers.ApiVersionDescriptions)
        {
            opciones.SwaggerEndpoint($"/swagger/{descripcion.GroupName}/swagger.json", descripcion.GroupName.ToUpperInvariant());
        }
    });
}

app.UseCors("Todos");



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();


app.Run();
