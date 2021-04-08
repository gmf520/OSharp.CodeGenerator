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
        public  Task<bool> CheckCodePropertyExists(Expression<Func<CodeProperty, bool>> predicate, Guid id = default)
        {
            return PropertyRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 更新实体属性信息信息
        /// </summary>
        /// <param name="entities">包含更新信息的实体属性信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateCodeProperties(params CodeProperty[] entities)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (var entity in entities)
            {
                entity.Validate();
                CodeEntity module = await EntityRepository.GetAsync(entity.EntityId);
                if (module == null)
                {
                    return new OperationResult(OperationResultType.Error, $"编号为“{entity.EntityId}”的实体信息不存在");
                }

                if (await CheckCodePropertyExists(m => m.Name == entity.Name && m.EntityId == entity.EntityId, entity.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"实体“{module.Name}”中名称为“{entity.Name}”的属性信息已存在");
                }

                int count;
                if (entity.Id == default)
                {
                    count = await PropertyRepository.InsertAsync(entity);
                }
                else
                {
                    CodeProperty entity1 = await PropertyRepository.GetAsync(entity.Id);
                    entity1 = entity.MapTo(entity1);
                    count = await PropertyRepository.UpdateAsync(entity1);
                }

                if (count > 0)
                {
                    names.Add(entity.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"属性“{names.ExpandAndToString()}”更新成功")
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
                var entity = await PropertyRepository.GetAsync(id);
                if (entity == null)
                {
                    continue;
                }
                int count = await PropertyRepository.DeleteAsync(entity);
                if (count > 0)
                {
                    names.Add(entity.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"属性“{names.ExpandAndToString()}”删除成功")
                : OperationResult.NoChanged;
        }
    }
}
