// -----------------------------------------------------------------------
//  <copyright file="Bootstrapper.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-03 14:58</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using OSharp.CodeGenerator.Data;
using OSharp.CodeGenerator.Views;
using OSharp.Entity.Sqlite;
using OSharp.Wpf.Data;
using OSharp.Wpf.Stylet;

using Stylet;

using StyletIoC;


namespace OSharp.CodeGenerator
{
    public class Bootstrapper : Bootstrapper<MainViewModel>
    {
        private ILogger _logger;

        /// <summary>Override to add your own types to the IoC container.</summary>
        /// <param name="builder">StyletIoC builder to use to configure the container</param>
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.AddModule(new ViewModelsModule());
        }

        /// <summary>Hook called after the IoC container has been set up</summary>
        protected override void Configure()
        {
            IoC.Initialize(Container);
            StatusBarViewModel statusBar = IoC.Get<StatusBarViewModel>();
            Output.StatusBar = msg => statusBar.Message = msg;
            OsharpInit();
        }

        #region 私有方法

        private void OsharpInit()
        {
            IServiceCollection services = new ServiceCollection();
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            services.AddSingleton(configuration);

            services.AddLogging(opts =>
            {
#if DEBUG
                opts.SetMinimumLevel(LogLevel.Debug);
#else
                opts.SetMinimumLevel(LogLevel.Information);
#endif
            });

            services.AddOSharp()
                .AddPack<SqliteDefaultDbContextMigrationPack>();

            IServiceProvider provider = services.BuildServiceProvider();
            provider.UseOsharp();
            _logger = provider.GetLogger<Bootstrapper>();
        }

        #endregion
    }
}
