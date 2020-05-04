using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OSharp.Core.Options;
using OSharp.Data;
using OSharp.Entity;
using OSharp.Exceptions;
using OSharp.Extensions;
using OSharp.Reflection;


namespace OSharp.CodeGenerator.Data
{
    public class SqliteDesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<DefaultDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public SqliteDesignTimeDefaultDbContextFactory()
        { }

        public SqliteDesignTimeDefaultDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override string GetConnectionString()
        {
            if (_serviceProvider == null)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json");
                IConfiguration configuration = builder.Build();
                Singleton<IConfiguration>.Instance = configuration;
                Console.WriteLine($"configuration == null: {configuration == null}");
                string str = configuration["OSharp:DbContexts:Sqlite:ConnectionString"];
                return str;
            }
            OsharpOptions options = _serviceProvider.GetOSharpOptions();
            OsharpDbContextOptions contextOptions = options.GetDbContextOptions(typeof(DefaultDbContext));
            if (contextOptions == null)
            {
                throw new OsharpException($"上下文“{typeof(DefaultDbContext)}”的配置信息不存在");
            }
            return contextOptions.ConnectionString;
        }

        public override IEntityManager GetEntityManager()
        {
            if (_serviceProvider != null)
            {
                return _serviceProvider.GetService<IEntityManager>();
            }
            IEntityConfigurationTypeFinder typeFinder = new EntityConfigurationTypeFinder(new AppDomainAllAssemblyFinder());
            IEntityManager entityManager = new EntityManager(typeFinder);
            entityManager.Initialize();
            return entityManager;
        }

        public override bool LazyLoadingProxiesEnabled()
        {
            if (_serviceProvider == null)
            {
                IConfiguration configuration = Singleton<IConfiguration>.Instance;
                return configuration["OSharp:DbContexts:Sqlite:LazyLoadingProxiesEnabled"].CastTo(false);
            }
            OsharpOptions options = _serviceProvider.GetOSharpOptions();
            OsharpDbContextOptions contextOptions = options.GetDbContextOptions(typeof(DefaultDbContext));
            if (contextOptions == null)
            {
                throw new OsharpException($"上下文“{typeof(DefaultDbContext)}”的配置信息不存在");
            }

            return contextOptions.LazyLoadingProxiesEnabled;
        }

        public override DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString)
        {
            string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"entryAssemblyName: {entryAssemblyName}");
            return builder.UseSqlite(connString, b => b.MigrationsAssembly(entryAssemblyName));
        }
    }
}
