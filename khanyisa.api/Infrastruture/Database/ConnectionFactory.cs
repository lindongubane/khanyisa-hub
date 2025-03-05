using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastruture.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}

public class ConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public ConnectionFactory(string connectionString) => _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(token);
        
        return connection;
    }
}
