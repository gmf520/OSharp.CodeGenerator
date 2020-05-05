// -----------------------------------------------------------------------
//  <copyright file="ViewModelLocator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-05 23:51</last-date>
// -----------------------------------------------------------------------

using OSharp.Wpf.Stylet;


namespace OSharp.CodeGenerator.Views
{
    public class ViewModelLocator
    {
        public MainViewModel Main => IoC.Get<MainViewModel>();
    }
}
