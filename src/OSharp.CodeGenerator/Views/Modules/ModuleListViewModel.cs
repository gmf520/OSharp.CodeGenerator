// -----------------------------------------------------------------------
//  <copyright file="ModuleListViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-06 12:33</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

using Microsoft.Extensions.DependencyInjection;

using OSharp.CodeGeneration.Data;
using OSharp.CodeGeneration.Entities;
using OSharp.CodeGenerator.Data;
using OSharp.CodeGenerator.Views.Entities;
using OSharp.CodeGenerator.Views.Projects;
using OSharp.Exceptions;
using OSharp.Mapping;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views.Modules
{
    [Singleton]
    public class ModuleListViewModel : Screen
    {
        private readonly IServiceProvider _provider;

        public ModuleListViewModel(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ProjectViewModel Project { get; set; }

        public IObservableCollection<ModuleViewModel> Modules { get; } = new BindableCollection<ModuleViewModel>();

        public string EditTitle { get; set; }

        public bool IsShowEdit { get; set; }

        public ModuleViewModel EditingModel { get; set; }

        public void Init()
        {
            if (Project == null)
            {
                throw new OsharpException("当前项目为空，请先通过菜单“项目-项目管理”加载项目");
            }
            List<CodeModule> entities = new List<CodeModule>();
            _provider.ExecuteScopedWork(provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                entities = contract.CodeModules.Where(m => m.ProjectId == Project.Id).ToList();
            });
            Modules.Clear();
            foreach (CodeModule entity in entities)
            {
                ModuleViewModel model = _provider.GetRequiredService<ModuleViewModel>();
                model = entity.MapTo(model);
                model.Project = Project;
                Modules.Add(model);
            }
            Helper.Output($"模块列表刷新成功，共{Modules.Count}个模块");
        }

        public void New()
        {
            if (Project == null)
            {
                throw new OsharpException("当前项目为空，请先通过菜单“项目-项目管理”加载项目");
            }
            ModuleViewModel model = IoC.Get<ModuleViewModel>();
            model.Project = Project;
            EditingModel = model;
            EditTitle = $"新增模块，项目：{Project.Name}";
            IsShowEdit = true;
        }

        public void Select(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            ModuleViewModel module = e.AddedItems[0] as ModuleViewModel;
            if (module == null)
            {
                return;
            }

            EntityListViewModel list = IoC.Get<EntityListViewModel>();
            list.Module = module;
            list.Init();
            Helper.Output($"切换到“{module.Name} [{module.Display}]”模块");
        }
    }
}
