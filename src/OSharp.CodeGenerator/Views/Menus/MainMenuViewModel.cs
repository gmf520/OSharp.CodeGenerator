// -----------------------------------------------------------------------
//  <copyright file="MainMenuViewModel.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-03 18:09</last-date>
// -----------------------------------------------------------------------

using System.Net.Mime;
using System.Windows;

using OSharp.Wpf.Stylet;

using Stylet;

using StyletIoC;


namespace OSharp.CodeGenerator.Views
{
    [Singleton]
    public class MainMenuViewModel : Screen
    {
        private StatusBarViewModel _statusBar;

        public MainMenuViewModel()
        {
            _statusBar = IoC.Get<StatusBarViewModel>();
        }

        #region 文件

        public void New()
        {
            _statusBar.Message = "新建项目";
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
