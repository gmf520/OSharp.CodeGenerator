// -----------------------------------------------------------------------
//  <copyright file="ViewModelsModule.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-03 15:04</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;

using FluentValidation;

using OSharp.Reflection;
using OSharp.Wpf.FluentValidation;

using Stylet;

using StyletIoC;


namespace OSharp.CodeGenerator.Views
{
    public class ViewModelsModule : StyletIoCModule
    {
        /// <summary>
        /// Override, and use 'Bind' to add bindings to the module
        /// </summary>
        protected override void Load()
        {
            // Validator
            Bind(typeof(IValidator<>)).ToAllImplementations(true);
            Bind(typeof(IModelValidator<>)).To(typeof(FluentModelValidator<>));

            // ViewModels
            Type baseType = typeof(Screen);
            Type[] types = Assembly.GetExecutingAssembly().GetTypes().Where(m => !m.IsAbstract && m.IsBaseOn(baseType)).ToArray();
            Type[] singletonTypes = types.Where(m => m.HasAttribute<SingletonAttribute>()).ToArray();

            foreach (Type type in singletonTypes)
            {
                Bind(type).ToSelf().InSingletonScope();
            }

            foreach (Type type in types.Except(singletonTypes))
            {
                Bind(type).ToSelf();
            }
        }
    }
}
