﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OSharp.Entity;

namespace OSharp.CodeGenerator.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    partial class DefaultDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("OSharp.Authorization.EntityInfos.EntityInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("AuditEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PropertyJson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TypeName")
                        .IsUnique()
                        .HasDatabaseName("ClassFullNameIndex");

                    b.ToTable("Auth_EntityInfo");
                });

            modelBuilder.Entity("OSharp.Authorization.Functions.Function", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Action")
                        .HasColumnType("TEXT");

                    b.Property<string>("Area")
                        .HasColumnType("TEXT");

                    b.Property<bool>("AuditEntityEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AuditOperationEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CacheExpirationSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Controller")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAccessTypeChanged")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAjax")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCacheSliding")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsController")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSlaveDatabase")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Area", "Controller", "Action")
                        .IsUnique()
                        .HasDatabaseName("AreaControllerActionIndex");

                    b.ToTable("Auth_Function");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Addable")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deletable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Display")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasCreatedTime")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasCreationAudited")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasLocked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasSoftDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasUpdateAudited")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDataAuth")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Listable")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PrimaryKeyTypeFullName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Updatable")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("CodeGen_CodeEntity");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeModule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Display")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("CodeGen_CodeModule");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Company")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Copyright")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Creator")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("NamespacePrefix")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CodeGen_CodeProject");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataAuthFlag")
                        .HasColumnType("TEXT");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Display")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Filterable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsForeignKey")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsInputDto")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNavigation")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNullable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOutputDto")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsRequired")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxLength")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MinLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RelateEntity")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Sortable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Updatable")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("CodeGen_CodeProperty");
                });

            modelBuilder.Entity("OSharp.Core.Systems.KeyValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ValueJson")
                        .HasColumnType("TEXT");

                    b.Property<string>("ValueType")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasDatabaseName("KeyIndex");

                    b.ToTable("Systems_KeyValue");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeEntity", b =>
                {
                    b.HasOne("OSharp.CodeGeneration.Services.Entities.CodeModule", "Module")
                        .WithMany("Entities")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeModule", b =>
                {
                    b.HasOne("OSharp.CodeGeneration.Services.Entities.CodeProject", "Project")
                        .WithMany("Modules")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeProperty", b =>
                {
                    b.HasOne("OSharp.CodeGeneration.Services.Entities.CodeEntity", "Entity")
                        .WithMany("Properties")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeEntity", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeModule", b =>
                {
                    b.Navigation("Entities");
                });

            modelBuilder.Entity("OSharp.CodeGeneration.Services.Entities.CodeProject", b =>
                {
                    b.Navigation("Modules");
                });
#pragma warning restore 612, 618
        }
    }
}
