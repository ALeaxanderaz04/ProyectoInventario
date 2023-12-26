using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class EstadoEnvioMap : IEntityTypeConfiguration<EstadoEnvio>
    {
        public void Configure(EntityTypeBuilder<EstadoEnvio> entity)
        {
            entity.ToTable("EstadoEnvios");

            entity.HasKey(e => e.IdEstadoEnvio).HasName("PK_dbo_EstadoEnvios_IdEstadoEnvio");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.EstadoEnvioIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_EstadoEnvios_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.EstadoEnvioIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_EstadoEnvios_dbo_Usuarios_IdUsuarioModificacion");
        }
    }
}
