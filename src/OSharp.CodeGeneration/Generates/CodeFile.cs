// -----------------------------------------------------------------------
//  <copyright file="CodeFile.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-08 21:39</last-date>
// -----------------------------------------------------------------------

using OSharp.CodeGeneration.Services.Entities;
using OSharp.Extensions;


namespace OSharp.CodeGeneration.Generates
{
    /// <summary>
    /// 表示代码文件信息
    /// </summary>
    public class CodeFile
    {
        private string _sourceCode;

        /// <summary>
        /// 获取或设置 代码配置
        /// </summary>
        public CodeTemplate Template { get; set; }

        /// <summary>
        /// 获取或设置 源代码字符串
        /// </summary>
        public string SourceCode
        {
            get => _sourceCode;
            set => _sourceCode = value.ToHtmlDecode();
        }

        /// <summary>
        /// 获取或设置 代码输出文件
        /// </summary>
        public string FileName { get; set; }
    }
}
