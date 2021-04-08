// -----------------------------------------------------------------------
//  <copyright file="CodeEntity.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-04 23:06</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using OSharp.Entity;
using OSharp.Mapping;


namespace OSharp.CodeGeneration.Services.Entities
{
    /// <summary>
    /// 实体类：代码实体信息
    /// </summary>
    [Description("代码实体信息")]
    [TableNamePrefix("CodeGen")]
    [MapTo(typeof(CodeEntity))]
    public class CodeEntity : EntityBase<Guid>, ILockable
    {
        /// <summary>
        /// 获取或设置 类型名称
        /// </summary>
        [Required(), StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 类型显示名称
        /// </summary>
        [Required(), StringLength(200)]
        public string Display { get; set; }

        /// <summary>
        /// 获取或设置 主键类型全名
        /// </summary>
        [Required(), StringLength(500)]
        public string PrimaryKeyTypeFullName { get; set; }

        /// <summary>
        /// 获取或设置 是否数据权限控制
        /// </summary>
        public bool IsDataAuth { get; set; }

        /// <summary>获取或设置 创建时间</summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>获取或设置 是否锁定当前信息</summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 所属模块编号
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// 获取或设置 所属模块
        /// </summary>
        public virtual CodeModule Module { get; set; }

        /// <summary>
        /// 获取或设置 实体的属性集合
        /// </summary>
        public virtual ICollection<CodeProperty> Properties { get; set; } = new List<CodeProperty>();
    }
}
