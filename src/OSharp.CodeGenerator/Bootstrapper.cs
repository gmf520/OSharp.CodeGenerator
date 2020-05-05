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
using OSharp.Core.Builders;
using OSharp.Log4Net;
using OSharp.Wpf.Data;
using OSharp.Wpf.FluentValidation;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator
{
    public class Bootstrapper : ServiceProviderBootstrapper<MainViewModel>
    {
        protected override void ConfigureIoC(IServiceCollection services)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration configuration = configurationBuilder.Build();
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
                .AddPack<ViewModelPack>()
                .AddPack<Log4NetPack>()
                .AddPack<SqliteDefaultDbContextMigrationPack>();
        }

        /// <summary>Hook called after the IoC container has been set up</summary>
        protected override void Configure()
        {
            IServiceProvider provider = ServiceProvider;
            provider.UseOsharp();
            IoC.Initialize(provider);
            MainViewModel main = IoC.Get<MainViewModel>();
            Output.StatusBar = msg => main.StatusBar.Message = msg;
        }
    }
}
