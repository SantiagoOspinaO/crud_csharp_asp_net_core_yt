
CREATE DATABASE EjemploDB

USE EjemploDB

CREATE TABLE Productos(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50),
    Precio DECIMAL(18,2)
)

CREATE PROCEDURE ObtenerProductos
AS BEGIN
SELECT * FROM Productos
END

CREATE PROCEDURE AgregarProducto
@Nombre VARCHAR(50),
@Precio DECIMAL(18,2)
AS BEGIN
INSERT INTO Productos VALUES(@Nombre, @Precio)
END

CREATE PROCEDURE ActualizarProducto
@Id INT,
@Nombre VARCHAR(50),
@Precio DECIMAL(18,2)
AS BEGIN
UPDATE Productos SET Nombre=@Nombre, Precio=@Precio WHERE Id=@Id
END

CREATE PROCEDURE EliminarProducto
@Id INT
AS BEGIN
DELETE FROM Productos WHERE Id=@Id
END