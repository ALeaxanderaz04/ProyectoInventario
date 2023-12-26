using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Identidad { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public int IdEstadoCivil { get; set; }

    public string IdMunicipio { get; set; } = null!;

    public string DireccionExacta { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int IdUsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual EstadoCivil IdEstadoCivilNavigation { get; set; } = null!;

    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
