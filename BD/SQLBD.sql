create database MantPersonasBD;
GO
use MantPersonasBD;
GO

--Creación de la tabla Personas

Create table Personas (
	id INT PRIMARY KEY IDENTITY (1,1),
	nombre NVARCHAR(100) NOT NULL,
	apellido NVARCHAR(100) NOT NULL,
	correo NVARCHAR(100) NOT NULL,
	telefono NVARCHAR(15) NOT NULL,
	direccion NVARCHAR(100) NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	UsuarioCreacion NVARCHAR(50) NOT NULL,
	FechaModificacion DATETIME NULL,
	UsuarioModificacion NVARCHAR(50) NULL,
	);
GO
