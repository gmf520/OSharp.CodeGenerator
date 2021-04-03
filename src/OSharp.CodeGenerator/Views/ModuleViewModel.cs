// -----------------------------------------------------------------------
//  <copyright file="ModuleViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-07 0:17</last-date>
// -----------------------------------------------------------------------

using FluentValidation;

using OSharp.CodeGeneration.Entities;
using OSharp.Mapping;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [MapTo(typeof(CodeModule))]
    public class ModuleViewModel : Screen
    {
        /// <summary>
        /// 初始化一个<see cref="ModuleViewModel"/>类型的新实例
        /// </summary>
        public ModuleViewModel(IModelValidator<ModuleViewModel> validator) : base(validator)
        {
            Validate();
            Entities = new BindableCollection<EntityViewModel>();
        }

        public ProjectViewModel Project { get; set; }

        public IObservableCollection<EntityViewModel> Entities { get; set; }

        public string Name { get; set; }

        public string Display { get; set; }

        public string Namespace => $"{(Project == null ? "" : Project.NamespacePrefix + ".")}{Name}";

        public CodeModule ToModule()
        {
            CodeModule module = this.MapTo<CodeModule>();
            return module;
        }
    }


    public class ModuleViewModelValidator : AbstractValidator<ModuleViewModel>
    {
        public ModuleViewModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("模块名称不能为空")
                .Matches("^[a-zA-Z_\u2E80-\u9FFF][0-9a-zA-Z_\u2E80-\u9FFF]*$").WithMessage("模块名称不符合标识符命名规则，只能是字母、数值、下划线、汉字，并且不能以数值开关");
        }
    }
}
