// -----------------------------------------------------------------------
//  <copyright file="ProjectViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-04 10:54</last-date>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Data;

using FluentValidation;

using Stylet;


namespace OSharp.CodeGenerator.Views.Project
{
    [Singleton]
    public class ProjectViewModel : Screen
    {
        public string Name { get; set; }

        public string NamespacePrefix { get; set; }

        public string Company { get; set; }

        public string SiteUrl { get; set; }

        public string Creator { get; set; }

        public string Copyright { get; set; }

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
