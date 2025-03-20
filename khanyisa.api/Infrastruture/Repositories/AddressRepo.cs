using System.Data;
using Application.Interfaces.Repositories;
using Dapper;
using Domain.Model;
using Infrastruture.Database;

namespace Infrastruture.Repositories;

public class AddressRepo : IAddressRepo
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public AddressRepo(IDbConnectionFactory dbConnectionFactory) => _dbConnectionFactory = dbConnectionFactory;

    public async Task<Address?> AddAddressAsync(Address address, CancellationToken token = default)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        var param = new
        {
            address.Id,
            address.Line1,
            address.Line2,
            address.UserId,
            address.Type,
            address.City,
            address.Province,
            address.Country,
            address.ZipCode,
            address.CreatedOn,
        };

        var command = new CommandDefinition(
            commandText: "spInsetAddress",
            parameters: param,
            commandType: CommandType.StoredProcedure,
            cancellationToken: token
        );

        return await connection.QueryFirstOrDefaultAsync<Address>(command);
    }
}
