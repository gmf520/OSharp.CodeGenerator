// -----------------------------------------------------------------------
//  <copyright file="NavViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-10 11:52</last-date>
// -----------------------------------------------------------------------

using System;
using System.Windows;

using MahApps.Metro.IconPacks;

using Microsoft.Extensions.DependencyInjection;

using OSharp.CodeGeneration.Services;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGenerator.Data;
using OSharp.CodeGenerator.Views.Entities;
using OSharp.CodeGenerator.Views.Modules;
using OSharp.CodeGenerator.Views.Projects;
using OSharp.CodeGenerator.Views.Properties;
using OSharp.Exceptions;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [Singleton]
    public class MenuViewModel : Screen
    {
        private readonly IServiceProvider _provider;

        public MenuViewModel(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ProjectViewModel Project { get; set; }

        public IObservableCollection<MenuItem> MenuItems { get; set; } = new BindableCollection<MenuItem>();

        public Screen SelectModel { get; set; }

        public void Init()
        {
            if (Project == null)
            {
                throw new OsharpException("当前项目为空，请先通过菜单“项目-项目管理”加载项目");
            }

            CodeProject[] projects = null;
            _provider.ExecuteScopedWork(provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                projects = contract.GetCodeProject(m => m.Name == Project.Name);
            });
            if (projects == null)
            {
                throw new OsharpException($"名称为“{Project.Name}”的项目信息不存在");
            }

            MenuItems.Clear();
            foreach (CodeProject project in projects)
            {
                MenuItems.Add(ToMenu(project));
            }
        }

        public void Select(RoutedPropertyChangedEventArgs<object> args)
        {
            if (args.NewValue is not MenuItem item)
            {
                return;
            }

            SelectModel = item.Screen;
            MainViewModel main = IoC.Get<MainViewModel>();
            main.ModuleList.IsShow = SelectModel is ProjectViewModel;
            main.EntityList.IsShow = SelectModel is ModuleViewModel;
            main.PropertyList.IsShow = SelectModel is EntityViewModel;
            switch (SelectModel)
            {
                case ProjectViewModel project:
                    ModuleListViewModel list1 = IoC.Get<ModuleListViewModel>();
                    list1.Project = project;
                    list1.Init();
                    break;
                case ModuleViewModel module:
                    EntityListViewModel list2 = IoC.Get<EntityListViewModel>();
                    list2.Module = module;
                    list2.Init();
                    break;
                case EntityViewModel entity:
                    PropertyListViewModel list3 = IoC.Get<PropertyListViewModel>();
                    list3.Entity = entity;
                    list3.Init();
                    break;
            }
        }
        
        private MenuItem ToMenu(CodeProject project)
        {
            MenuItem projectMenu = _provider.GetRequiredService<MenuItem>();
            projectMenu.Id = project.Id;
            projectMenu.Text = $"{project.Name}[{project.NamespacePrefix}]";
            projectMenu.Type = MenuItemType.Project;
            projectMenu.Icon = PackIconMaterialKind.AlphaPBoxOutline;
            ProjectViewModel projectModel = project.ToViewModel();
            projectMenu.Screen = projectModel;
            foreach (CodeModule module in project.Modules)
            {
                MenuItem moduleMenu = _provider.GetRequiredService<MenuItem>();
                moduleMenu.Id = module.Id;
                moduleMenu.Text = $"{module.Display}[{module.Name}]";
                moduleMenu.Type = MenuItemType.Module;
                moduleMenu.Icon = PackIconMaterialKind.AlphaMBoxOutline;
                ModuleViewModel moduleModel = module.ToViewModel(projectModel);
                moduleMenu.Screen = moduleModel;
                foreach (CodeEntity entity in module.Entities)
                {
                    MenuItem entityMenu = _provider.GetRequiredService<MenuItem>();
                    entityMenu.Id = entity.Id;
                    entityMenu.Text = $"{entity.Display}[{entity.Name}]";
                    entityMenu.Type = MenuItemType.Entity;
                    entityMenu.Icon = PackIconMaterialKind.AlphaEBoxOutline;
                    EntityViewModel entityModel = entity.ToViewModel(moduleModel);
                    entityMenu.Screen = entityModel;
                    moduleMenu.ItemMenus.Add(entityMenu);
                }
                projectMenu.ItemMenus.Add(moduleMenu);
            }

            return projectMenu;
        }
    }


    public class MenuItem : Screen
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 显示
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 获取或设置 图标
        /// </summary>
        public PackIconMaterialKind Icon { get; set; }

        /// <summary>
        /// 获取或设置 节点类型
        /// </summary>
        public MenuItemType Type { get; set; }

        /// <summary>
        /// 获取或设置 当前关联视图模型
        /// </summary>
        public Screen Screen { get; set; }

        /// <summary>
        /// 获取或设置 子节点集合
        /// </summary>
        public IObservableCollection<MenuItem> ItemMenus { get; set; } = new BindableCollection<MenuItem>();
    }


    public enum MenuItemType
    {
        Project,

        Module,

        Entity,

        Property
    }
}
