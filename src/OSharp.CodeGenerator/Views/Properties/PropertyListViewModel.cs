// -----------------------------------------------------------------------
//  <copyright file="PropertyListViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-08 13:44</last-date>
// -----------------------------------------------------------------------

using System;

using Stylet;


namespace OSharp.CodeGenerator.Views.Properties
{
    public class PropertyListViewModel : Screen
    {
        private readonly IServiceProvider _provider;

        public PropertyListViewModel(IServiceProvider provider)
        {
            _provider = provider;
        }
    }
}
