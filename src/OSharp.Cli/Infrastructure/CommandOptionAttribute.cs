// -----------------------------------------------------------------------
//  <copyright file="CommandOptionAttribute.cs" company="OSharp开源团队">
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
    /// Specify a property to receive an option of command from the user.
    /// The option template format can be "-n", "--name" or "-n|--name".
    /// The property type can be bool, string or List{string} (or any other base types).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CommandOptionAttribute : Attribute
    {
        /// <summary>
        /// Specify a property to receive an option of command from the user.
        /// The option template format can be "-n", "--name" or "-n|--name".
        /// The property type can be bool, string or List{string} (or any other base types).
        /// </summary>
        public CommandOptionAttribute(string template)
        {
            Template = template;
        }

        /// <summary>
        /// Gets the option template of this option.
        /// </summary>
        public string Template { get; }

        /// <summary>
        /// Gets or sets the description of the option.
        /// This will be shown when the user typed --help option.
        /// </summary>
        public string Description { get; set; }
    }
}
