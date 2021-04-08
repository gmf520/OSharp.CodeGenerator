// -----------------------------------------------------------------------
//  <copyright file="EntityListViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-06 19:10</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Notifications.Wpf.Core;

using OSharp.CodeGeneration.Services;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGenerator.Data;
using OSharp.CodeGenerator.Views.Modules;
using OSharp.Data;
using OSharp.Exceptions;
using OSharp.Mapping;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views.Entities
{
    [Singleton]
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
            if (Module == null)
            {
                throw new OsharpException($"当前模块为空，请点击模块列表选择一行");
            }

            List<CodeEntity> entities = new List<CodeEntity>();
            _provider.ExecuteScopedWork(provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                entities = contract.CodeEntities.Where(m => m.ModuleId == Module.Id).ToList();
            });
            Entities.Clear();
            foreach (CodeEntity entity in entities)
            {
                EntityViewModel model = _provider.GetRequiredService<EntityViewModel>();
                model = entity.MapTo(model);
                model.Module = Module;
                Entities.Add(model);
            }
            Helper.Output($"模块“{Module.Display}”的实体列表刷新成功，共{Entities.Count}个实体");
        }

        public void New()
        {
            EntityViewModel entity = _provider.GetRequiredService<EntityViewModel>();
            entity.Module = Module;
            Entities.Add(entity);
        }

        public bool CanSave => Entities.All(m => !m.HasErrors);

        public async void Save()
        {
            if (!CanSave)
            {
                Helper.Notify("实体信息验证失败", NotificationType.Warning);
                return;
            }

            CodeEntity[] entities = Entities.Select(m => m.ToEntity()).ToArray();
            OperationResult result = null;
            await _provider.ExecuteScopedWorkAsync(async provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                result = await contract.UpdateCodeEntities(entities);
            });
            Helper.Notify(result);
            if (!result.Succeeded)
            {
                return;
            }
            Init();
        }

        /// <summary>
        /// Called whenever the error state of any properties changes. Calls NotifyOfPropertyChange("HasErrors") by default
        /// </summary>
        /// <param name="changedProperties">List of property names which have changed validation state</param>
        protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
        {
            base.OnValidationStateChanged(changedProperties);
            NotifyOfPropertyChange(() => CanSave);
        }
    }
}