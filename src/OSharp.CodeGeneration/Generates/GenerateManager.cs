// -----------------------------------------------------------------------
//  <copyright file="GenerateManager.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-08 21:39</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using OSharp.CodeGeneration.Services.Entities;


namespace OSharp.CodeGeneration.Generates
{
    /// <summary>
    /// 代码生成管理器
    /// </summary>
    public class GenerateManager : IGenerateManager
    {
        private readonly IServiceProvider _provider;

        public GenerateManager(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 开始生成代码
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {
            throw new NotImplementedException();
        }
        
    }
}
