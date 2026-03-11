DROP DATABASE TallerLaUnion

CREATE DATABASE TallerLaUnion
GO

USE TallerLaUnion
GO

/*
 ROLES Y USUARIOS*/
 CREATE TABLE Estado(
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    Estado varchar(20)
);

CREATE TABLE Roles (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    NombreRol VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(200)
);
drop table Usuarios
CREATE TABLE Usuarios (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto VARCHAR(120) NOT NULL,
    Cedula VARCHAR(50),
    Correo VARCHAR(120) UNIQUE NOT NULL,
    UsuarioLogin VARCHAR(50) UNIQUE NOT NULL,
    Contrasenna VARCHAR(250) NOT NULL,
    Estado INT,
    FechaRegistro DATETIME DEFAULT GETDATE(),
   NombreRol INT NOT NULL,
    FOREIGN KEY (NombreRol) REFERENCES Roles(Consecutivo),
    FOREIGN KEY (Estado) REFERENCES Estado(Consecutivo)
);

drop table Usuarios;
--CREATE TABLE RecuperacionContrasena (
--    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
--    IdUsuario INT NOT NULL,
--    NuevaContra VARCHAR(200) NOT NULL,
--    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Consecutivo)
--);

/*
 CLIENTES Y VEHICULOS*/

CREATE TABLE Vehiculos (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Cliente VARCHAR(100),
    Telefono INT,
    Cedula VARCHAR(100),
    Placa VARCHAR(20) UNIQUE NOT NULL,
    Marca VARCHAR(50),
    Modelo VARCHAR(50),
    Anio INT,
    Problema VARCHAR(500),
    Revision VARCHAR(500),
    Estado INT,
    FOREIGN KEY (Estado) REFERENCES Estado(Consecutivo)
);

CREATE TABLE HistorialAutomovil (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    IdVehiculo INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Descripcion VARCHAR(300),
    Repuestos varchar(300)
    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculos(Consecutivo)
);

/*
= CITAS*/
drop table Citas
CREATE TABLE Citas (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Cliente VARCHAR(100),
    Cedula VARCHAR(100),
    FechaCita DATE NOT NULL,
    HoraCita TIME NOT NULL,
    Telefono INT,
    Email VARCHAR(100),
    Servicio VARCHAR(200),
    Estado INT,
    CreadaPor INT NOT NULL,
    FOREIGN KEY (CreadaPor) REFERENCES Usuarios(Consecutivo),
    FOREIGN KEY (Estado) REFERENCES Estado(Consecutivo)
);
drop table  CitasHistorial
CREATE TABLE CitasHistorial (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    IdCita INT NOT NULL,
    FechaCambio DATETIME DEFAULT GETDATE(),
    EstadoAnterior VARCHAR(20),
    EstadoNuevo VARCHAR(20),
    CambiadoPor INT NOT NULL,
    FOREIGN KEY (IdCita) REFERENCES Citas(Consecutivo),
    FOREIGN KEY (CambiadoPor) REFERENCES Usuarios(Consecutivo)
);

/*INVENTARIO*/

CREATE TABLE Proveedores (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Correo VARCHAR(120),
    Direccion VARCHAR(200)
);

CREATE TABLE Productos (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(120) NOT NULL,
    IdArticulo Varchar (50) NOT NULL,
    Descripcion VARCHAR(200),
    PrecioCompra DECIMAL(10,2) NOT NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    StockMinimo INT DEFAULT 5,
    IdProveedor INT,
    FOREIGN KEY (IdProveedor) REFERENCES Proveedores(Consecutivo)
);
drop table  MovimientosInventario
CREATE TABLE MovimientosInventario (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    Referencia VARCHAR(150),
    HechoPor INT NOT NULL,
    FOREIGN KEY (IdProducto) REFERENCES Productos(Consecutivo),
    FOREIGN KEY (HechoPor) REFERENCES Usuarios(Consecutivo)
);

/*ORDENES Y TRABAJOS*/

--CREATE TABLE OrdenesTrabajo (
--    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
--    IdVehiculo INT NOT NULL,
--    FechaIngreso DATETIME DEFAULT GETDATE(),
--    DiagnosticoInicial VARCHAR(300),
--    Estado VARCHAR(20) DEFAULT 'EN PROCESO',
--    AsignadoA INT,
--    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculos(Consecutivo),
--    FOREIGN KEY (AsignadoA) REFERENCES Usuarios(Consecutivo)
--);

CREATE TABLE DetalleTrabajo (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    --IdOrden INT NOT NULL,
    Descripcion VARCHAR(200),
    Productos_Utilizados INT,
    CostoManoObra DECIMAL(10,2),
    --FOREIGN KEY (IdOrden) REFERENCES OrdenesTrabajo(Consecutivo),
    FOREIGN KEY (Productos_Utilizados) REFERENCES Productos(Consecutivo)
);

/* CONTABILIDAD*/

CREATE TABLE Ingresos (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(200),
    Monto DECIMAL(10,2) NOT NULL,
    Saldo_Pendiente DECIMAL(10,2),
    Estado INT,
    Fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (Estado) REFERENCES Estado(Consecutivo)
);


drop table Egresos
CREATE TABLE Egresos (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    Motivo VARCHAR(200),
    Monto DECIMAL(10,2) NOT NULL,
    Cantidad INT,
    Fecha DATETIME DEFAULT GETDATE(),
    RegistradoPor INT NOT NULL,
    MetodoPago VARCHAR(50),
    FOREIGN KEY (RegistradoPor) REFERENCES Usuarios(Consecutivo),
);

/*REPORTERÍA*/
drop table Reporteria
CREATE TABLE Reporteria (
    Consecutivo INT IDENTITY(1,1) PRIMARY KEY,
    TipoReporte VARCHAR(50) NOT NULL,
    FechaGeneracion DATETIME DEFAULT GETDATE(),
    IdUsuario INT NOT NULL,
    IdIngreso INT NULL,
    IdEgreso INT NULL,
    IdCita INT NULL,
    IdVehiculo INT NULL,
    --IdOrden INT NULL,
    IdProducto INT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Consecutivo),
    FOREIGN KEY (IdIngreso) REFERENCES Ingresos(Consecutivo),
    FOREIGN KEY (IdEgreso) REFERENCES Egresos(Consecutivo),
    FOREIGN KEY (IdCita) REFERENCES Citas(Consecutivo),
    FOREIGN KEY (IdVehiculo) REFERENCES Vehiculos(Consecutivo),
    --FOREIGN KEY (IdOrden) REFERENCES OrdenesTrabajo(Consecutivo),
    FOREIGN KEY (IdProducto) REFERENCES Productos(Consecutivo)
);
drop PROCEDURE  sp_RegistroUsuario


/*  PROCEDIMIENTOS ALMACENADOS*/

/*USUARIOS*/
drop procedure sp_RegistroUsuario
CREATE PROCEDURE sp_RegistroUsuario
    @NombreCompleto VARCHAR(100),
    @Cedula VARCHAR(20),
    @Correo VARCHAR(100),
    @UsuarioLogin VARCHAR(50),
    @Contrasenna VARCHAR(200),
    @Estado INT,
    @NombreRol INT
AS
BEGIN

    -- VALIDAR CEDULA
    IF EXISTS (SELECT 1 FROM Usuarios WHERE Cedula = @Cedula)
    BEGIN
        SELECT -1 AS Resultado
        RETURN
    END

    -- VALIDAR USUARIO
    IF EXISTS (SELECT 1 FROM Usuarios WHERE UsuarioLogin = @UsuarioLogin)
    BEGIN
        SELECT -2 AS Resultado
        RETURN
    END

    -- INSERT
    INSERT INTO Usuarios
    (
        NombreCompleto,
        Cedula,
        Correo,
        UsuarioLogin,
        Contrasenna,
        Estado,
        NombreRol
    )
    VALUES
    (
        @NombreCompleto,
        @Cedula,
        @Correo,
        @UsuarioLogin,
        @Contrasenna,
        @Estado,
        @NombreRol
    )

    SELECT 1 AS Resultado

END

drop procedure [dbo].[sp_ConsultarUsuarios]
CREATE PROCEDURE sp_ConsultarUsuarios
AS
BEGIN
    SELECT 
        U.Consecutivo,
        U.NombreCompleto,
        U.Cedula,
        U.Correo,
        U.UsuarioLogin,
        U.FechaRegistro,
        R.NombreRol,
        E.Estado AS Estado
    FROM Usuarios U
    INNER JOIN Roles R ON U.NombreRol = R.Consecutivo
    INNER JOIN Estado E ON U.Estado = E.Consecutivo
    WHERE U.Estado = 1
END



drop procedure [dbo].[sp_ObtenerRoles]
CREATE PROCEDURE [dbo].[sp_ObtenerRoles]
AS
BEGIN

    SELECT 
    Consecutivo, 
      NombreRol,
        Descripcion
    FROM Roles

END
GO



CREATE PROCEDURE [dbo].[sp_ObtenerEstado]
AS
BEGIN

    SELECT 
    Consecutivo,
    Estado
    FROM Estado

END
GO


CREATE PROCEDURE [dbo].[sp_ObtenerId]
    @Consecutivo INT
AS
BEGIN
    SELECT 
        U.Consecutivo,
        U.NombreCompleto,
        U.Cedula,
        U.Correo,
        U.UsuarioLogin,
        U.Contrasenna,
        U.Estado,
        U.NombreRol
    FROM Usuarios U
    WHERE U.Consecutivo = @Consecutivo
END

EXEC sp_ObtenerId 1
DROP PROCEDURE  [dbo].[sp_EditarUsuario]
CREATE PROCEDURE [dbo].[sp_EditarUsuario]
    @Consecutivo INT,
    @NombreCompleto VARCHAR(120),
    @Cedula VARCHAR(20),
    @Correo VARCHAR(120),
    @UsuarioLogin VARCHAR(50),
    @Estado INT,
    @NombreRol INT,
    @Contrasenna VARCHAR(100) = NULL
AS
BEGIN

UPDATE Usuarios
SET 
    NombreCompleto = @NombreCompleto,
    Cedula = @Cedula,
    Correo = @Correo,
    UsuarioLogin = @UsuarioLogin,
    Estado = @Estado,
    NombreRol = @NombreRol,

    Contrasenna = CASE 
                    WHEN @Contrasenna IS NULL OR @Contrasenna = ''
                    THEN Contrasenna
                    ELSE @Contrasenna
                  END

WHERE Consecutivo = @Consecutivo

END

insert into Estado(Estado) VALUES 
('Activo'), ('Inactivo');

INSERT INTO Roles(NombreRol, Descripcion) VALUES
('Encargado', 'Usuario responsable de supervisar operaciones'),
('Administrador', 'Usuario con control total del sistema'),
('Mecanico', 'Usuario encargado de realizar reparaciones y mantenimiento');

select * from Roles
select * from Estado
select * from Usuarios
EXEC sp_EditarUsuario 2

    DELETE FROM Usuarios;
DBCC CHECKIDENT ('Usuarios', RESEED, 0);