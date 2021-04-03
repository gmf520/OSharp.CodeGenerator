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
using System.IO;

using FluentValidation;

using Notifications.Wpf.Core;

using OSharp.CodeGeneration.Entities;
using OSharp.Extensions;
using OSharp.Json;
using OSharp.Mapping;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [MapTo(typeof(CodeProject))]
    public class ProjectViewModel : Screen
    {
        public ProjectViewModel(IModelValidator<ProjectViewModel> validator) : base(validator)
        {
            Validate();
            Modules = new BindableCollection<ModuleViewModel>();
        }

        public IObservableCollection<ModuleViewModel> Modules { get; set; }

        public string Name { get; set; } = "示例项目";

        public string NamespacePrefix { get; set; } = "Liuliu.Demo";

        public string Company { get; set; } = "柳柳软件";

        public string SiteUrl { get; set; } = "https://www.osharp.org";

        public string Creator { get; set; } = "郭明锋";

        public string Copyright { get; set; } = "Copyright OSHARP.ORG @2020";

        public bool CanSaveProject => !HasErrors;

        public void SaveProject()
        {
            MainViewModel main = IoC.Get<MainViewModel>();
            if (!Validate())
            {
                main.Notify("项目信息验证失败", NotificationType.Warning);
                return;
            }

            CodeProject project = ToProject();
            string json = project.ToJsonString();
            File.WriteAllText("1.txt", json);
            main.Notify($"项目“{Name}[{NamespacePrefix}]”保存成功", NotificationType.Success);
            main.IsProjectOpen = false;
        }

        public void CloseProject()
        {
            MainViewModel main = IoC.Get<MainViewModel>();
            main.IsProjectOpen = false;
        }

        /// <summary>
        /// Called whenever the error state of any properties changes. Calls NotifyOfPropertyChange("HasErrors") by default
        /// </summary>
        /// <param name="changedProperties">List of property names which have changed validation state</param>
        protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
        {
            base.OnValidationStateChanged(changedProperties);

            // Fody 无法编织其他组件，所以我们必须手动提高这个值
            this.NotifyOfPropertyChange(() => CanSaveProject);
        }

        public CodeProject ToProject()
        {
            CodeProject project = this.MapTo<CodeProject>();
            return project;
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
