// -----------------------------------------------------------------------
//  <copyright file="EntityViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-07 1:11</last-date>
// -----------------------------------------------------------------------

using FluentValidation;

using OSharp.CodeGeneration.Entities;
using OSharp.Mapping;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [MapTo(typeof(CodeEntity))]
    public class EntityViewModel : Screen
    {
        /// <summary>
        /// 初始化一个<see cref="EntityViewModel"/>类型的新实例
        /// </summary>
        public EntityViewModel(IModelValidator<EntityViewModel> validator)
            : base(validator)
        {
            Validate();
            Properties = new BindableCollection<PropertyViewModel>();
        }

        public ModuleViewModel Module { get; set; }

        public IObservableCollection<PropertyViewModel> Properties { get; set; }

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

        public CodeEntity ToEntity()
        {
            CodeEntity entity = this.MapTo<CodeEntity>();
            return entity;
        }
    }


    public class EntityViewModelValidator : AbstractValidator<ModuleViewModel>
    {
        public EntityViewModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("实体类名称不能为空")
                .Matches("^[a-zA-Z_\u2E80-\u9FFF][0-9a-zA-Z_\u2E80-\u9FFF]*$").WithMessage("实体名称不符合标识符命名规则，只能是字母、数值、下划线、汉字，并且不能以数值开关");
        }
    }
}
