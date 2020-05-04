// -----------------------------------------------------------------------
//  <copyright file="KeyValueConfiguration.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-05 0:00</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OSharp.Core.Systems;
using OSharp.Entity;


namespace OSharp.CodeGeneration.Data.Entities
{
    public class KeyValueConfiguration : EntityTypeConfigurationBase<KeyValue, Guid>
    {
        /// <summary>重写以实现实体类型各个属性的数据库配置</summary>
        /// <param name="builder">实体类型创建器</param>
        public override void Configure(EntityTypeBuilder<KeyValue> builder)
        { }
    }
}
