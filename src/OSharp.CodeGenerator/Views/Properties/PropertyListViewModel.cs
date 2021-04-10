// -----------------------------------------------------------------------
//  <copyright file="PropertyListViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-08 13:44</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Notifications.Wpf.Core;

using OSharp.CodeGeneration.Services;
using OSharp.CodeGeneration.Services.Entities;
using OSharp.CodeGenerator.Data;
using OSharp.CodeGenerator.Views.Entities;
using OSharp.Data;
using OSharp.Exceptions;
using OSharp.Mapping;

using Stylet;


namespace OSharp.CodeGenerator.Views.Properties
{
    public class PropertyListViewModel : Screen
    {
        private readonly IServiceProvider _provider;

        public PropertyListViewModel(IServiceProvider provider)
        {
            _provider = provider;
        }

        public EntityViewModel Entity { get; set; }

        public IObservableCollection<PropertyViewModel> Properties { get; set; } = new BindableCollection<PropertyViewModel>();

        public void Init()
        {
            if (Entity == null)
            {
                throw new OsharpException("当前实体为空，请选择一个实体");
            }

            List<CodeProperty> properties = new List<CodeProperty>();
            _provider.ExecuteScopedWork(provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                properties = contract.CodeProperties.Where(m => m.EntityId == Entity.Id).OrderBy(m => m.Order).ToList();
            });
            Properties.Clear();
            foreach (CodeProperty property in properties)
            {
                PropertyViewModel model = _provider.GetRequiredService<PropertyViewModel>();
                model = property.MapTo(model);
                model.Entity = Entity;
                Properties.Add(model);
            }
            Helper.Output($"实体“{Entity.Display}”的属性列表刷新成功，共{Properties.Count}个属性");
        }

        public void New()
        {
            PropertyViewModel property = _provider.GetRequiredService<PropertyViewModel>();
            property.Entity = Entity;
            Properties.Add(property);
        }

        public bool CanSave => Properties.All(m => !m.HasErrors);

        public async void Save()
        {
            if (!CanSave)
            {
                Helper.Notify("属性信息验证失败", NotificationType.Warning);
                return;
            }

            CodeProperty[] properties = Properties.Select(m => m.ToProperty()).ToArray();
            OperationResult result = null;
            await _provider.ExecuteScopedWorkAsync(async provider =>
            {
                IDataContract contract = provider.GetRequiredService<IDataContract>();
                result = await contract.UpdateCodeProperties(properties);
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
