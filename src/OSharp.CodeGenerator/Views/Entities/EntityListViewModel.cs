// -----------------------------------------------------------------------
//  <copyright file="EntityListViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-06 19:10</last-date>
// -----------------------------------------------------------------------

using System;

using OSharp.CodeGenerator.Views.Modules;

using Stylet;


namespace OSharp.CodeGenerator.Views.Entities
{
    public class EntityListViewModel : Screen
    {
        private readonly IServiceProvider _provider;

        public EntityListViewModel(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ModuleViewModel Module { get; set; }

        public IObservableCollection<EntityViewModel> Entities { get; } = new BindableCollection<EntityViewModel>();

        public void Init()
        {

        }

        public void New()
        {

        }

        #region 实体编辑

        public string EditTitle { get; set; } = "实体编辑";
        
        #endregion
    }
}
