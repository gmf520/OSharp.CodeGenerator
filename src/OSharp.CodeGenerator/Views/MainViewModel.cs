// -----------------------------------------------------------------------
//  <copyright file="MainViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-03 14:53</last-date>
// -----------------------------------------------------------------------

using Notifications.Wpf.Core;
using Notifications.Wpf.Core.Controls;

using OSharp.CodeGenerator.Views.Entities;
using OSharp.CodeGenerator.Views.Modules;
using OSharp.CodeGenerator.Views.Projects;
using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [Singleton]
    public class MainViewModel : Screen
    {
        private readonly INotificationManager _notificationManager;

        /// <summary>
        /// 初始化一个<see cref="MainViewModel"/>类型的新实例
        /// </summary>
        public MainViewModel()
        {
            DisplayName = "OSharp代码生成器";
            _notificationManager = new NotificationManager(NotificationPosition.BottomRight);
        }
        
        public MainMenuViewModel MainMenu { get; set; } = IoC.Get<MainMenuViewModel>();

        public StatusBarViewModel StatusBar { get; set; } = IoC.Get<StatusBarViewModel>();

        public ProjectListViewModel ProjectList { get; set; } = IoC.Get<ProjectListViewModel>();

        public ModuleListViewModel ModuleList { get; set; } = IoC.Get<ModuleListViewModel>();

        public EntityListViewModel EntityList { get; set; } = IoC.Get<EntityListViewModel>();

        public async void Notify(string message, NotificationType type = NotificationType.Information, string title = "消息提示")
        {
            NotificationContent content = new NotificationContent()
            {
                Title = title,
                Message = message,
                Type = type
            };
            await _notificationManager.ShowAsync(content, "MainNotifyArea");
        }
    }
}
