using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;

public partial class Sucursal
{
    public int IdSucursal { get; set; }
    public string Nombre { get; set; } = null!;
    public string IdMunicipio { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public int IdUsuarioCreacion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int? IdUsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public bool Activo { get; set; }
    public virtual Municipio IdMunicipioNavigation { get; set; } = null!;
    public virtual Usuario IdUsuarioCreacionNavigation { get; set; } = null!;
    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }
    public virtual ICollection<Salida> Salida { get; set; } = new List<Salida>();
}
