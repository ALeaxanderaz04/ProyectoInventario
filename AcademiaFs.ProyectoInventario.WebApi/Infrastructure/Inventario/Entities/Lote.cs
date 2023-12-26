using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;

public partial class Lote
{
    public int IdLote { get; set; }

    public int IdProducto { get; set; }

    public int CantidadIngresada { get; set; }

    public decimal CostoUnidad { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public int CantidadActual { get; set; }

    public int IdUsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual ICollection<SalidaDetalle> SalidasDetalles { get; set; } = new List<SalidaDetalle>();
}
