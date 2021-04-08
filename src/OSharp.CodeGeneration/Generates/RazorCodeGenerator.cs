// -----------------------------------------------------------------------
//  <copyright file="RazorCodeGenerator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-08 21:39</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiniRazor;

using OSharp.CodeGeneration.Services.Entities;
using OSharp.Exceptions;


namespace OSharp.CodeGeneration.Generates
{
    /// <summary>
    /// Razor代码生成器
    /// </summary>
    public class RazorCodeGenerator : ICodeGenerator
    {
        /// <summary>
        /// 生成项目所有代码
        /// </summary>
        /// <param name="codeConfigs">要处理的代码配置集合</param>
        /// <param name="project">代码项目信息</param>
        /// <returns>输出的代码文件信息集合</returns>
        public async Task<CodeFile[]> GenerateCodes(GenCodeConfig[] codeConfigs, CodeProject project)
        {
            List<CodeFile> codeFiles = new List<CodeFile>();
            CodeModule[] modules = project.Modules.ToArray();
            CodeEntity[] entities = modules.SelectMany(m => m.Entities).ToArray();

            foreach (GenCodeConfig codeConfig in codeConfigs.Where(m => m.MetadataType == MetadataType.Entity))
            {
                foreach (CodeEntity entity in entities)
                {
                    codeFiles.Add(await GenerateCode(codeConfig, entity));
                }
            }

            foreach (GenCodeConfig codeConfig in codeConfigs.Where(m => m.MetadataType == MetadataType.Module))
            {
                foreach (CodeModule module in modules)
                {
                    codeFiles.Add(await GenerateCode(codeConfig, module));
                }
            }

            foreach (GenCodeConfig codeConfig in codeConfigs.Where(m => m.MetadataType == MetadataType.Project))
            {
                codeFiles.Add(await GenerateCode(codeConfig, project));
            }

            return codeFiles.OrderBy(m => m.FileName).ToArray();
        }

        /// <summary>
        /// 生成项目元数据相关代码
        /// <param name="codeConfig">代码配置</param>
        /// <param name="project">项目元数据</param>
        /// </summary>
        public Task<CodeFile> GenerateCode(GenCodeConfig codeConfig, CodeProject project)
        {
            string fileName = codeConfig.GetCodeFileName(project);
            return GenerateCodeCore(codeConfig, project, fileName);
        }

        /// <summary>
        /// 生成模块元数据相关代码
        /// <param name="codeConfig">代码配置</param>
        /// <param name="module">模块元数据</param>
        /// </summary>
        public Task<CodeFile> GenerateCode(GenCodeConfig codeConfig, CodeModule module)
        {
            string fileName = codeConfig.GetCodeFileName(module);
            return GenerateCodeCore(codeConfig, module, fileName);
        }

        /// <summary>
        /// 生成实体元数据相关代码
        /// <param name="codeConfig">代码配置</param>
        /// <param name="entity">实体元数据</param>
        /// </summary>
        public Task<CodeFile> GenerateCode(GenCodeConfig codeConfig, CodeEntity entity)
        {
            string fileName = codeConfig.GetCodeFileName(entity);
            return GenerateCodeCore(codeConfig, entity, fileName);
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="codeConfig">代码配置</param>
        /// <param name="model">代码数据模型</param>
        /// <param name="fileName">代码输出文件</param>
        /// <returns></returns>
        protected virtual async Task<CodeFile> GenerateCodeCore(GenCodeConfig codeConfig, object model, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            await using TextWriter writer = new StringWriter(sb);
            if (codeConfig.TemplateType == null)
            {
                if (codeConfig.TemplateFile == null || !File.Exists(codeConfig.TemplateFile))
                {
                    throw new OsharpException($"代码配置“{codeConfig.Name}”的模板文件“{codeConfig.TemplateFile}”不存在");
                }

                string templateSource = await File.ReadAllTextAsync(codeConfig.TemplateFile);
                TemplateDescriptor descriptor = Razor.Compile(templateSource);
                await descriptor.RenderAsync(writer, model);
            }
            else
            {
                ITemplate template = (ITemplate)(Activator.CreateInstance(codeConfig.TemplateType)
                    ?? throw new OsharpException($"代码配置“{codeConfig.Name}”的模板类型实例化失败"));
                template.Model = model;
                template.Output = writer;
                await template.ExecuteAsync();
            }

            string codeSource = sb.ToString();

            CodeFile codeFile = new CodeFile()
            {
                CodeConfig = codeConfig,
                SourceCode = codeSource,
                FileName = fileName
            };
            return codeFile;
        }
    }
}
