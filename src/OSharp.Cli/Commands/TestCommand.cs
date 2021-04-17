// -----------------------------------------------------------------------
//  <copyright file="TestCommandTask.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-16 1:41</last-date>
// -----------------------------------------------------------------------

using McMaster.Extensions.CommandLineUtils;

using OSharp.Cli.Infrastructure;


namespace OSharp.Cli.Commands
{
    [CommandMetadata("test", Description = "test task")]
    public class TestCommand : CommandBase
    {
        private readonly IConsole _console;

        public TestCommand(IConsole console)
        {
            _console = console;
        }

        [CommandArgument("[word]", Description = "word to say")]
        public string Word { get; set; }

        /// <summary>
        /// Run command when derived class override this method.
        /// </summary>
        /// <returns>
        /// Return value of the whole application.
        /// </returns>
        public override int Run()
        {
            _console.WriteLine($"{nameof(TestCommand)} say: {Word}");
            return base.Run();
        }
    }
}
