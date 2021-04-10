// -----------------------------------------------------------------------
//  <copyright file="AutoMapperConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-06 13:13</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

using OSharp.AutoMapper;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.Dependency;


namespace OSharp.CodeConsoles.Data
{
    [Dependency(ServiceLifetime.Singleton)]
    public class AutoMapperConfiguration : AutoMapperTupleBase
    {
        /// <summary>创建对象映射</summary>
        public override void CreateMap()
        {
            CreateMap<CodeModule, CodeModule>().ForMember(vm => vm.Namespace, opt => opt.Ignore())
                .ForMember(vm => vm.Project, opt => opt.Ignore());
            
            CreateMap<CodeEntity, CodeEntity>().ForMember(vm => vm.Module, opt => opt.Ignore());
            
            CreateMap<CodeProperty, CodeProperty>().ForMember(vm => vm.Entity, opt => opt.Ignore());
        }
    }
}
