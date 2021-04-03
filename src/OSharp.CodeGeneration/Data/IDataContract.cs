using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        #region Project

        /// <summary>
        /// 创建代码工程
        /// </summary>
        /// <param name="project">工程信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> CreateProject(CodeProject project);

        /// <summary>
        /// 更新代码工程
        /// </summary>
        /// <param name="project">工程信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> UpdateProject(CodeProject project);

        /// <summary>
        /// 删除代码工程
        /// </summary>
        /// <param name="project">工程信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> DeleteProject(Guid id);

        #endregion
    }
}
