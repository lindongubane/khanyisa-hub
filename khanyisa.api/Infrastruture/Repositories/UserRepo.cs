using System.Data;
using System.Net;
using Application.Interfaces.Repositories;
using Dapper;
using Domain.Model;
using Infrastruture.Database;

namespace Infrastruture.Repositories;

public class UserRepo : IUserRepo
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    public UserRepo(IDbConnectionFactory dbConnectionFactory) => _dbConnectionFactory = dbConnectionFactory;

    public async Task<string?> GetLastUsername(CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var command = new CommandDefinition(
            commandText: "spGelLastUserTableIndex",
            commandType: CommandType.StoredProcedure,
            cancellationToken: token
        );

        int result = await connection.QuerySingleOrDefaultAsync<int>(command);

        return result == default ? null : result.ToString();
    }

    public async Task<User?> CreateUser(User user, CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using IDbTransaction transaction = connection.BeginTransaction();

        var param = new
        {
            user.Id,
            user.Username,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Cell,
            user.CreatedOn,
        };

        var command = new CommandDefinition(
           commandText: "spInsertUser",
           parameters: param,
           commandType: CommandType.StoredProcedure,
           transaction: transaction,
           cancellationToken: token
       );

        User? newUser = await connection.QueryFirstOrDefaultAsync<User>(command);

        if (newUser is null)
        {
            transaction.Rollback();
            return null;
        }

        var addressCommand = new CommandDefinition(
            commandText: "spInsetAddress",
            parameters: new
            {
                user.Address.Id,
                user.Address.UserId,
                user.Address.Line1,
                user.Address.Line2,
                user.Address.Type,
                user.Address.City,
                user.Address.Province,
                user.Address.Country,
                user.Address.ZipCode,
                user.Address.CreatedOn,
            },
            commandType: CommandType.StoredProcedure,
            cancellationToken: token,
            transaction: transaction
        );

        Address? newAddress = await connection.QueryFirstOrDefaultAsync<Address>(addressCommand);

        if (newAddress is null)
        {
            transaction.Rollback();
            return null;
        }

        transaction.Commit();

        newUser.Address = newAddress;
        return newUser;
    }

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var command = new CommandDefinition(
            commandText: "spAllSelectUser",
            parameters: new { Username = username },
            commandType: CommandType.StoredProcedure,
            cancellationToken: token
        );

        return await connection.QueryFirstOrDefaultAsync<User>(command);
    }

    public async Task<IEnumerable<User>> GetUserListAsync(CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.QueryAsync<User>(new CommandDefinition("spSelectAllUsers", commandType: CommandType.StoredProcedure, cancellationToken: token));
    }
}
