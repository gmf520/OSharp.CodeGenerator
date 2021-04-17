// -----------------------------------------------------------------------
//  <copyright file="NewCommand.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-17 0:06</last-date>
// -----------------------------------------------------------------------

using McMaster.Extensions.CommandLineUtils;

using OSharp.Cli.Infrastructure;
using OSharp.CommandLine;


namespace OSharp.Cli.Commands
{
    [CommandMetadata("new", Description = "基于OSharp项目模板创建一个解决方案基础项目代码")]
    public class NewCommand : CommandBase
    {
        private readonly IConsole _console;

        public NewCommand(IConsole console)
        {
            _console = console;
        }

        /// <summary>
        /// Run command when derived class override this method.
        /// </summary>
        /// <returns>
        /// Return value of the whole application.
        /// </returns>
        public override int Run()
        {
            string output = CmdExecutor.ExecuteCmd("dotnet --info");
            _console.WriteLine(output);
            return 0;
        }
    }
}
