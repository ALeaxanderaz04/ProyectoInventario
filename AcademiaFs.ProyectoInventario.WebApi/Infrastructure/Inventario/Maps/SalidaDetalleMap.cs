using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class SalidaDetalleMap : IEntityTypeConfiguration<SalidaDetalle>
    {
        public void Configure(EntityTypeBuilder<SalidaDetalle> entity)
        {
            entity.ToTable("SalidasDetalle");

            entity.HasKey(e => e.IdSalidaDetalle).HasName("PK_dbo_SalidasDetalles_IdSalidaDetalle");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdSalidaDetalle).ValueGeneratedOnAdd();

            entity.HasOne(e => e.IdSalidaNavigation).WithMany(p => p.SalidaDetalles)
                .HasForeignKey(e => e.IdSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_SalidasDetalles_dbo_Salidas_IdSalida");


            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.SalidasDetalles)
                .HasForeignKey(d => d.IdLote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_SalidasDetalles_dbo_Lotes_IdLote");

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.SalidasDetalleIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_SalidasDetalles_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.SalidasDetalleIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_SalidasDetalles_dbo_Usuarios_IdUsuarioModificacion");
        }
    }
}
