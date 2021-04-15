// -----------------------------------------------------------------------
//  <copyright file="CommandArgumentAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-16 1:30</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.Cli
{
    /// <summary>
    /// Specify a property to receive argument of command from the user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CommandArgumentAttribute : Attribute
    {
        /// <summary>
        /// Specify a property to receive argument of command from the user.
        /// </summary>
        public CommandArgumentAttribute(string argumentName)
        {
            Name = argumentName;
        }

        /// <summary>
        /// Gets the argument name of a command task.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the description of the argument.
        /// This will be shown when the user typed --help option.
        /// </summary>
        public string Description { get; set; }
    }
}
