using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class LoteMap : IEntityTypeConfiguration<Lote>
    {
        public void Configure(EntityTypeBuilder<Lote> entity)
        {
            entity.ToTable("Lotes");

            entity.HasKey(e => e.IdLote).HasName("PK_dbo_Lotes_IdLote");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CostoUnidad).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Lotes_dbo_Productos_IdProducto");

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.LoteIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Lotes_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.LoteIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_Lotes_dbo_Usuarios_IdUsuarioModificacion");
        }
    }
}
