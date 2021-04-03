using System;
using Microsoft.Extensions.DependencyInjection;
using OSharp.Entity;


namespace OSharp.CodeGenerator.Data
{
    public class SqliteDesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<DefaultDbContext>
    {
        public SqliteDesignTimeDefaultDbContextFactory() : base(null)
        { }

        public SqliteDesignTimeDefaultDbContextFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        { }

        protected override IServiceProvider CreateDesignTimeServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
