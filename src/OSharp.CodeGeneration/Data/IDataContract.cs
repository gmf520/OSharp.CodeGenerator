// -----------------------------------------------------------------------
//  <copyright file="IDataContract.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-06 12:45</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using OSharp.CodeGeneration.Entities;
using OSharp.Data;


namespace OSharp.CodeGeneration.Data
{
    /// <summary>
    /// 数据服务契约
    /// </summary>
    public interface IDataContract
    {
        #region 项目信息业务

        /// <summary>
        /// 获取 项目信息查询数据集
        /// </summary>
        IQueryable<CodeProject> CodeProjects { get; }

        /// <summary>
        /// 检查项目信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的项目信息编号</param>
        /// <returns>项目信息是否存在</returns>
        Task<bool> CheckCodeProjectExists(Expression<Func<CodeProject, bool>> predicate, Guid id = default);

        /// <summary>
        /// 添加项目信息信息
        /// </summary>
        /// <param name="entities">要添加的项目信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateCodeProjects(params CodeProject[] entities);

        /// <summary>
        /// 更新项目信息信息
        /// </summary>
        /// <param name="entities">包含更新信息的项目信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateCodeProjects(params CodeProject[] entities);

        /// <summary>
        /// 删除项目信息信息
        /// </summary>
        /// <param name="ids">要删除的项目信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteCodeProjects(params Guid[] ids);

        #endregion

        #region 代码模块信息业务

        /// <summary>
        /// 获取 代码模块信息查询数据集
        /// </summary>
        IQueryable<CodeModule> CodeModules { get; }

        /// <summary>
        /// 检查代码模块信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的代码模块信息编号</param>
        /// <returns>代码模块信息是否存在</returns>
        Task<bool> CheckCodeModuleExists(Expression<Func<CodeModule, bool>> predicate, Guid id = default);

        /// <summary>
        /// 添加代码模块信息信息
        /// </summary>
        /// <param name="entities">要添加的代码模块信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateCodeModules(params CodeModule[] entities);

        /// <summary>
        /// 更新代码模块信息信息
        /// </summary>
        /// <param name="entities">包含更新信息的代码模块信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateCodeModules(params CodeModule[] entities);

        /// <summary>
        /// 删除代码模块信息信息
        /// </summary>
        /// <param name="ids">要删除的代码模块信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteCodeModules(params Guid[] ids);

        #endregion

        #region 代码实体信息业务

        /// <summary>
        /// 获取 代码实体信息查询数据集
        /// </summary>
        IQueryable<CodeEntity> CodeEntities { get; }

        /// <summary>
        /// 检查代码实体信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的代码实体信息编号</param>
        /// <returns>代码实体信息是否存在</returns>
        Task<bool> CheckCodeEntityExists(Expression<Func<CodeEntity, bool>> predicate, Guid id = default);
        
        /// <summary>
        /// 更新代码实体信息信息
        /// </summary>
        /// <param name="entities">包含更新信息的代码实体信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateCodeEntities(params CodeEntity[] entities);

        /// <summary>
        /// 删除代码实体信息信息
        /// </summary>
        /// <param name="ids">要删除的代码实体信息编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteCodeEntities(params Guid[] ids);

        #endregion

    }
}
