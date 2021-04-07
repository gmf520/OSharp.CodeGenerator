// -----------------------------------------------------------------------
//  <copyright file="DataService.Entity.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-07 1:20</last-date>
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
using OSharp.Mapping;


namespace OSharp.CodeGeneration.Data
{
    public partial class DataService
    {
        #region Implementation of IDataContract

        /// <summary>
        /// 获取 代码实体信息查询数据集
        /// </summary>
        public IQueryable<CodeEntity> CodeEntities => EntityRepository.QueryAsNoTracking();

        /// <summary>
        /// 检查代码实体信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的代码实体信息编号</param>
        /// <returns>代码实体信息是否存在</returns>
        public Task<bool> CheckCodeEntityExists(Expression<Func<CodeEntity, bool>> predicate, Guid id = default)
        {
            return EntityRepository.CheckExistsAsync(predicate, id);
        }
        
        /// <summary>
        /// 更新代码实体信息信息
        /// </summary>
        /// <param name="entities">包含更新信息的代码实体信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateCodeEntities(params CodeEntity[] entities)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (var entity in entities)
            {
                entity.Validate();
                CodeModule module = await ModuleRepository.GetAsync(entity.ModuleId);
                if (module == null)
                {
                    return new OperationResult(OperationResultType.Error, $"编号为“{entity.ModuleId}”的模块信息不存在");
                }

                if (await CheckCodeEntityExists(m => m.Name == entity.Name && m.ModuleId == entity.ModuleId, entity.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"模块“{module.Name}”中名称为“{entity.Name}”的实体信息已存在");
                }

                int count;
                if (entity.Id == default)
                {
                    count = await EntityRepository.InsertAsync(entity);
                }
                else
                {
                    CodeEntity entity1 = await EntityRepository.GetAsync(entity.Id);
                    entity1 = entity.MapTo(entity1);
                    count = await EntityRepository.UpdateAsync(entity1);
                }

                if (count > 0)
                {
                    names.Add(entity.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return new OperationResult(OperationResultType.Success, $"实体“{names.ExpandAndToString()}”更新成功");
        }

        /// <summary>
        /// 删除代码实体信息信息
        /// </summary>
        /// <param name="ids">要删除的代码实体信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteCodeEntities(params Guid[] ids)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (var id in ids)
            {
                var entity = EntityRepository.Query(m => m.Id == id).Select(m => new { D = m, PropertyCount = m.Properties.Count() })
                    .FirstOrDefault();
                if (entity == null)
                {
                    return null;
                }

                if (entity.PropertyCount > 0)
                {
                    return new OperationResult(OperationResultType.Error, $"实体“{entity.D.Name}”包含着 {entity.PropertyCount} 个属性，请先删除下属属性信息");
                }

                int count = await EntityRepository.DeleteAsync(entity.D);
                if (count > 0)
                {
                    names.Add(entity.D.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return new OperationResult(OperationResultType.Success, $"实体“{names.ExpandAndToString()}”删除成功");
        }

        #endregion
    }
}
