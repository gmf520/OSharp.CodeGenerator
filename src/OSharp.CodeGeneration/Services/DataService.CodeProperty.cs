// -----------------------------------------------------------------------
//  <copyright file="DataService.CodeProperty.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-08 23:10</last-date>
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
        /// 获取 实体属性信息查询数据集
        /// </summary>
        public IQueryable<CodeProperty> CodeProperties => PropertyRepository.QueryAsNoTracking();

        /// <summary>
        /// 检查实体属性信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的实体属性信息编号</param>
        /// <returns>实体属性信息是否存在</returns>
        public Task<bool> CheckCodePropertyExists(Expression<Func<CodeProperty, bool>> predicate, Guid id = default)
        {
            return PropertyRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 更新实体属性信息信息
        /// </summary>
        /// <param name="properties">包含更新信息的实体属性信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateCodeProperties(params CodeProperty[] properties)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (var property in properties)
            {
                property.Validate();
                CodeEntity entity = await EntityRepository.GetAsync(property.EntityId);
                if (entity == null)
                {
                    return new OperationResult(OperationResultType.Error, $"编号为“{property.EntityId}”的实体信息不存在");
                }

                if (await CheckCodePropertyExists(m => m.Name == property.Name && m.EntityId == property.EntityId, property.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"实体“{entity.Name}”中名称为“{property.Name}”的属性信息已存在");
                }

                if (property.Order == 0)
                {
                    property.Order = PropertyRepository.Query(m => m.EntityId == entity.Id).Count() + 1;
                }
                int count;
                if (property.Id == default)
                {
                    count = await PropertyRepository.InsertAsync(property);
                }
                else
                {
                    CodeProperty existing = await PropertyRepository.GetAsync(property.Id);
                    existing = property.MapTo(existing);
                    count = await PropertyRepository.UpdateAsync(existing);
                }

                if (count > 0)
                {
                    names.Add(property.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"属性“{names.ExpandAndToString()}”保存成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除实体属性信息信息
        /// </summary>
        /// <param name="ids">要删除的实体属性信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteCodeProperties(params Guid[] ids)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (var id in ids)
            {
                var property = await PropertyRepository.GetAsync(id);
                if (property == null)
                {
                    continue;
                }

                int count = await PropertyRepository.DeleteAsync(property);
                if (count > 0)
                {
                    names.Add(property.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"属性“{names.ExpandAndToString()}”删除成功")
                : OperationResult.NoChanged;
        }
    }
}
