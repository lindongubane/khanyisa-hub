using System.Data;
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

    public async Task<ApplicationUser?> CreateUser(ApplicationUser user, CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var command = new CommandDefinition(
           commandText: "spInsetUser",
           parameters: user,
           commandType: CommandType.StoredProcedure,
           cancellationToken: token
       );

        SqlMapper.AddTypeHandler(typeof(AdditionalData), new JsonTypeHandler());
        return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(command);
    }

    public async Task<ApplicationUser?> GetUserByUsernameAsync(string username, CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        SqlMapper.AddTypeHandler(typeof(AdditionalData), new JsonTypeHandler());

        var command = new CommandDefinition(
            commandText: "spSelectUser",
            parameters: new { Username = username },
            commandType: CommandType.StoredProcedure,
            cancellationToken: token
        );

        return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(command);
    }

    public async Task<IEnumerable<ApplicationUser>> GetUserListAsync(CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        SqlMapper.AddTypeHandler(typeof(AdditionalData), new JsonTypeHandler());

        return await connection.QueryAsync<ApplicationUser>(new CommandDefinition("spSelectAllUsers", commandType: CommandType.StoredProcedure, cancellationToken: token));
    }
}
