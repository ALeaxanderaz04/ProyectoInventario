using System;
using System.Collections.Generic;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;

public partial class InventarioCaazContext : DbContext
{
    public InventarioCaazContext()
    {
    }

    public InventarioCaazContext(DbContextOptions<InventarioCaazContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }
    public virtual DbSet<Empleado> Empleados { get; set; }
    public virtual DbSet<EstadoEnvio> EstadoEnvios { get; set; }
    public virtual DbSet<EstadoCivil> EstadosCiviles { get; set; }
    public virtual DbSet<Lote> Lotes { get; set; }
    public virtual DbSet<Municipio> Municipios { get; set; }
    public virtual DbSet<Pantalla> Pantallas { get; set; }
    public virtual DbSet<PantallaPorRol> PantallasPorRols { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Rol> Roles { get; set; }
    public virtual DbSet<Salida> Salidas { get; set; }
    public virtual DbSet<SalidaDetalle> SalidasDetalles { get; set; }
    public virtual DbSet<Sucursal> Sucursales { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DepartamentoMap());
        modelBuilder.ApplyConfiguration(new EmpleadoMap());
        modelBuilder.ApplyConfiguration(new EstadoEnvioMap());
        modelBuilder.ApplyConfiguration(new EstadoCivilMap());
        modelBuilder.ApplyConfiguration(new LoteMap());
        modelBuilder.ApplyConfiguration(new MunicipioMap());
        modelBuilder.ApplyConfiguration(new PantallaMap());
        modelBuilder.ApplyConfiguration(new PantallaPorRolMap());
        modelBuilder.ApplyConfiguration(new ProductoMap());
        modelBuilder.ApplyConfiguration(new RolMap());
        modelBuilder.ApplyConfiguration(new SalidaMap());
        modelBuilder.ApplyConfiguration(new SalidaDetalleMap());
        modelBuilder.ApplyConfiguration(new SucursalMap());
        modelBuilder.ApplyConfiguration(new UsuarioMap());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
