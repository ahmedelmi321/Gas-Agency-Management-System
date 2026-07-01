CREATE DATABASE GasAgencyDB;
USE GasAgencyDB;
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY ,
    FullName VARCHAR(40),
    Phone VARCHAR(20),
    Address VARCHAR(30)
);

CREATE TABLE GasCylinder (
    CylinderID INT PRIMARY KEY ,
    CylinderType VARCHAR(50),
    Price DECIMAL(10,2),
    QuantityInStock INT
);

CREATE TABLE Sales (
    SaleID INT PRIMARY KEY ,
    CustomerID INT,
    CylinderID INT,
    QuantitySold INT,
    SaleDate DATE,

    FOREIGN KEY (CustomerID)
    REFERENCES Customers(CustomerID),

    FOREIGN KEY (CylinderID)
    REFERENCES GasCylinder(CylinderID)
);
INSERT INTO Customers (CustomerID,FullName, Phone, Address)
VALUES
(1,'Ahmed Ali', '061234567', 'Mogadishu'),
(2,'Asha Omar', '062345678', 'Mogadishu'),
(3,'Mohamed Hassan', '063456789', 'Hargeisa');

INSERT INTO GasCylinder (CylinderID,CylinderType, Price, QuantityInStock)
VALUES
(101,'6KG', 15.00, 50),
(102,'12KG', 30.00, 30)

INSERT INTO Sales (SaleID,CustomerID, CylinderID, QuantitySold, SaleDate)
VALUES
(300,1, 101, 2, '2026-06-18'),
(301,2, 102, 1, '2026-06-19')

SELECT * FROM Customers;
SELECT * FROM GasCylinder;
SELECT * FROM Sales;

