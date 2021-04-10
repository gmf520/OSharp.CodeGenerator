// -----------------------------------------------------------------------
//  <copyright file="ModuleViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-07 0:17</last-date>
// -----------------------------------------------------------------------

using System;
using System.Windows;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using OSharp.CodeGeneration.Services;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGenerator.Data;
using OSharp.CodeGenerator.Views.Projects;
using OSharp.Data;
using OSharp.Mapping;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views.Modules
{
    [MapTo(typeof(CodeModule))]
    [MapFrom(typeof(CodeModule))]
    public class ModuleViewModel : Screen
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 初始化一个<see cref="ModuleViewModel"/>类型的新实例
        /// </summary>
        public ModuleViewModel(IModelValidator<ModuleViewModel> validator, IServiceProvider provider) : base(validator)
        {
            _provider = provider;
            Validate();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Display { get; set; }

        public int Order { get; set; }

        public bool IsLocked { get; set; }

        public DateTime CreatedTime { get; set; }

        public string Namespace => $"{(Project == null ? "" : Project.NamespacePrefix + ".")}{Name}";

        public ProjectViewModel Project { get; set; }

        public CodeModule ToModule()
        {
            CodeModule module = this.MapTo<CodeModule>();
            return module;
        }
        
        public async void Delete()
        {
            if (MessageBox.Show($"是否删除模块“{Name}[{Display}]”?", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            {
                return;
            }

            OperationResult result = null;
            await _provider.ExecuteScopedWorkAsync(async provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                result = await contract.DeleteCodeModules(Id);
            });
            MainViewModel main = IoC.Get<MainViewModel>();
            Helper.Notify(result);
            if (!result.Succeeded)
            {
                return;
            }
            
            main.ModuleList.Init();
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
