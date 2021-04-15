// -----------------------------------------------------------------------
//  <copyright file="CommandTask.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-16 1:30</last-date>
// -----------------------------------------------------------------------

namespace OSharp.Cli
{
    /// <summary>
    /// Provide a base class for all tasks that can run command from command line.
    /// </summary>
    public abstract class CommandTask
    {
        /// <summary>
        /// Run command when derived class override this method.
        /// </summary>
        /// <returns>
        /// Return value of the whole application.
        /// </returns>
        public virtual int Run()
        {
            return 0;
        }
    }
}
