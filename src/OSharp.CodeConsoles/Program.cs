using System;
using System.Diagnostics;

using MiniRazor;

using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGeneration.Templates;


namespace OSharp.CodeConsoles
{
    class Program
    {
        static void Main(string[] args)
        {
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

        private static async void Method01()
        {
            var model = new CodeModule { Name = "Infos" };
            await TestTemplate.RenderAsync(Console.Out, model);
            ITemplate tmp = new TestTemplate();
            tmp.Model = model;
            tmp.Output = Console.Out;
            await tmp.ExecuteAsync();
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

        private static void Method11()
        {
            throw new NotImplementedException();
        }
    }
}
