CREATE OR ALTER PROCEDURE spInsetAddress
    @Id Guid
    @UserId Guid
    @Type VARCHAR (20)
    @City VARCHAR (200),
    @Province VARCHAR (200),
    @Country VARCHAR (200),
    @Line1 VARCHAR (200),
    @Line2 VARCHAR (200),
    @ZipCode INT,
AS
BEGIN
    INSERT INTO [Address] VALUES (@Id, @UserId, @Type, @City @Province, @Country,@Line1, @Line2, @ZipCode);
    SELECT * FROM [Address] WHERE Id = (Select SCOPE_IDENTITY());
END;