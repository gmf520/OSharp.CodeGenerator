// -----------------------------------------------------------------------
//  <copyright file="DataService.Module.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-06 12:49</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.CodeGeneration.Services.Entities;
using OSharp.Collections;
using OSharp.Data;
using OSharp.Extensions;
using OSharp.Mapping;


namespace OSharp.CodeGeneration.Services
{
    public partial class DataService
    {
        /// <summary>
        /// 获取 代码模块信息查询数据集
        /// </summary>
        public IQueryable<CodeModule> CodeModules => ModuleRepository.QueryAsNoTracking();

        /// <summary>
        /// 检查代码模块信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的代码模块信息编号</param>
        /// <returns>代码模块信息是否存在</returns>
        public Task<bool> CheckCodeModuleExists(Expression<Func<CodeModule, bool>> predicate, Guid id = default)
        {
            return ModuleRepository.CheckExistsAsync(predicate, id);
        }
        
        /// <summary>
        /// 更新代码模块信息信息
        /// </summary>
        /// <param name="modules">包含更新信息的代码模块信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateCodeModules(params CodeModule[] modules)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (CodeModule module in modules)
            {
                module.Validate();
                CodeProject project = await ProjectRepository.GetAsync(module.ProjectId);
                if (project == null)
                {
                    return new OperationResult(OperationResultType.Error, $"编号为“{module.ProjectId}”的项目信息不存在");
                }

                if (await CheckCodeModuleExists(m => m.Name == module.Name && m.ProjectId == module.ProjectId, module.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"项目“{project.Name}”中名称为“{module.Name}”的模块信息已存在");
                }

                int count;
                if (module.Id == default)
                {
                    count = await ModuleRepository.InsertAsync(module);
                }
                else
                {
                    CodeModule existing = await ModuleRepository.GetAsync(module.Id);
                    existing = module.MapTo(existing);
                    count = await ModuleRepository.UpdateAsync(existing);
                }
                if (count > 0)
                {
                    names.Add(module.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"模块“{names.ExpandAndToString()}”保存成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除代码模块信息信息
        /// </summary>
        /// <param name="ids">要删除的代码模块信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteCodeModules(params Guid[] ids)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (var id in ids)
            {
                var module = ModuleRepository.Query(m => m.Id == id).Select(m => new { D = m, EntityCount = m.Entities.Count() })
                    .FirstOrDefault();
                if (module == null)
                {
                    return null;
                }

                if (module.EntityCount > 0)
                {
                    return new OperationResult(OperationResultType.Error, $"模块“{module.D.Name}”包含着 {module.EntityCount} 个实体，请先删除下属实体信息");
                }

                int count = await ModuleRepository.DeleteAsync(module.D);
                if (count > 0)
                {
                    names.Add(module.D.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"模块“{names.ExpandAndToString()}”删除成功")
                : OperationResult.NoChanged;
        }
    }
}
