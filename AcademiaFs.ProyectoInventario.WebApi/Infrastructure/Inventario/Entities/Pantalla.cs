using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;

public partial class Pantalla
{
    public int IdPantalla { get; set; }

    public string Nombre { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Menu { get; set; } = null!;

    public string Icono { get; set; } = null!;

    public int IdUsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<PantallaPorRol> PantallasPorRols { get; set; } = new List<PantallaPorRol>();
}
