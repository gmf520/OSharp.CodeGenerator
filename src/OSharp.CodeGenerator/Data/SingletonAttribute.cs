// -----------------------------------------------------------------------
//  <copyright file="SingletonAttribute.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-03 14:52</last-date>
// -----------------------------------------------------------------------

using System;


namespace OSharp.CodeGenerator.Views
{
    /// <summary>
    /// 标注类型的注册到IoC的生命周期为Singleton
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    { }
}
