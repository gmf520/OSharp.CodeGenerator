// -----------------------------------------------------------------------
//  <copyright file="ViewModelLocator.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2021 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2021-04-07 22:24</last-date>
// -----------------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore;

using OSharp.CodeGeneration.Services.Entities;


namespace OSharp.CodeGenerator.Views
{
    public class ViewModelLocator
    {
        /// <summary>
        /// 获取 实体主键类型数据源
        /// </summary>
        public string[] EntityKeys { get; } =
        {
            typeof(int).FullName,
            typeof(Guid).FullName,
            typeof(string).FullName,
            typeof(long).FullName
        };

        public string[] TypeNames { get; } =
        {
            typeof(string).FullName,
            typeof(int).FullName,
            typeof(bool).FullName,
            typeof(double).FullName,
            typeof(DateTime).FullName,
            typeof(Guid).FullName,
            typeof(long).FullName,
            "ICollection<>"
        };

        public ForeignRelation[] ForeignRelations { get; } = { ForeignRelation.ManyToOne, ForeignRelation.OneToMany, ForeignRelation.OneToOne, ForeignRelation.OwnsOne, ForeignRelation.OwnsMany };

        public DeleteBehavior?[] DeleteBehaviors { get; } =
            { null, DeleteBehavior.ClientSetNull, DeleteBehavior.Restrict, DeleteBehavior.SetNull, DeleteBehavior.Cascade };

    }
}
