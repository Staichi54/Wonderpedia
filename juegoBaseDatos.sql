CREATE DATABASE Wonderpedia;

GO

USE Wonderpedia;
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Correo NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT GETDATE(),

    FinalizarIngles BIT NOT NULL DEFAULT 0,
    FinalizarMates BIT NOT NULL DEFAULT 0,
    FinalizarHistoria BIT NOT NULL DEFAULT 0
);
GO

ALTER TABLE Usuarios
ADD CONSTRAINT UQ_Usuarios_Nombre UNIQUE (Nombre);
GO

ALTER TABLE Usuarios
ADD CONSTRAINT UQ_Usuarios_Correo UNIQUE (Correo);
GO

CREATE TABLE HistorialLogros (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Modulo NVARCHAR(50) NOT NULL,
    FechaLogro DATETIME2 NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_HistorialLogros_Usuarios
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

CREATE PROCEDURE sp_ConsultarProgresoUsuario
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Nombre,
        Correo,
        FinalizarIngles,
        FinalizarMates,
        FinalizarHistoria,
        FechaCreacion
    FROM Usuarios
    WHERE Id = @UsuarioId;
END;
GO

CREATE TRIGGER trg_RegistrarLogroUsuario
ON Usuarios
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO HistorialLogros (UsuarioId, Modulo)
    SELECT 
        i.Id,
        'Inglés'
    FROM inserted i
    INNER JOIN deleted d ON i.Id = d.Id
    WHERE d.FinalizarIngles = 0 
      AND i.FinalizarIngles = 1;

    INSERT INTO HistorialLogros (UsuarioId, Modulo)
    SELECT 
        i.Id,
        'Matemáticas'
    FROM inserted i
    INNER JOIN deleted d ON i.Id = d.Id
    WHERE d.FinalizarMates = 0 
      AND i.FinalizarMates = 1;

    INSERT INTO HistorialLogros (UsuarioId, Modulo)
    SELECT 
        i.Id,
        'Historia'
    FROM inserted i
    INNER JOIN deleted d ON i.Id = d.Id
    WHERE d.FinalizarHistoria = 0 
      AND i.FinalizarHistoria = 1;
END;
GO

SELECT * FROM Usuarios;
GO

SELECT * FROM HistorialLogros;
GO

USE Wonderpedia;
GO

UPDATE Usuarios
SET FinalizarIngles = 1 WHERE Id = 3;
UPDATE Usuarios
SET FinalizarHistoria = 1 WHERE Id = 3;
UPDATE Usuarios
SET FinalizarMates = 1 WHERE Id = 3;
GO

UPDATE Usuarios
SET FinalizarIngles = 0 WHERE Id = 3;
UPDATE Usuarios
SET FinalizarHistoria = 0 WHERE Id = 3;
UPDATE Usuarios
SET FinalizarMates = 0 WHERE Id = 3;
GO

SELECT * FROM Usuarios;
GO
