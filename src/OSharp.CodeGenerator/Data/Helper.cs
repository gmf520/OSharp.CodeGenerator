// -----------------------------------------------------------------------
//  <copyright file="Helper.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-07 19:29</last-date>
// -----------------------------------------------------------------------

using OSharp.CodeGenerator.Views;
using OSharp.Wpf.Stylet;


namespace OSharp.CodeGenerator.Data
{
    public static class Helper
    {
        /// <summary>
        /// 输出信息到状态栏
        /// </summary>
        /// <param name="message">消息</param>
        public static void Output(string message)
        {
            IoC.Get<StatusBarViewModel>().Message = message;
        }
    }
}
