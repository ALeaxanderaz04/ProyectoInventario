using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class SalidaMap : IEntityTypeConfiguration<Salida>
    {
        public void Configure(EntityTypeBuilder<Salida> entity)
        {
            entity.ToTable("Salidas");

            entity.HasKey(e => e.IdSalida).HasName("PK_dbo_Salidas_IdSalida");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRecibido).HasColumnType("datetime");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Salida)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Salidas_dbo_Sucursales_IdSucursal");

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.SalidaIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Salidas_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.SalidaIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_Salidas_dbo_Usuarios_IdUsuarioModificacion");

            entity.HasOne(d => d.IdUsuarioRecibeNavigation).WithMany(p => p.SalidaIdUsuarioRecibeNavigations)
                .HasForeignKey(d => d.IdUsuarioRecibe)
                .HasConstraintName("FK_dbo_Salidas_dbo_Usuarios_IdUsuarioRecibe");
        }
    }
}
