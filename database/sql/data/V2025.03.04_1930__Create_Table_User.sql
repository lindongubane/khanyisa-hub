CREATE TABLE [user]
(
    [id]         UNIQUEIDENTIFIER PRIMARY KEY,
    [username]   VARCHAR(8),
    [first_name] varchar(50),
    [last_name]  varchar(50),
    [cell]       varchar(20) unique,
    [email]      varchar(255) unique,
    [created_on]  DateTime,
);