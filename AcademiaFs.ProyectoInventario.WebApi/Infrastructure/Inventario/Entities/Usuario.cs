using System;
using System.Collections.Generic;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public byte[] Contrasena { get; set; } = null!;

    public bool? EsAdmin { get; set; }

    public int? IdEmpleado { get; set; }

    public int? IdRol { get; set; }

    public int IdUsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? IdUsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Departamento> DepartamentoIdUsuarioCreacionNavigations { get; set; } = new List<Departamento>();

    public virtual ICollection<Departamento> DepartamentoIdUsuarioModificacionNavigations { get; set; } = new List<Departamento>();

    public virtual ICollection<Empleado> EmpleadoIdUsuarioCreacionNavigations { get; set; } = new List<Empleado>();

    public virtual ICollection<Empleado> EmpleadoIdUsuarioModificacionNavigations { get; set; } = new List<Empleado>();

    public virtual ICollection<EstadoEnvio> EstadoEnvioIdUsuarioCreacionNavigations { get; set; } = new List<EstadoEnvio>();

    public virtual ICollection<EstadoEnvio> EstadoEnvioIdUsuarioModificacionNavigations { get; set; } = new List<EstadoEnvio>();

    public virtual ICollection<EstadoCivil> EstadosCivileIdUsuarioCreacionNavigations { get; set; } = new List<EstadoCivil>();

    public virtual ICollection<EstadoCivil> EstadosCivileIdUsuarioModificacionNavigations { get; set; } = new List<EstadoCivil>();

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual Usuario IdUsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioModificacionNavigation { get; set; }

    public virtual ICollection<Usuario> InverseIdUsuarioCreacionNavigation { get; set; } = new List<Usuario>();

    public virtual ICollection<Usuario> InverseIdUsuarioModificacionNavigation { get; set; } = new List<Usuario>();

    public virtual ICollection<Lote> LoteIdUsuarioCreacionNavigations { get; set; } = new List<Lote>();

    public virtual ICollection<Lote> LoteIdUsuarioModificacionNavigations { get; set; } = new List<Lote>();

    public virtual ICollection<Municipio> MunicipioIdUsuarioCreacionNavigations { get; set; } = new List<Municipio>();

    public virtual ICollection<Municipio> MunicipioIdUsuarioModificacionNavigations { get; set; } = new List<Municipio>();

    public virtual ICollection<PantallaPorRol> PantallasPorRolIdUsuarioCreacionNavigations { get; set; } = new List<PantallaPorRol>();

    public virtual ICollection<PantallaPorRol> PantallasPorRolIdUsuarioModificacionNavigations { get; set; } = new List<PantallaPorRol>();

    public virtual ICollection<Producto> ProductoIdUsuarioCreacionNavigations { get; set; } = new List<Producto>();

    public virtual ICollection<Producto> ProductoIdUsuarioModificacionNavigations { get; set; } = new List<Producto>();

    public virtual ICollection<Rol> RoleIdUsuarioCreacionNavigations { get; set; } = new List<Rol>();

    public virtual ICollection<Rol> RoleIdUsuarioModificacionNavigations { get; set; } = new List<Rol>();

    public virtual ICollection<Salida> SalidaIdUsuarioCreacionNavigations { get; set; } = new List<Salida>();

    public virtual ICollection<Salida> SalidaIdUsuarioModificacionNavigations { get; set; } = new List<Salida>();

    public virtual ICollection<Salida> SalidaIdUsuarioRecibeNavigations { get; set; } = new List<Salida>();

    public virtual ICollection<SalidaDetalle> SalidasDetalleIdUsuarioCreacionNavigations { get; set; } = new List<SalidaDetalle>();

    public virtual ICollection<SalidaDetalle> SalidasDetalleIdUsuarioModificacionNavigations { get; set; } = new List<SalidaDetalle>();

    public virtual ICollection<Sucursal> SucursaleIdUsuarioCreacionNavigations { get; set; } = new List<Sucursal>();

    public virtual ICollection<Sucursal> SucursaleIdUsuarioModificacionNavigations { get; set; } = new List<Sucursal>();
}
