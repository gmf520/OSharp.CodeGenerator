// -----------------------------------------------------------------------
//  <copyright file="CodeProjectTemplateConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-09 13:08</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OSharp.Entity;


namespace OSharp.CodeGeneration.Services.Entities
{
    public class CodeProjectTemplateConfiguration : EntityTypeConfigurationBase<CodeProjectTemplate, Guid>
    {
        /// <summary>重写以实现实体类型各个属性的数据库配置</summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<CodeProjectTemplate> builder)
        {
            builder.HasOne(m => m.Project).WithMany(n => n.ProjectTemplates).HasForeignKey(m => m.ProjectId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Template).WithMany(n => n.ProjectTemplates).HasForeignKey(m => m.TemplateId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
