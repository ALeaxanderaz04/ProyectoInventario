using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;

public partial class Salida
{
    public int IdSalida { get; set; }

    public int IdSucursal { get; set; }

    public DateTime FechaSalida { get; set; }

    public decimal Total { get; set; }

    public DateTime? FechaRecibido { get; set; }

    public int? IdUsuarioRecibe { get; set; }

    public int IdUsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioRecibeNavigation { get; set; }
}
