CREATE OR ALTER PROCEDURE spInsetAddress
    @Id UNIQUEIDENTIFIER,
    @Type VARCHAR (10),
    @City VARCHAR (50),
    @Province VARCHAR (50),
    @Country VARCHAR (255),
    @Line1 VARCHAR (50),
    @Line2 VARCHAR (50),
    @ZipCode INT,
    @CreatedOn DATETIME,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO [address] VALUES (@Id, @Type, @Line1, @Line2, @zipCode,@City,@Province, @Country, @CreatedOn, @UserId);
    SELECT * FROM [address] WHERE Id = @Id;
END;