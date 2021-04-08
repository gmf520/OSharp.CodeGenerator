using Microsoft.Extensions.DependencyInjection;

using OSharp.Core.Packs;


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

            return base.AddServices(services);
        }
    }
}
