// -----------------------------------------------------------------------
//  <copyright file="Bootstrapper.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-08-26 10:41</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using OSharp.CodeGenerator.Views;
using OSharp.Wpf.Data;
using OSharp.Wpf.Stylet;


namespace OSharp.CodeGenerator
{
    public class Bootstrapper : ServiceProviderBootstrapper<MainViewModel>
    {
        private readonly Startup _startup = new Startup();

        protected override void ConfigureIoC(IServiceCollection services)
        {
            _startup.ConfigureServices(services);
        }

        /// <summary>Hook called after the IoC container has been set up</summary>
        protected override void Configure()
        {
            _startup.Configure(ServiceProvider);
            IoC.Initialize(ServiceProvider);
            MainViewModel main = IoC.Get<MainViewModel>();
            Output.StatusBar = msg => main.StatusBar.Message = msg;
        }
    }
}
