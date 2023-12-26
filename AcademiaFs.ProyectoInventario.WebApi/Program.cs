using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using Farsiman.Domain.Core.Standard;
using Farsiman.Extensions.Configuration;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
     builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForFsIdentityServer(opt =>
{
    opt.Title = "Prueba";
    opt.Description = "Descripcion";
    opt.Version = "v1.0";
});

builder.Services.AddFsAuthService(configureOptions =>
{
    configureOptions.Username = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Username");
    configureOptions.Password = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Password");
});

var connectionString = builder.Configuration.GetConnectionString("EFCoreTransporte");
builder.Services.AddDbContext<InventarioCaazContext>(opciones => opciones.UseSqlServer(connectionString));

builder.Services.AddTransient<UnitOfworkBuilder, UnitOfworkBuilder>();

builder.Services.AddAutoMapper(typeof(MapProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithFsIdentityServer();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowSpecificOrigin");
app.MapControllers();

app.Run();

