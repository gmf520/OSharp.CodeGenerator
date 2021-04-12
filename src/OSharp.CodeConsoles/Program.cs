using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MiniRazor;

using OSharp.CodeConsoles.Data;
using OSharp.CodeGeneration.Generates;
using OSharp.CodeGeneration.Services;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGeneration.Templates;
using OSharp.Data;
using OSharp.Entity;
using OSharp.Extensions;
using OSharp.Json;


namespace OSharp.CodeConsoles
{
    class Program
    {
        private static ServiceProvider _provider;

        static void Main(string[] args)
        {
            Init();

            bool exit = false;
            while (true)
            {
                try
                {
                    Console.WriteLine(@"请输入命令：0; 退出程序，功能命令：1 - n");
                    string input = Console.ReadLine();
                    if (input == null)
                    {
                        continue;
                    }

                    Stopwatch watch = Stopwatch.StartNew();
                    switch (input.ToLower())
                    {
                        case "0":
                            exit = true;
                            break;
                        case "1":
                            Method01();
                            break;
                        case "2":
                            Method02();
                            break;
                        case "3":
                            Method03();
                            break;
                        case "4":
                            Method04();
                            break;
                        case "5":
                            Method05();
                            break;
                        case "6":
                            Method06();
                            break;
                        case "7":
                            Method07();
                            break;
                        case "8":
                            Method08();
                            break;
                        case "9":
                            Method09();
                            break;
                        case "10":
                            Method10();
                            break;
                        case "11":
                            Method11();
                            break;
                    }

                    watch.Stop();
                    Console.WriteLine($"执行完成，耗时：{watch.Elapsed}\n");
                    if (exit)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static void Init()
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            _provider = services.BuildServiceProvider();
            startup.Configure(_provider);
        }

        private static void Method01()
        {
            
        }

        private static async void Method02()
        {
            // Compile the template into an in-memory assembly
            TemplateDescriptor template = Razor.Compile("<p>Hello, @Model.Name!</p>");

            // Render the template to string
            var output = await template.RenderAsync(new CodeModule { Name = "World" });
        }

        private static void Method03()
        {
            throw new NotImplementedException();
        }

        private static void Method04()
        {
            throw new NotImplementedException();
        }

        private static void Method05()
        {
            throw new NotImplementedException();
        }

        private static void Method06()
        {
            throw new NotImplementedException();
        }

        private static void Method07()
        {
            throw new NotImplementedException();
        }

        private static void Method08()
        {
            throw new NotImplementedException();
        }

        private static void Method09()
        {
            throw new NotImplementedException();
        }

        private static void Method10()
        {
            throw new NotImplementedException();
        }

        private static async void Method11()
        {
            Console.WriteLine("请输入代码模板：");
            string input = Console.ReadLine();
            CodeProject project = null;
            CodeTemplate template = null;
            await _provider.ExecuteScopedWorkAsync(provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                CodeProject[] projects = contract.GetCodeProject(m => true);
                project = projects[0];
                template = contract.CodeTemplates.FirstOrDefault(m => m.Name == input);
                return Task.CompletedTask;
            });

            if (template == null)
            {
                Console.WriteLine($"模板 {input} 不存在");
                return;
            }
            ICodeGenerator generator = _provider.GetService<ICodeGenerator>();

            //var model = project.Modules.First().Entities.First();
            //var model = project.Modules.First();
            var model = project;
            CodeFile file = await generator.GenerateCode(template, model);
            File.WriteAllText(@"D:\Temp\11.txt", file.SourceCode);
            Console.WriteLine(file.SourceCode);
        }
    }
}
