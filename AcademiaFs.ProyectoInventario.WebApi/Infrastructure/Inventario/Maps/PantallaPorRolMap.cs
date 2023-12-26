using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class PantallaPorRolMap : IEntityTypeConfiguration<PantallaPorRol>
    {
        public void Configure(EntityTypeBuilder<PantallaPorRol> entity)
        {
            entity.ToTable("PantallasPorRol");

            entity.HasKey(e => e.IdPantallasPorRol).HasName("PK_dbo_PantallasPorRol_IdPantallasPorRol");

            entity.ToTable("PantallasPorRol");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdPantallaNavigation).WithMany(p => p.PantallasPorRols)
                .HasForeignKey(d => d.IdPantalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_PantallasPorRol_dbo_Pantallas_IdPantalla");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.PantallasPorRols)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_PantallasPorRol_dbo_Roles_IdRol");

            entity.HasOne(d => d.IdUsuarioCreacionNavigation).WithMany(p => p.PantallasPorRolIdUsuarioCreacionNavigations)
                .HasForeignKey(d => d.IdUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo_PantallasPorRol_dbo_Usuarios_IdUsuarioCreacion");

            entity.HasOne(d => d.IdUsuarioModificacionNavigation).WithMany(p => p.PantallasPorRolIdUsuarioModificacionNavigations)
                .HasForeignKey(d => d.IdUsuarioModificacion)
                .HasConstraintName("FK_dbo_PantallasPorRol_dbo_Usuarios_IdUsuarioModificacion");
        }
    }
}
