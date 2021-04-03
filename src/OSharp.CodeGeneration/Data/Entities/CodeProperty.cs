// -----------------------------------------------------------------------
//  <copyright file="CodeProperty.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2020 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2020-05-04 23:06</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OSharp.Entity;


namespace OSharp.CodeGeneration.Entities
{
    /// <summary>
    /// 实体类：代码实体属性信息
    /// </summary>
    [Description("代码实体属性")]
    [TableNamePrefix("CodeGen")]
    public class CodeProperty : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 属性名称
        /// </summary>
        [Required(), StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 属性类型名称
        /// </summary>
        [Required(), StringLength(500)]
        public string TypeName { get; set; }

        /// <summary>
        /// 获取或设置 显示名称
        /// </summary>
        [Required(), StringLength(200)]
        public string Display { get; set; }

        /// <summary>
        /// 获取或设置 是否必须
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// 获取或设置 最大长度
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// 获取或设置 最小长度
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// 获取或设置 是否值类型可空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 获取或设置 是否虚属性
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 获取或设置 是否外键
        /// </summary>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// 获取或设置 是否包含在输入Dto
        /// </summary>
        public bool IsInputDto { get; set; } = true;

        /// <summary>
        /// 获取或设置 是否包含在输出Dto
        /// </summary>
        public bool IsOutputDto { get; set; } = true;

        /// <summary>
        /// 获取或设置 实体编号
        /// </summary>
        public Guid EntityId { get; set; }

        /// <summary>
        /// 获取或设置 所属实体信息
        /// </summary>
        public virtual CodeEntity Entity { get; set; }
    }
}
