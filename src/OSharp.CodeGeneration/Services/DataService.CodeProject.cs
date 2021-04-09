using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using OSharp.CodeGeneration.Services.Entities;
using OSharp.Collections;
using OSharp.Data;
using OSharp.Extensions;
using OSharp.Json;


namespace OSharp.CodeGeneration.Services
{
    public partial class DataService
    {
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
        /// 获取指定条件的项目信息
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <returns>项目信息集合</returns>
        public CodeProject[] GetCodeProject(Expression<Func<CodeProject, bool>> predicate)
        {
            CodeProject[] projects = ProjectRepository.Query(predicate).ToArray();
            string json = projects.ToJsonString();
            projects = json.FromJsonString<CodeProject[]>();
            foreach (CodeProject project in projects)
            {
                foreach (CodeModule module in project.Modules)
                {
                    foreach (CodeEntity entity in module.Entities)
                    {
                        foreach (CodeProperty property in entity.Properties)
                        {
                            property.Entity = entity;
                        }

                        entity.Module = module;
                    }

                    module.Project = project;
                }
            }

            return projects;
        }

        /// <summary>
        /// 添加项目信息信息
        /// </summary>
        /// <param name="projects">要添加的项目信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> CreateCodeProjects(params CodeProject[] projects)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (CodeProject project in projects)
            {
                project.Validate();
                if (await CheckCodeProjectExists(m => m.Name == project.Name))
                {
                    return new OperationResult(OperationResultType.Error, $"名称为“{project.Name}”的项目信息已存在");
                }

                int count = await ProjectRepository.InsertAsync(project);
                if (count > 0)
                {
                    names.Add(project.Name);
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
        /// <param name="projects">包含更新信息的项目信息</param>
        /// <returns>业务操作结果</returns>
        public async Task<OperationResult> UpdateCodeProjects(params CodeProject[] projects)
        {
            List<string> names = new List<string>();
            UnitOfWork.EnableTransaction();
            foreach (CodeProject project in projects)
            {
                project.Validate();
                if (await CheckCodeProjectExists(m => m.Name == project.Name, project.Id))
                {
                    return new OperationResult(OperationResultType.Error, $"名称为“{project.Name}”的项目信息已存在");
                }

                int count = await ProjectRepository.UpdateAsync(project);
                if (count > 0)
                {
                    names.Add(project.Name);
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
                var project = ProjectRepository.Query(m => m.Id == id).Select(m => new { D = m, ModuleCount = m.Modules.Count() })
                    .FirstOrDefault();
                if (project == null)
                {
                    return null;
                }

                if (project.ModuleCount > 0)
                {
                    return new OperationResult(OperationResultType.Error, $"项目“{project.D.Name}”包含着 {project.ModuleCount} 个模块，请先删除下属模块信息");
                }

                int count = await ProjectRepository.DeleteAsync(project.D);
                if (count > 0)
                {
                    names.Add(project.D.Name);
                }
            }

            await UnitOfWork.CommitAsync();
            return names.Count > 0
                ? new OperationResult(OperationResultType.Success, $"项目“{names.ExpandAndToString()}”删除成功")
                : OperationResult.NoChanged;
        }
    }
}
