using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using StaffHub.Core.Interfaces;
using StaffHub.Database.Repositories;
using Environment = NHibernate.Cfg.Environment;

namespace StaffHub.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStaffHubDatabase(this IServiceCollection services)
    {
        // Connection string based on db.txt
        string connectionString = "Server=rc1b-1dtlcql6dlbt3qvg.mdb.yandexcloud.net;Port=3306;Database=db1;Uid=user1;Pwd=qwerty123;SslMode=VerifyCA;";

        var configuration = new Configuration();
        configuration.SetProperty(Environment.ConnectionDriver, typeof(MySqlDataDriver).AssemblyQualifiedName);
        configuration.SetProperty(Environment.Dialect, typeof(MySQLDialect).AssemblyQualifiedName);
        configuration.SetProperty(Environment.ConnectionString, connectionString);
        configuration.SetProperty(Environment.FormatSql, "true");
        configuration.SetProperty(Environment.ShowSql, "true");

        // Add mappings
        var mapper = new ModelMapper();
        mapper.AddMappings(typeof(ServiceCollectionExtensions).Assembly.GetExportedTypes());
        var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        configuration.AddMapping(mapping);

        var sessionFactory = configuration.BuildSessionFactory();

        services.AddSingleton(sessionFactory);
        services.AddScoped(factory => factory.GetRequiredService<ISessionFactory>().OpenSession());

        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
