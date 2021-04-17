using System;
using System.Diagnostics;
using System.Reflection;

using McMaster.Extensions.CommandLineUtils;

using Microsoft.Extensions.DependencyInjection;

using OSharp.Cli.Infrastructure;
using OSharp.Dependency;
using OSharp.IO;


namespace OSharp.Cli
{
    [Command(Name = "osharp", Description = "OSHARP CLI （https://osharp.org）")]
    [HelpOption]
    class Program
    {
        static int Main(string[] args)
        {
            IServiceProvider provider = BuildServices();
            var app = new CommandLineApplication<Program>();
            app.Conventions.UseDefaultConventions().UseConstructorInjection(provider);
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            app.VersionOption("-v|--version", fvi.ProductVersion, fvi.FileVersion);
            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });
            app.ReflectFrom(assembly);
            return app.Execute(args);
        }

        private static IServiceProvider BuildServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddOSharp().AddPack<OsharpCliPack>();
            services.AddSingleton<IConsole>(PhysicalConsole.Singleton);
            IServiceProvider provider = services.BuildServiceProvider();
            provider.UseOsharp();
            return provider;
        }
    }
}
