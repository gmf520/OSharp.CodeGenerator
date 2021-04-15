// -----------------------------------------------------------------------
//  <copyright file="CommandTaskPack.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-16 1:37</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.Extensions.DependencyInjection;

using OSharp.Core.Packs;
using OSharp.Reflection;


namespace OSharp.Cli
{
    public class CommandTaskPack : OsharpPack
    {
        /// <summary>将模块服务添加到依赖注入服务容器中</summary>
        /// <param name="services">依赖注入服务容器</param>
        /// <returns></returns>
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            Type[] commandTaskTypes = AssemblyManager.FindTypes(m => m.IsBaseOn<CommandTask>() && !m.IsAbstract);
            foreach (Type type in commandTaskTypes)
            {
                services.AddTransient(type);
            }
            return base.AddServices(services);
        }
    }
}
