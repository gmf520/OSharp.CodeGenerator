// -----------------------------------------------------------------------
//  <copyright file="CommandTask.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-16 1:30</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Cli.Infrastructure
{
    /// <summary>
    /// 为可以从命令行运行命令的所有任务提供一个基类。
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// 当派生类覆盖此方法时，运行命令。
        /// </summary>
        /// <returns>
        /// 整个应用程序的返回值。
        /// </returns>
        public virtual int Run()
        {
            return 0;
        }
    }
}
