// -----------------------------------------------------------------------
//  <copyright file="MainViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-03 14:53</last-date>
// -----------------------------------------------------------------------

using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [Singleton]
    public class MainViewModel : Screen
    {
        /// <summary>
        /// 初始化一个<see cref="MainViewModel"/>类型的新实例
        /// </summary>
        public MainViewModel()
        {
            DisplayName = "OSharp代码生成器";
        }

        public bool IsProjectOpen { get; set; } = false;

        public MainMenuViewModel MainMenu { get; set; } = IoC.Get<MainMenuViewModel>();

        public StatusBarViewModel StatusBar { get; set; } = IoC.Get<StatusBarViewModel>();

        public ProjectViewModel Project { get; set; } = IoC.Get<ProjectViewModel>();
    }
}
