CREATE TABLE [address]
(
    [id]        UNIQUEIDENTIFIER PRIMARY KEY,
    [type]      VARCHAR(10),
    [line_1]    varchar(50),
    [line_2]    varchar(50),
    [zip_code]  varchar(50),
    [city]      varchar(50),
    [province]  varchar(50),
    [country]   varchar(255),
    [created_on] DateTime,
    [user_id]   UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [user] ([id]) NULL,
)