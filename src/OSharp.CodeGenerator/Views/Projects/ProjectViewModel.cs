// -----------------------------------------------------------------------
//  <copyright file="ProjectViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-04 10:54</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using Notifications.Wpf.Core;

using OSharp.CodeGeneration.Services;
using OSharp.CodeGeneration.Services.Dtos;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGenerator.Data;
using OSharp.Data;
using OSharp.Extensions;
using OSharp.Mapping;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views.Projects
{
    [MapTo(typeof(CodeProjectInputDto))]
    [MapFrom(typeof(CodeProject))]
    public class ProjectViewModel : Screen
    {
        private readonly IServiceProvider _serviceProvider;

        public ProjectViewModel(IModelValidator<ProjectViewModel> validator, IServiceProvider serviceProvider) : base(validator)
        {
            _serviceProvider = serviceProvider;
            Validate();
        }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NamespacePrefix { get; set; }

        public string Company { get; set; }

        public string SiteUrl { get; set; }

        public string Creator { get; set; }

        public string Copyright { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public void Load()
        {
            MainViewModel main = IoC.Get<MainViewModel>();
            main.DisplayName = $"OSharp代码生成器 - {Name}";
            MenuViewModel menu = IoC.Get<MenuViewModel>();
            menu.Project = this;
            menu.Init();
            main.ProjectList.IsShow = false;
            main.StatusBar.Message = $"项目“{Name}”加载成功";
        }

        public void Edit()
        {
            ProjectListViewModel model = IoC.Get<ProjectListViewModel>();
            model.EditingModel = this;
            model.EditTitle = $"项目编辑 - {Name}";
            model.IsShowEdit = true;
        }

        public async void Delete()
        {
            if (MessageBox.Show($"是否删除项目“{Name}”?", "请确认", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            {
                return;
            }
            OperationResult result = null;
            await _serviceProvider.ExecuteScopedWorkAsync(async provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                result = await contract.DeleteCodeProjects(Id);
            });
            Helper.Notify(result);
            if (!result.Succeeded)
            {
                return;
            }
            MainViewModel main = IoC.Get<MainViewModel>();
            main.ProjectList.Init();
        }

        public bool CanEditSave => !HasErrors;

        public async void EditSave()
        {
            MainViewModel main = IoC.Get<MainViewModel>();
            if (!await ValidateAsync())
            {
                await main.Notify("项目信息验证失败", NotificationType.Warning);
                return;
            }

            CodeProjectInputDto dto = this.MapTo<CodeProjectInputDto>();
            OperationResult result = null;
            await _serviceProvider.ExecuteScopedWorkAsync(async provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                result = dto.Id == default
                    ? await contract.CreateCodeProjects(dto)
                    : await contract.UpdateCodeProjects(dto);
            });
            Helper.Notify(result);
            if (!result.Succeeded)
            {
                return;
            }

            ProjectListViewModel list = main.ProjectList;
            list.EditingModel = null;
            list.IsShowEdit = false;
            list.Init();
        }

        public void EditCancel()
        {
            var list = IoC.Get<ProjectListViewModel>();
            list.EditingModel = null;
            list.IsShowEdit = false;
            list.Init();
        }

        /// <summary>
        /// Called whenever the error state of any properties changes. Calls NotifyOfPropertyChange("HasErrors") by default
        /// </summary>
        /// <param name="changedProperties">List of property names which have changed validation state</param>
        protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
        {
            base.OnValidationStateChanged(changedProperties);

            // Fody 无法编织其他组件，所以我们必须手动提高这个值
            this.NotifyOfPropertyChange(() => CanEditSave);
        }
        
    }


    public class ProjectViewModelValidator : AbstractValidator<ProjectViewModel>
    {
        public ProjectViewModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("项目名称不能为空")
                .Matches("^[a-zA-Z_\u2E80-\u9FFF][0-9a-zA-Z_\u2E80-\u9FFF]*$").WithMessage("项目名称不符合标识符命名规则，只能是字母、数值、下划线、汉字，并且不能以数值开关");
            RuleFor(m => m.NamespacePrefix).NotEmpty().WithMessage("命名空间前缀不能为空");
            RuleFor(m => m.SiteUrl).Must(m => m.IsNullOrEmpty() || new UrlAttribute().IsValid(m)).WithMessage("网站Url不符合URL格式");
        }
    }
}
