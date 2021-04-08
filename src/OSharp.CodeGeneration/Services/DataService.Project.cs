using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.CodeGeneration.Services.Entities;
using OSharp.Collections;
using OSharp.Data;
using OSharp.Extensions;


namespace OSharp.CodeGeneration.Services
{
    public partial class DataService
    {
        #region Implementation of IDataContract

        /// <summary>
        /// 获取 项目信息查询数据集
        /// </summary>
        public IQueryable<CodeProject> CodeProjects => ProjectRepository.QueryAsNoTracking();

        /// <summary>
        /// 检查项目信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的项目信息编号</param>
        /// <returns>项目信息是否存在</returns>
        public Task<bool> CheckCodeProjectExists(Expression<Func<CodeProject, bool>> predicate, Guid id = default)
        {
            return ProjectRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加项目信息信息
        /// </summary>
        /// <param name="entities">要添加的项目信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> CreateCodeProjects(params CodeProject[] entities)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (CodeProject entity in entities)
            {
                entity.Validate();
                if (await CheckCodeProjectExists(m => m.Name == entity.Name))
                {
                    return new OperationResult(OperationResultType.Error, $"名称为“{entity.Name}”的项目信息已存在");
                }

                int count = await ProjectRepository.InsertAsync(entity);
                if (count > 0)
                {
                    names.Add(entity.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"项目“{names.ExpandAndToString()}”创建成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 更新项目信息信息
        /// </summary>
        /// <param name="entities">包含更新信息的项目信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateCodeProjects(params CodeProject[] entities)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (CodeProject entity in entities)
            {
                entity.Validate();
                if (await CheckCodeProjectExists(m => m.Name == entity.Name, entity.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"名称为“{entity.Name}”的项目信息已存在");
                }

                int count = await ProjectRepository.UpdateAsync(entity);
                if (count > 0)
                {
                    names.Add(entity.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"项目“{names.ExpandAndToString()}”更新成功")
                : OperationResult.NoChanged;
        }

        /// <summary>
        /// 删除项目信息信息
        /// </summary>
        /// <param name="ids">要删除的项目信息编号</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> DeleteCodeProjects(params Guid[] ids)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (var id in ids)
            {
                var entity = ProjectRepository.Query(m => m.Id == id).Select(m => new { D = m, ModuleCount = m.Modules.Count() })
                    .FirstOrDefault();
                if (entity == null)
                {
                    return null;
                }

                if (entity.ModuleCount > 0)
                {
                    return new OperationResult(OperationResultType.Error, $"项目“{entity.D.Name}”包含着 {entity.ModuleCount} 个模块，请先删除下属模块信息");
                }

                int count = await ProjectRepository.DeleteAsync(entity.D);
                if (count > 0)
                {
                    names.Add(entity.D.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"项目“{names.ExpandAndToString()}”删除成功")
                : OperationResult.NoChanged;
        }

        #endregion
    }
}
