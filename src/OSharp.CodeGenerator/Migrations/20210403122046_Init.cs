﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSharp.CodeGenerator.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auth_EntityInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false),
                    AuditEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    PropertyJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_EntityInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Function",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Area = table.Column<string>(type: "TEXT", nullable: true),
                    Controller = table.Column<string>(type: "TEXT", nullable: true),
                    Action = table.Column<string>(type: "TEXT", nullable: true),
                    IsController = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAjax = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAccessTypeChanged = table.Column<bool>(type: "INTEGER", nullable: false),
                    AuditOperationEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AuditEntityEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    CacheExpirationSeconds = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCacheSliding = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSlaveDatabase = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeGen_CodeProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    NamespacePrefix = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Company = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    SiteUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Creator = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Copyright = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGen_CodeProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Systems_KeyValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ValueJson = table.Column<string>(type: "TEXT", nullable: true),
                    ValueType = table.Column<string>(type: "TEXT", nullable: true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems_KeyValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeGen_CodeModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Display = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGen_CodeModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeGen_CodeModule_CodeGen_CodeProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "CodeGen_CodeProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodeGen_CodeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Display = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PrimaryKeyTypeFullName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsDataAuth = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGen_CodeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeGen_CodeEntity_CodeGen_CodeModule_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "CodeGen_CodeModule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodeGen_CodeProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Display = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: true),
                    MaxLength = table.Column<int>(type: "INTEGER", nullable: true),
                    MinLength = table.Column<int>(type: "INTEGER", nullable: true),
                    IsNullable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsVirtual = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsForeignKey = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInputDto = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsOutputDto = table.Column<bool>(type: "INTEGER", nullable: false),
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGen_CodeProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeGen_CodeProperty_CodeGen_CodeEntity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "CodeGen_CodeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ClassFullNameIndex",
                table: "Auth_EntityInfo",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AreaControllerActionIndex",
                table: "Auth_Function",
                columns: new[] { "Area", "Controller", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeGen_CodeEntity_ModuleId",
                table: "CodeGen_CodeEntity",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeGen_CodeModule_ProjectId",
                table: "CodeGen_CodeModule",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeGen_CodeProperty_EntityId",
                table: "CodeGen_CodeProperty",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "KeyIndex",
                table: "Systems_KeyValue",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth_EntityInfo");

            migrationBuilder.DropTable(
                name: "Auth_Function");

            migrationBuilder.DropTable(
                name: "CodeGen_CodeProperty");

            migrationBuilder.DropTable(
                name: "Systems_KeyValue");

            migrationBuilder.DropTable(
                name: "CodeGen_CodeEntity");

            migrationBuilder.DropTable(
                name: "CodeGen_CodeModule");

            migrationBuilder.DropTable(
                name: "CodeGen_CodeProject");
        }
    }
}