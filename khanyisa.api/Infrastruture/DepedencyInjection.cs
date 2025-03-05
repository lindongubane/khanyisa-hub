using Application.Interfaces.Repositories;
using Infrastruture.Database;
using Infrastruture.Options;
using Infrastruture.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastruture;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastruture(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepo, UserRepo>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory>(provider =>
        {
            IOptions<DatabaseSettings> dbOptions = provider.GetRequiredService<IOptions<DatabaseSettings>>();
            return new ConnectionFactory(dbOptions.Value.ConnectionString);
        });

        return services;
    }
}
