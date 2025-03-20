CREATE OR ALTER PROCEDURE spInsertUser
    @Id UNIQUEIDENTIFIER,
    @Username VARCHAR(8),
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(255),
    @Cell VARCHAR(20),
    @CreatedOn DATETIME
AS
BEGIN
    INSERT INTO [user] VALUES (@Id, @Username, @FirstName, @LastName, @Cell, @Email, @CreatedOn);
    SELECT * FROM [user] WHERE id = @Id;
END