using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class MunicipioMap : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> entity)
        {
            entity.ToTable("Municipios");

            entity.HasKey(e => e.IdMunicipio).HasName("PK_dbo_Municipios_IdMunicipio");

            entity.Property(e => e.IdMunicipio)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdDepartamento)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Municipios_dbo_Departamentos_IdDepartamento");

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.MunicipioIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Municipios_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.MunicipioIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_Municipios_dbo_Usuarios_IdUsuarioModificacion");
        }
    }
}
