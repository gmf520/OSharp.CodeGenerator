// -----------------------------------------------------------------------
//  <copyright file="AutoMapperConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-06 13:13</last-date>
// -----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

using AutoMapper.Configuration;

using OSharp.AutoMapper;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGenerator.Views;
using OSharp.CodeGenerator.Views.Entities;
using OSharp.CodeGenerator.Views.Modules;
using OSharp.CodeGenerator.Views.Properties;


namespace OSharp.CodeGenerator.Data
{
    public class AutoMapperConfiguration : AutoMapperTupleBase
    {
        /// <summary>创建对象映射</summary>
        public override void CreateMap()
        {
            CreateMap<ModuleViewModel, CodeModule>().ForMember(e => e.ProjectId, opt => opt.MapFrom(vm => vm.Project.Id))
                .ForMember(e => e.Project, opt => opt.Ignore());
            CreateMap<CodeModule, ModuleViewModel>().ForMember(vm => vm.Namespace, opt => opt.Ignore())
                .ForMember(vm => vm.Project, opt => opt.Ignore());
            CreateMap<CodeModule, CodeModule>().ForMember(vm => vm.Namespace, opt => opt.Ignore())
                .ForMember(vm => vm.Project, opt => opt.Ignore());

            CreateMap<EntityViewModel, CodeEntity>().ForMember(e => e.ModuleId, opt => opt.MapFrom(vm => vm.Module.Id))
                .ForMember(e => e.Module, opt => opt.Ignore());
            CreateMap<CodeEntity, EntityViewModel>().ForMember(vm => vm.Module, opt => opt.Ignore());
            CreateMap<CodeEntity, CodeEntity>().ForMember(vm => vm.Module, opt => opt.Ignore());

            CreateMap<PropertyViewModel, CodeProperty>().ForMember(e => e.EntityId, opt => opt.MapFrom(vm => vm.Entity.Id))
                .ForMember(e=>e.Entity, opt=>opt.Ignore());
            CreateMap<CodeProperty, PropertyViewModel>().ForMember(vm => vm.Entity, opt => opt.Ignore());
            CreateMap<CodeProperty, CodeProperty>().ForMember(vm => vm.Entity, opt => opt.Ignore());
        }
    }
}