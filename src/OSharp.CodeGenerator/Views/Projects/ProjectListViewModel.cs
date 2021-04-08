// -----------------------------------------------------------------------
//  <copyright file="ProjectList.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-04 0:32</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using OSharp.CodeGeneration.Services;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.Mapping;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views.Projects
{
    [Singleton]
    public class ProjectListViewModel : Screen
    {
        private readonly IServiceProvider _provider;

        public ProjectListViewModel(IServiceProvider provider)
        {
            _provider = provider;
        }

        public bool IsShow { get; set; }

        public IObservableCollection<ProjectViewModel> Projects { get; } = new BindableCollection<ProjectViewModel>();

        public string EditTitle { get; set; }

        public bool IsShowEdit { get; set; }

        public ProjectViewModel EditingModel { get; set; }

        public void Show()
        {
            Init();
            IsShow = true;
        }

        public void Init()
        {
            List<CodeProject> projects = new List<CodeProject>();
            _provider.ExecuteScopedWork(provider =>
            {
                Projects.Clear();
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                projects = contract.CodeProjects.ToList();
            });
            foreach (CodeProject project in projects)
            {
                ProjectViewModel model = _provider.GetRequiredService<ProjectViewModel>();
                model = project.MapTo(model);
                Projects.Add(model);
            }
        }

        public void New()
        {
            ProjectListViewModel model = IoC.Get<ProjectListViewModel>();
            model.EditingModel = IoC.Get<ProjectViewModel>();
            model.EditTitle = "新增项目";
            model.IsShowEdit = true;
        }
    }
    
}
