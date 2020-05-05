// -----------------------------------------------------------------------
//  <copyright file="ProjectViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-04 10:54</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

using FluentValidation;

using HandyControl.Controls;
using HandyControl.Data;

using OSharp.Extensions;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    public class ProjectViewModel : Screen
    {
        public ProjectViewModel(IModelValidator<ProjectViewModel> validator) : base(validator)
        {
            Validate();
        }

        public string Name { get; set; } = "项目名称";

        public string NamespacePrefix { get; set; } = "Liuliu.Demo";

        public string Company { get; set; }

        public string SiteUrl { get; set; } = "https://www.osharp.org";

        public string Creator { get; set; }

        public string Copyright { get; set; }

        public Func<string, OperationResult<bool>> CheckName { get; set; } = name =>
        {
            if (string.IsNullOrEmpty(name))
            {
                return OperationResult.Failed("项目名称不能为空");
            }

            if (!name.IsMatch("^[a-zA-Z_\u2E80-\u9FFF][0-9a-zA-Z_\u2E80-\u9FFF]*$"))
            {
                return OperationResult.Failed("项目名称不符合标识符命名规则");
            }

            return OperationResult.Success();
        };

        public bool CanSaveProject => !HasErrors;

        public void SaveProject()
        {
            if (!Validate())
            {
                Growl.Warning("数据验证失败，请检查！");
                return;
            }
            IoC.Get<StatusBarViewModel>().Message = "保存项目: " + Name;
        }

        public bool CanCloseDrawer => true;
        public void CloseDrawer()
        {
            MainViewModel main = IoC.Get<MainViewModel>();
            main.IsProjectOpen = false;
        }
    }


    public class ProjectViewModelValidator : AbstractValidator<ProjectViewModel>
    {
        public ProjectViewModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("项目名称不能为空")
                .Matches("^[a-zA-Z_\u2E80-\u9FFF][0-9a-zA-Z_\u2E80-\u9FFF]*$").WithMessage("项目名称不符合标识符命名规则,只能是字母、数值、下划线、汉字，并且不能以数值开关");
            RuleFor(m => m.NamespacePrefix).NotEmpty().WithMessage("命名空间前缀不能为空");
            RuleFor(m => m.SiteUrl).Must(m => new UrlAttribute().IsValid(m)).WithMessage("网站Url不符合URL格式");
        }
    }
}
