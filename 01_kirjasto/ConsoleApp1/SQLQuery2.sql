CREATE TABLE Opiskelija (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Etunimi NVARCHAR(50) NOT NULL,
    Sukunimi NVARCHAR(50) NOT NULL,
    RyhmaId INT NULL
);
