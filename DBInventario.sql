CREATE DATABASE Inventario_CAAZ
GO 

USE Inventario_CAAZ
GO


--***************** ACCESO *******************--

GO
CREATE TABLE Roles(
  IdRol					INT IDENTITY,  
  Nombre				VARCHAR(100) UNIQUE		NOT NULL,
  IdUsuarioCreacion		INT						NOT NULL,
  FechaCreacion			DATETIME				NOT NULL,
  IdUsuarioModificacion	INT						NULL,
  FechaModificacion		DATETIME				NULL,
  Activo				BIT						NOT NULL CONSTRAINT DF_dbo_Roles_Activo DEFAULT(1)
  CONSTRAINT PK_dbo_Roles_IdRol PRIMARY KEY(IdRol)
);

GO
CREATE TABLE Pantallas(
  IdPantalla			INT IDENTITY,
  Nombre				VARCHAR(100)	NOT NULL,
  [Url]					VARCHAR(300)	NOT NULL,
  Menu					VARCHAR(300)	NOT NULL,
  Icono					VARCHAR(300)	NOT NULL,
  IdUsuarioCreacion		INT				NOT NULL,
  FechaCreacion			DATETIME		NOT NULL,
  IdUsuarioModificacion	INT				NULL,
  FechaModificacion		DATETIME		NULL,
  Activo				BIT				NOT NULL CONSTRAINT DF_dbo_Pantallas_Activo DEFAULT(1)
  CONSTRAINT PK_dbo_Pantallas_IdPantalla PRIMARY KEY(IdPantalla)
);

GO
CREATE TABLE PantallasPorRol(
  IdPantallasPorRol		INT IDENTITY,
  IdRol					INT			NOT NULL,
  IdPantalla			INT			NOT NULL,
  IdUsuarioCreacion		INT			NOT NULL,
  FechaCreacion			DATETIME	NOT NULL ,
  IdUsuarioModificacion	INT			NULL,
  FechaModificacion		DATETIME	NULL,
  Activo				BIT			NOT NULL CONSTRAINT DF_dbo_PantallasPorRol_Activo DEFAULT(1)

  CONSTRAINT FK_dbo_PantallasPorRol_dbo_Roles_IdRol				FOREIGN KEY(IdRol)			REFERENCES Roles(IdRol),
  CONSTRAINT FK_dbo_PantallasPorRol_dbo_Pantallas_IdPantalla	FOREIGN KEY(IdPantalla)		REFERENCES Pantallas(IdPantalla),
  CONSTRAINT PK_dbo_PantallasPorRol_IdPantallasPorRol			PRIMARY KEY(IdPantallasPorRol)
);


GO
CREATE TABLE Usuarios(
  IdUsuario				INT IDENTITY(1,1),
  NombreUsuario			VARCHAR(100)	NOT NULL,
  Contrasena			VARBINARY		NOT NULL,
  EsAdmin				BIT,
  IdEmpleado			INT				NULL,
  IdRol					INT				NULL,
  IdUsuarioCreacion		INT				NOT NULL,
  FechaCreacion			DATETIME		NOT NULL,
  IdUsuarioModificacion	INT				NULL,
  FechaModificacion		DATETIME		NULL,
  Activo				BIT NOT NULL CONSTRAINT DF_dbo_Usuarios_Activo DEFAULT(1)
  CONSTRAINT PK_dbo_Usuarios_IdUsuario		PRIMARY KEY(IdUsuario),
  CONSTRAINT UQ_dbo_Usuarios_NombreUsuario	UNIQUE(NombreUsuario)
);

--***************** GENERALES *******************--

GO
CREATE TABLE Departamentos(
IdDepartamento			VARCHAR(2)			NOT NULL,
Nombre					VARCHAR(100)		NOT NULL,
IdUsuarioCreacion		INT					NOT NULL,
FechaCreacion			DATETIME			NOT NULL,
IdUsuarioModificacion	INT					NULL,
FechaModificacion		DATETIME			NULL,
Activo					BIT CONSTRAINT DF_dbo_Departamentos_Activo DEFAULT (1)

CONSTRAINT PK_dbo_Departamentos_IdDepartamento						PRIMARY KEY (IdDepartamento),
CONSTRAINT FK_dbo_Departementos_dbo_Usuarios_IdUsuarioCreacion		FOREIGN KEY	(IdUsuarioCreacion)		REFERENCES	Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Departementos_dbo_Usuarios_IdUsuaroModificacion	FOREIGN KEY	(IdUsuarioModificacion) REFERENCES	Usuarios(IdUsuario),
)

GO 
CREATE TABLE Municipios(
IdMunicipio				VARCHAR(4)		NOT NULL,
Nombre					VARCHAR(100)	NOT NULL,
IdDepartamento			VARCHAR(2)		NOT NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_Municipios_Activo DEFAULT (1)

CONSTRAINT PK_dbo_Municipios_IdMunicipio							PRIMARY KEY(IdMunicipio),
CONSTRAINT FK_dbo_Municipios_dbo_Departamentos_IdDepartamento		FOREIGN KEY(IdDepartamento)				REFERENCES Departamentos(IdDepartamento),
CONSTRAINT FK_dbo_Municipios_dbo_Usuarios_IdUsuarioCreacion			FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Municipios_dbo_Usuarios_IdUsuarioModificacion		FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)
)

GO
CREATE TABLE EstadosCiviles (
IdEstadoCivil			INT IDENTITY(1,1),
Nombre					VARCHAR(100)	NOT NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_EstadosCiviles_Activo DEFAULT (1)

CONSTRAINT PK_dbo_EstadosCiviles_IdEstadoCivil						PRIMARY KEY(IdEstadoCivil)
CONSTRAINT FK_dbo_EstadosCiviles_dbo_Usuarios_IdUsuarioCreacion		FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_EstadosCiviles_dbo_Usuarios_IdUsuarioModificacion	FOREIGN KEY(IdUsuarioModificacion)      REFERENCES Usuarios(IdUsuario),
)

GO
CREATE TABLE Empleados(
IdEmpleado					INT IDENTITY(1,1),
Nombres						VARCHAR(200)	NOT NULL,
Apellidos					VARCHAR(200)	NOT NULL,
Identidad					VARCHAR(15)		NOT NULL,
FechaNacimiento				DATE			NOT NULL,
Sexo						CHAR(1)			NOT NULL,
IdEstadoCivil				INT				NOT NULL,
IdMunicipio					VARCHAR(4)		NOT NULL,
DireccionExacta				VARCHAR(250)	NOT NULL,
Telefono					VARCHAR(20)		NOT NULL,
IdUsuarioCreacion			INT				NOT NULL,
FechaCreacion				DATETIME		NOT NULL,
IdUsuarioModificacion		INT				NULL,
FechaModificacion			DATETIME		NULL,
Activo						BIT				NOT NULL CONSTRAINT DF_dbo_Empleados_Activo DEFAULT(1),
  
CONSTRAINT PK_dbo_Empleados_IdEmpleado							PRIMARY KEY(IdEmpleado),
CONSTRAINT CK_dbo_Empleados_Sexo								CHECK(Sexo IN ('F', 'M')),
CONSTRAINT QU_dbo_Empleados_Identidad							UNIQUE(Identidad),
CONSTRAINT Fk_dbo_Empleados_dbo_Municipios_IdMunicipio			FOREIGN KEY(IdMunicipio)				REFERENCES Municipios(IdMunicipio),
CONSTRAINT FK_dbo_Empleados_dbo_EsatdosCiviles_IdEstadoCivil	FOREIGN KEY(IdEstadocivil)				REFERENCES EstadosCiviles(IdEstadoCivil),
CONSTRAINT FK_dbo_Empleados_dbo_Usuarios_IdUsuarioCreacion		FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Empleados_dbo_Usuarios_IdUsuarioModificacion	FOREIGN KEY(IdUsuarioModificacion)      REFERENCES Usuarios(IdUsuario),
);

--***************** INVENTARIO *******************--

GO
CREATE TABLE Sucursales(
IdSucursal				INT IDENTITY(1,1),
Nombre					VARCHAR(200)	NOT NULL,
IdMunicipio				VARCHAR(4)		NOT NULL,
Direccion				VARCHAR(200)	NOT NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_Sucursales_Activo DEFAULT (1)

CONSTRAINT PK_dbo_Sucursales_IdSucursal								PRIMARY KEY(IdSucursal),
CONSTRAINT FK_dbo_Sucursales_dbo_Muncipios_IdMunicipio				FOREIGN KEY(IdMunicipio)					REFERENCES Municipios(IdMunicipio),
CONSTRAINT FK_dbo_Sucursales_dbo_Usuarios_IdUsuarioCreacion			FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Sucursales_dbo_Usuarios_IdUsuarioModificacion     FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)
);

CREATE TABLE Productos(
IdProducto				INT IDENTITY(1,1),
Nombre					VARCHAR(150)	NOT NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_Productos_Activo DEFAULT (1)

CONSTRAINT PK_dbo_Productos_IdProducto								PRIMARY KEY(IdProducto),
CONSTRAINT FK_dbo_Productos_dbo_Usuarios_IdUsuarioCreacion			FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Productos_dbo_Usuarios_IdUsuarioModificacion		FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)
)


CREATE TABLE EstadoEnvios(
IdEstadoEnvio			INT IDENTITY(1,1),
Nombre					VARCHAR(100)	NOT NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_EstadoEnvios_Activo DEFAULT (1)

CONSTRAINT PK_dbo_EstadoEnvios_IdEstadoEnvio							PRIMARY KEY(IdEstadoEnvio),
CONSTRAINT FK_dbo_EstadoEnvios_dbo_Usuarios_IdUsuarioCreacion			FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_EstadoEnvios_dbo_Usuarios_IdUsuarioModificacion		FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)
)


CREATE TABLE Lotes(
IdLote					INT IDENTITY(1,1),
IdProducto				INT				NOT NULL, 
CantidadIngresada		INT				NOT NULL,
CostoUnidad				DECIMAL(18,2)	NOT NULL,
FechaVencimiento		DATETIME		NOT NULL,
CantidadActual			INT				NOT NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_Lotes_Activo DEFAULT (1)

CONSTRAINT PK_dbo_Lotes_IdLote									PRIMARY KEY(IdLote),
CONSTRAINT FK_dbo_Lotes_dbo_Productos_IdProducto				FOREIGN KEY(Idproducto)					REFERENCES Productos(IdProducto),
CONSTRAINT FK_dbo_Lotes_dbo_Usuarios_IdUsuarioCreacion			FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Lotes_dbo_Usuarios_IdUsuarioModificacion		FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)
)


CREATE TABLE Salidas(
IdSalida				INT IDENTITY (1,1),
IdSucursal				INT				NOT NULL,
FechaSalida				DATETIME		NOT NULL,
Total					DECIMAL(18,2)	NOT NULL,
IdEstadoEnvio			INT				NOT NULL,
FechaRecibido			DATETIME		NULL,
IdUsuarioRecibe			INT				NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_Salidas_Activo DEFAULT (1)

CONSTRAINT PK_dbo_Salidas_IdSalida									PRIMARY KEY(IdSalida),
CONSTRAINT FK_dbo_Salidas_dbo_Sucursales_IdSucursal					FOREIGN KEY(IdSucursal)					REFERENCES Sucursales(IdSucursal),
CONSTRAINT FK_dbo_Salidas_dbo_EstadoEnvios_IdEstadoEnvio			FOREIGN KEY(IdEstadoEnvio)				REFERENCES EstadoEnvios(IdEstadoEnvio),
CONSTRAINT FK_dbo_Salidas_dbo_Usuarios_IdUsuarioRecibe				FOREIGN KEY(IdUsuarioRecibe)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Salidas_dbo_Usuarios_IdUsuarioCreacion			FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_Salidas_dbo_Usuarios_IdUsuarioModificacion		FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)
)

CREATE TABLE SalidasDetalle(
IdSalidaDetalle			INT IDENTITY(1,1),
IdSalida				INT NOT NULL,
IdLote					INT NOT NULL,
CantidadProducto		INT NOT NULL,
IdUsuarioCreacion		INT             NOT NULL,
FechaCreacion			DATETIME        NOT NULL,
IdUsuarioModificacion	INT				NULL,
FechaModificacion		DATETIME		NULL,
Activo					BIT             CONSTRAINT DF_dbo_SalidasDetalle_Activo DEFAULT (1)

CONSTRAINT PK_dbo_SalidasDetalles_IdSalidaDetalle							PRIMARY KEY(IdSalida),
CONSTRAINT FK_dbo_SalidasDetalles_dbo_Lotes_IdLote							FOREIGN KEY(IdLote)						REFERENCES Lotes(IdLote),
CONSTRAINT FK_dbo_SalidasDetalles_dbo_Usuarios_IdUsuarioCreacion			FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
CONSTRAINT FK_dbo_SalidasDetalles_dbo_Usuarios_IdUsuarioModificacion		FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)
)


--***************** INSERT NECESARIOS *******************--

GO
INSERT INTO Usuarios(NombreUsuario,Contrasena,EsAdmin,IdEmpleado,IdRol,IdUsuarioCreacion,FechaCreacion)
VALUES('caaz',1,1,1,1,1,'2023-12-19');

GO
INSERT INTO Roles(Nombre, IdUsuarioCreacion, FechaCreacion)
VALUES  ('Jefe de Bodega', 1, '2023-12-19');

GO
INSERT INTO Departamentos(IdDepartamento, Nombre, Activo, IdUsuarioCreacion, FechaCreacion, IdUsuarioModificacion, FechaModificacion)
VALUES	('01','Atlántida',				'1', 1, GETDATE(), NULL, NULL),
		('02','Colón',					'1', 1, GETDATE(), NULL, NULL),
		('03','Comayagua',				'1', 1, GETDATE(), NULL,NULL),
		('04','Copán',					'1', 1, GETDATE(), NULL, NULL),
		('05','Cortés',					'1', 1, GETDATE(), NULL, NULL),
		('06','Choluteca',				'1', 1, GETDATE(), NULL, NULL),
		('07','El Paraíso',				'1', 1, GETDATE(), NULL, NULL),
		('08','Francisco Morazán',		'1', 1, GETDATE(), NULL, NULL),
		('09','Gracias a Dios',			'1', 1, GETDATE(), NULL, NULL),
		('10','Intibucá',				'1', 1, GETDATE(), NULL, NULL),
		('11','Islas de la Bahía',		'1', 1, GETDATE(), NULL, NULL),
		('12','La Paz',					'1', 1, GETDATE(), NULL, NULL),
		('13','Lempira',				'1', 1, GETDATE(), NULL,NULL ),
		('14','Ocotepeque',				'1', 1, GETDATE(), NULL, NULL),
		('15','Olancho',				'1', 1, GETDATE(), NULL, NULL),
		('16','Santa Bárbara',			'1', 1, GETDATE(), NULL, NULL),
		('17','Valle',					'1', 1, GETDATE(), NULL, NULL),
		('18','Yoro',					'1', 1, GETDATE(), NULL, NULL);

GO
INSERT INTO Municipios(IdMunicipio, Nombre, IdDepartamento, IdUsuarioCreacion, FechaCreacion)
VALUES ('0101','La Ceiba','01',1,'2023-12-26');

GO
INSERT INTO EstadosCiviles(Nombre, IdUsuarioCreacion, FechaCreacion)
VALUES	('Soltero(a)',1,'2023-12-26'),
		('Casado(a)',1,'2023-12-26');

GO
INSERT INTO Empleados(Nombres, Apellidos, Identidad, FechaNacimiento, Sexo, IdEstadoCivil, IdMunicipio, DireccionExacta, Telefono, IdUsuarioCreacion, FechaCreacion)
VALUES ('Cristian Alexander', 'Aguilar Zuniga', '0311200400203','2004-10-16','M',2,'0101','Frente al parque central','88264741',1,'2023-12-26');


--***************** ALTER TABLE *******************--

GO
ALTER TABLE Roles
ADD CONSTRAINT FK_dbo_Roles_dbo_Usuarios_IdUsuarioCreacion		FOREIGN KEY(IdUsuarioCreacion)		REFERENCES Usuarios(IdUsuario),
	CONSTRAINT FK_dbo_Roles_dbo_Usuarios_IdUsuarioModificacion	FOREIGN KEY(IdUsuarioModificacion)	REFERENCES Usuarios(IdUsuario);

GO 
ALTER TABLE [PantallasPorRol]
ADD CONSTRAINT	FK_dbo_PantallasPorRol_dbo_Usuarios_IdUsuarioCreacion		FOREIGN KEY(IdUsuarioCreacion)			REFERENCES Usuarios(IdUsuario),
	CONSTRAINT	FK_dbo_PantallasPorRol_dbo_Usuarios_IdUsuarioModificacion	FOREIGN KEY(IdUsuarioModificacion)		REFERENCES Usuarios(IdUsuario)

GO
ALTER TABLE [Usuarios]
ADD CONSTRAINT FK_dbo_Usuarios_dbo_Usuarios_IdUsuarioCreacion		FOREIGN KEY(IdUsuarioCreacion)		REFERENCES Usuarios(IdUsuario),
	CONSTRAINT FK_dbo_Usuarios_dbo_Usuarios_IdUsuarioModificacion	FOREIGN KEY(IdUsuarioModificacion)	REFERENCES Usuarios(IdUsuario),
	CONSTRAINT FK_dbo_Usuarios_dbo_Roles_IdRol						FOREIGN KEY(IdRol)					REFERENCES Roles(IdRol),
	CONSTRAINT FK_dbo_Usuarios_dbo_Empleados_IdEmpleado				FOREIGN KEY(IdEmpleado)				REFERENCES Empleados(IdEmpleado)
