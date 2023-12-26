using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class EmpleadoMap : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> entity)
        {
            entity.ToTable("Empleados");

            entity.HasKey(e => e.IdEmpleado).HasName("PK_dbo_Empleados_IdEmpleado");

            entity.HasIndex(e => e.Identidad, "QU_dbo_Empleados_Identidad").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DireccionExacta)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdMunicipio)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Identidad)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoCivilNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEstadoCivil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Empleados_dbo_EsatdosCiviles_IdEstadoCivil");

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_dbo_Empleados_dbo_Municipios_IdMunicipio");

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.EmpleadoIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_Empleados_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.EmpleadoIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_Empleados_dbo_Usuarios_IdUsuarioModificacion");
        }
    }
}
