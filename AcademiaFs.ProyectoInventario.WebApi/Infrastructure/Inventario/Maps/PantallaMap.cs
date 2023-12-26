using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Maps
{
    public class PantallaMap : IEntityTypeConfiguration<Pantalla>
    {
        public void Configure(EntityTypeBuilder<Pantalla> entity)
        {
            entity.ToTable("Pantallas");

            entity.HasKey(e => e.IdPantalla).HasName("PK_dbo_Pantallas_IdPantalla");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Icono)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Menu)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(300)
                .IsUnicode(false);
        }
    }
}
