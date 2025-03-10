-- Crear la base de datos GestorFinanzasDB
CREATE DATABASE GestorFinanzasDB;
GO

USE GestorFinanzasDB;
GO

-- Crear la tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
GO
