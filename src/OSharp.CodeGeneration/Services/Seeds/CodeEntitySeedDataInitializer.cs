// -----------------------------------------------------------------------
//  <copyright file="CodeEntitySeedDataInitializer.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-09 17:14</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.Extensions.DependencyInjection;

using OSharp.CodeGeneration.Services.Entities;
using OSharp.Entity;


namespace OSharp.CodeGeneration.Services.Seeds
{
    public class CodeEntitySeedDataInitializer : SeedDataInitializerBase<CodeEntity, Guid>
    {
        /// <summary>
        /// 初始化一个<see cref="T:OSharp.Entity.SeedDataInitializerBase`2" />类型的新实例
        /// </summary>
        public CodeEntitySeedDataInitializer(IServiceProvider rootProvider)
            : base(rootProvider)
        { }

        /// <summary>获取 种子数据初始化的顺序</summary>
        public override int Order => 3;

        /// <summary>重写以提供要初始化的种子数据</summary>
        /// <returns></returns>
        protected override CodeEntity[] SeedData(IServiceProvider provider)
        {
            IRepository<CodeModule, Guid> repository = provider.GetRequiredService<IRepository<CodeModule, Guid>>();
            CodeModule module = repository.GetFirst(m => m.Name == "Identity");
            List<CodeEntity> entities = new List<CodeEntity>()
            {
                new CodeEntity(){Name = "User", Display = "用户", PrimaryKeyTypeFullName = "System.Int32", ModuleId = module.Id},
                new CodeEntity(){Name = "Role", Display = "角色", PrimaryKeyTypeFullName = "System.Int32", ModuleId = module.Id},
                new CodeEntity(){Name = "UserRole", Display = "用户角色", PrimaryKeyTypeFullName = "System.Int32", ModuleId = module.Id},
            };

            module = repository.GetFirst(m => m.Name == "Auth");
            entities.AddRange(new List<CodeEntity>()
            {
                new CodeEntity(){Name = "Module", Display = "模块", PrimaryKeyTypeFullName = "System.Int32", ModuleId = module.Id},
                new CodeEntity(){Name = "Function", Display = "功能", PrimaryKeyTypeFullName = "System.Guid", ModuleId = module.Id},
                new CodeEntity(){Name = "EntityInfo", Display = "实体", PrimaryKeyTypeFullName = "System.Guid", ModuleId = module.Id},
            });

            module = repository.GetFirst(m => m.Name == "Infos");
            entities.AddRange(new List<CodeEntity>()
            {
                new CodeEntity(){Name = "Article", Display = "文章", PrimaryKeyTypeFullName = "System.Int32", ModuleId = module.Id},
                new CodeEntity(){Name = "Message", Display = "站内信", PrimaryKeyTypeFullName = "System.Guid", ModuleId = module.Id}
            });

            return entities.ToArray();
        }

        /// <summary>重写以提供判断某个实体是否存在的表达式</summary>
        /// <param name="entity">要判断的实体</param>
        /// <returns></returns>
        protected override Expression<Func<CodeEntity, bool>> ExistingExpression(CodeEntity entity)
        {
            return m => m.Name == entity.Name && m.ModuleId == entity.ModuleId;
        }
    }
}