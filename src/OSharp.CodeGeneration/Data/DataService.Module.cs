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

using OSharp.CodeGeneration.Entities;
using OSharp.Collections;
using OSharp.Data;
using OSharp.Extensions;


namespace OSharp.CodeGeneration.Data
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
        /// 添加代码模块信息信息
        /// </summary>
        /// <param name="entities">要添加的代码模块信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> CreateCodeModules(params CodeModule[] entities)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (CodeModule entity in entities)
            {
                entity.Validate();
                CodeProject project = await ProjectRepository.GetAsync(entity.ProjectId);
                if (project == null)
                {
                    return new OperationResult(OperationResultType.Error, $"编号为“{entity.ProjectId}”的项目信息不存在");
                }

                if (await CheckCodeModuleExists(m => m.Name == entity.Name && m.ProjectId == entity.ProjectId))
                {
                    return new OperationResult(OperationResultType.Error, $"项目“{project.Name}”中名称为“{entity.Name}”的模块信息已存在");
                }

                int count = await ModuleRepository.InsertAsync(entity);
                if (count > 0)
                {
                    names.Add(entity.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return new OperationResult(OperationResultType.Success, $"模块“{names.ExpandAndToString()}”创建成功");
        }

        /// <summary>
        /// 更新代码模块信息信息
        /// </summary>
        /// <param name="entities">包含更新信息的代码模块信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateCodeModules(params CodeModule[] entities)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (CodeModule entity in entities)
            {
                entity.Validate();
                CodeProject project = await ProjectRepository.GetAsync(entity.ProjectId);
                if (project == null)
                {
                    return new OperationResult(OperationResultType.Error, $"编号为“{entity.ProjectId}”的项目信息不存在");
                }

                if (await CheckCodeModuleExists(m => m.Name == entity.Name && m.ProjectId == entity.ProjectId, entity.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"项目“{project.Name}”中名称为“{entity.Name}”的模块信息已存在");
                }

                int count = await ModuleRepository.UpdateAsync(entity);
                if (count > 0)
                {
                    names.Add(entity.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return new OperationResult(OperationResultType.Success, $"模块“{names.ExpandAndToString()}”创建成功");
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
                var entity = ModuleRepository.Query(m => m.Id == id).Select(m => new { D = m, EntityCount = m.Entities.Count() })
                    .FirstOrDefault();
                if (entity == null)
                {
                    return null;
                }

                if (entity.EntityCount > 0)
                {
                    return new OperationResult(OperationResultType.Error, $"模块“{entity.D.Name}”包含着 {entity.EntityCount} 个实体，请先删除下属实体信息");
                }

                int count = await ModuleRepository.DeleteAsync(entity.D);
                if (count > 0)
                {
                    names.Add(entity.D.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return new OperationResult(OperationResultType.Success, $"模块“{names.ExpandAndToString()}”删除成功");
        }
    }
}
