// -----------------------------------------------------------------------
//  <copyright file="EntityViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-07 1:11</last-date>
// -----------------------------------------------------------------------

using System;

using FluentValidation;

using OSharp.CodeGeneration.Services.Dtos;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGenerator.Data;
using OSharp.CodeGenerator.Views.Modules;
using OSharp.Mapping;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views.Entities
{
    [MapTo(typeof(CodeEntityInputDto))]
    [MapFrom(typeof(CodeEntity))]
    public class EntityViewModel : Screen
    {
        private readonly IServiceProvider _provider;
        
        public EntityViewModel(IModelValidator<EntityViewModel> validator, IServiceProvider provider)
            : base(validator)
        {
            _provider = provider;
            Validate();
        }

        public ModuleViewModel Module { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Display { get; set; }

        public string PrimaryKeyTypeFullName { get; set; }

        public bool Listable { get; set; }

        public bool Addable { get; set; }

        public bool Updatable { get; set; }

        public bool Deletable { get; set; }

        public bool IsDataAuth { get; set; }

        public bool HasCreatedTime { get; set; }

        public bool HasLocked { get; set; }

        public bool HasSoftDeleted { get; set; }

        public bool HasCreationAudited { get; set; }

        public bool HasUpdateAudited { get; set; }

        public int Order { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public void ForeignKey()
        {
            ForeignListViewModel list = IoC.Get<ForeignListViewModel>();
            list.Entity = this;
            list.Title = $"实体“{Display}[{Name}]”的外键管理";
            list.Init();
            list.IsShow = true;
        }

        public void Up()
        {
            Helper.Output($"“{Name}” - Up");
        }

        public void Down()
        {
            Helper.Output($"“{Name}” - Down");
        }

        public void Delete()
        {
            Helper.Output($"“{Name}” - Delete");
        }
    }


    public class EntityViewModelValidator : AbstractValidator<EntityViewModel>
    {
        public EntityViewModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("实体类名称不能为空")
                .Matches("^[a-zA-Z_\u2E80-\u9FFF][0-9a-zA-Z_\u2E80-\u9FFF]*$").WithMessage("实体名称不符合标识符命名规则，只能是字母、数值、下划线、汉字，并且不能以数值开关");
        }
    }
}
