// -----------------------------------------------------------------------
//  <copyright file="MainMenuViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-03 18:09</last-date>
// -----------------------------------------------------------------------

using System;
using System.Windows;

using OSharp.Wpf.Stylet;

using Stylet;


namespace OSharp.CodeGenerator.Views
{
    [Singleton]
    public class MainMenuViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly StatusBarViewModel _statusBar;

        public MainMenuViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _statusBar = IoC.Get<StatusBarViewModel>();
        }

        #region 文件

        public void New()
        {
            //_windowManager.ShowDialog(IoC.Get<MainViewModel>().Project);
            MainViewModel main = IoC.Get<MainViewModel>();
            main.IsProjectOpen = true;
        }

        public void Open()
        {
            _statusBar.Message = "打开项目";
        }

        public void Save()
        {
            _statusBar.Message = "保存项目";
        }

        public void SaveAs()
        {
            _statusBar.Message = "项目另存为";
        }

        public void Exit()
        {
            Application.Current.MainWindow?.Close();
        }

        #endregion

        #region 项目

        public void Project()
        {
            
        }

        public void Module()
        {
            
        }

        public void Template()
        {
            
        }

        #endregion

        #region 设置

        public void GlobalTemplate()
        {
            
        }

        #endregion

        #region 帮助

        public void Github()
        {
            
        }

        public void About()
        {
            
        }

        #endregion
    }
}
