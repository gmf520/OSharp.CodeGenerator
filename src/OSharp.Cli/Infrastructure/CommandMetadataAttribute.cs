// -----------------------------------------------------------------------
//  <copyright file="CommandMetadataAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-16 1:30</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Cli.Infrastructure
{
    /// <summary>
    /// Specify a unique name of a command and when user typped a command
    /// with this name the Run method of this class will be executed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandMetadataAttribute : Attribute
    {
        /// <summary>
        /// Specify a unique name of a command and when user typped a command
        /// with this name the Run method of this class will be executed.
        /// </summary>
        public CommandMetadataAttribute(string commandName)
        {
            Name = commandName;
        }

        /// <summary>
        /// Gets the unique name of a command task.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the description of the command task.
        /// This will be shown when the user typed --help option.
        /// </summary>
        public string Description { get; set; }
    }
}
