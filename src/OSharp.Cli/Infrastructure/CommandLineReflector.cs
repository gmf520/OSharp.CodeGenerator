// -----------------------------------------------------------------------
//  <copyright file="CommandLineReflector.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-16 1:30</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using McMaster.Extensions.CommandLineUtils;

using OSharp.Dependency;


namespace OSharp.Cli.Infrastructure
{
    /// <summary>
    /// Contains a converter that can reflect an assembly to Specified <see cref="CommandLineApplication"/>.
    /// </summary>
    internal static class CommandLineReflector
    {
        /// <summary>
        /// Reflect an assembly and get all <see cref="CommandBase"/>s to the specified <see cref="CommandLineApplication"/>.
        /// </summary>
        /// <param name="app">The <see cref="CommandLineApplication"/> to receive configs.</param>
        /// <param name="assembly">The Assembly to reflect from.</param>
        internal static void ReflectFrom(this CommandLineApplication app, Assembly assembly)
        {
            foreach (var ct in assembly.GetTypes()
                .Where(x => typeof(CommandBase).IsAssignableFrom(x)))
            {
                var commandAttribute = ct.GetCustomAttribute<CommandMetadataAttribute>();
                if (commandAttribute == null)
                {
                    continue;
                }

                app.Command(commandAttribute.Name,
                    command =>
                    {
                        ConfigCommand(command, commandAttribute.Description, ct);
                    });
            }
        }

        /// <summary>
        /// Convert a <see cref="CommandBase"/> to <see cref="CommandLineApplication"/> configs.
        /// </summary>
        private static void ConfigCommand(CommandLineApplication app, string commandDescription, Type taskType)
        {
            // Config basic info.
            app.Description = commandDescription;
            app.HelpOption("-?|-h|--help");

            // Store argument list and option list.
            // so that when the command executed, all properties can be initialized from command lines.
            var argumentPropertyList = new List<(CommandArgument argument, PropertyInfo property)>();
            var optionPropertyList = new List<(CommandOption option, PropertyInfo property)>();

            // Enumerate command task properties to get enough metadata to config command.
            foreach (var property in taskType.GetTypeInfo().DeclaredProperties)
            {
                // Try to get argument and option info.
                var argumentAttribute = property.GetCustomAttribute<CommandArgumentAttribute>();
                var optionAttribute = property.GetCustomAttribute<CommandOptionAttribute>();

                if (argumentAttribute != null && property.CanWrite)
                {
                    // Try to record argument info.
                    var argument = app.Argument(
                        argumentAttribute.Name,
                        argumentAttribute.Description);
                    argumentPropertyList.Add((argument, property));
                }
                else if (optionAttribute != null && property.CanWrite)
                {
                    // Try to record option info.
                    CommandOptionType commandOptionType;
                    if (typeof(IEnumerable<string>).IsAssignableFrom(property.PropertyType))
                    {
                        // If this property is a List<string>.
                        commandOptionType = CommandOptionType.MultipleValue;
                    }
                    else if (typeof(string).IsAssignableFrom(property.PropertyType))
                    {
                        // If this property is a string.
                        commandOptionType = CommandOptionType.SingleValue;
                    }
                    else if (typeof(bool).IsAssignableFrom(property.PropertyType))
                    {
                        // If this property is a bool.
                        commandOptionType = CommandOptionType.NoValue;
                    }
                    else
                    {
                        continue;
                    }

                    var option = app.Option(
                        optionAttribute.Template,
                        optionAttribute.Description,
                        commandOptionType);
                    optionPropertyList.Add((option, property));
                }
            }

            // Config how to execute the command.
            app.OnExecute(() =>
            {
                // Create a new instance of CommandTask to call the Run method.
                //var commandTask = (CommandTask)Activator.CreateInstance(taskType);
                var command = (CommandBase)ServiceLocator.Instance.GetService(taskType);

                // Initialize the instance with prepared arguments and options.
                foreach (var (argument, property) in argumentPropertyList)
                {
                    property.SetValue(command, argument.Value);
                }

                foreach (var (option, property) in optionPropertyList)
                {
                    switch (option.OptionType)
                    {
                        case CommandOptionType.MultipleValue:
                            property.SetValue(command, option.Values.ToList());
                            break;
                        case CommandOptionType.SingleValue:
                            property.SetValue(command, option.Value());
                            break;
                        case CommandOptionType.NoValue:
                            property.SetValue(command, option.HasValue());
                            break;
                        default:
                            continue;
                    }
                }

                // Call the Run method.
                return command.Run();
            });
        }
    }
}
