// -----------------------------------------------------------------------
//  <copyright file="IGenerateManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-08 21:39</last-date>
// -----------------------------------------------------------------------

using System.Threading.Tasks;


namespace OSharp.CodeGeneration.Generates
{
    /// <summary>
    /// 定义代码生成管理器
    /// </summary>
    public interface IGenerateManager
    {
        /// <summary>
        /// 开始生成代码
        /// </summary>
        /// <returns></returns>
        Task Start();
    }
}
