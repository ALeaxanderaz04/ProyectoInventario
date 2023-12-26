using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class EstadoCivilMap : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(EntityTypeBuilder<EstadoCivil> entity)
        {
            entity.ToTable("EstadosCiviles");

            entity.HasKey(e => e.IdEstadoCivil).HasName("PK_dbo_EstadosCiviles_IdEstadoCivil");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.EstadosCivileIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_EstadosCiviles_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.EstadosCivileIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_EstadosCiviles_dbo_Usuarios_IdUsuarioModificacion");
        }
    }
}
