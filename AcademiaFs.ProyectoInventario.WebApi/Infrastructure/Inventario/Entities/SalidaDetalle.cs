using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;

public partial class SalidaDetalle
{
    public int IdSalidaDetalle { get; set; }
    public int IdSalida { get; set; }
    public int IdLote { get; set; }
    public int CantidadProducto { get; set; }
    public int IdUsuarioCreacion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int? IdUsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public bool? Activo { get; set; }
    public virtual Lote IdLoteNavigation { get; set; } = null!;
    public virtual Salida IdSalidaNavigation { get; set; } = null!;
    public virtual Usuario IdUsuarioCreacionNavigation { get; set; } = null!;
    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }
}
