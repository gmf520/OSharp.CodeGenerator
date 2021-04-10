using Microsoft.Extensions.DependencyInjection;

using OSharp.CodeGeneration.Services.Seeds;
using OSharp.Core.Packs;
using OSharp.Entity;


namespace OSharp.CodeGeneration.Services
{
    public class DataPack : OsharpPack
    {
        /// <summary>将模块服务添加到依赖注入服务容器中</summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IDataContract, DataService>();
            services.AddSingleton<ISeedDataInitializer, CodeTemplateSeedDataInitializer>();
            services.AddSingleton<ISeedDataInitializer, CodeProjectSeedDataInitializer>();
            services.AddSingleton<ISeedDataInitializer, CodeModuleSeedDataInitializer>();
            services.AddSingleton<ISeedDataInitializer, CodeEntitySeedDataInitializer>();
            services.AddSingleton<ISeedDataInitializer, CodePropertySeedDataInitializer>();

            return base.AddServices(services);
        }
    }
}
