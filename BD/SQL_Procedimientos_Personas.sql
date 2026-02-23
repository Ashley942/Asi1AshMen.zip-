use MantPersonasBD;
GO
-- Procedimiento para agregar un cliente
CREATE PROCEDURE sp_AgregarPersonas
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Correo NVARCHAR(100),
    @Telefono NVARCHAR(15),
    @Direccion NVARCHAR(100),
    @UsuarioCreacion NVARCHAR(50)
AS
BEGIN
    INSERT INTO Personas(Nombre,Apellido, Correo, Telefono,Direccion, UsuarioCreacion)
    VALUES (@Nombre,@Apellido, @Correo, @Telefono, @Direccion, @UsuarioCreacion);
END;
GO
-- Procedimiento para obtener todos los clientes
CREATE PROCEDURE sp_ObtenerPersonas
AS
BEGIN
    SELECT * FROM Personas;
END;
GO
-- Procedimiento para obtener un cliente por ID
CREATE PROCEDURE sp_ObtenerPersonasPorId
    @Id INT
AS
BEGIN
    SELECT * FROM Personas WHERE Id = @Id;
END;
GO
-- Procedimiento para actualizar un cliente
CREATE PROCEDURE sp_ActualizarPersonas
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Correo NVARCHAR(100),
    @Telefono NVARCHAR(15),
    @Direccion NVARCHAR(100),
    @UsuarioModificacion NVARCHAR(50)
AS
BEGIN
    UPDATE Personas
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Correo = @Correo,
        Telefono = @Telefono,
        Direccion = @Direccion,
        FechaModificacion = GETDATE(),
        UsuarioModificacion = @UsuarioModificacion
    WHERE Id = @Id;
END;
GO
-- Procedimiento para eliminar un cliente
CREATE PROCEDURE sp_EliminarPersona
    @Id INT
AS
BEGIN
    DELETE FROM Personas WHERE Id = @Id;
END;
