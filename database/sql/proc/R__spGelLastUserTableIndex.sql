CREATE PROCEDURE spGelLastUserTableIndex
AS
BEGIN
    select Top 1 [Username] from [User] order by [User].[Username] desc
END