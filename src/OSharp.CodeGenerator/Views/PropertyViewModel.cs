// -----------------------------------------------------------------------
//  <copyright file="PropertyViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-07 1:27</last-date>
// -----------------------------------------------------------------------

using FluentValidation;

using OSharp.CodeGeneration.Entities;
using OSharp.Mapping;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [MapTo(typeof(CodeProperty))]
    public class PropertyViewModel : Screen
    {
        /// <summary>
        /// 初始化一个<see cref="PropertyViewModel"/>类型的新实例
        /// </summary>
        public PropertyViewModel(IModelValidator<ProjectViewModel>validator) : base(validator)
        {
            Validate();
        }

        public EntityViewModel Entity { get; set; }

        public string Name { get; set; }

        public string Display { get; set; }

        public string TypeName { get; set; }

        public bool Readonly { get; set; }

        public bool Updatable { get; set; }

        public bool Sortable { get; set; }

        public bool Filterable { get; set; }

        public bool IsRequired { get; set; }

        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        public bool IsNullable { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsNavigation { get; set; }

        public string RelateEntity { get; set; }

        public string DataAuthFlag { get; set; }

        public bool IsInputDto { get; set; }

        public bool IsOutputDto { get; set; }

        public string DefaultValue { get; set; }

        public CodeProperty ToProperty()
        {
            CodeProperty property = this.MapTo<CodeProperty>();
            return property;
        }
    }


    public class PropertyViewModelValidator : AbstractValidator<ModuleViewModel>
    {
        public PropertyViewModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("实体类名称不能为空")
                .Matches("^[a-zA-Z_\u2E80-\u9FFF][0-9a-zA-Z_\u2E80-\u9FFF]*$").WithMessage("实体名称不符合标识符命名规则，只能是字母、数值、下划线、汉字，并且不能以数值开关");
        }
    }
}