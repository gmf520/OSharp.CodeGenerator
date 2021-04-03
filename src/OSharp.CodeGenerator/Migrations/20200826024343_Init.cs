using System;
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
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TypeName = table.Column<string>(nullable: false),
                    AuditEnabled = table.Column<bool>(nullable: false),
                    PropertyJson = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_EntityInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Function",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    IsController = table.Column<bool>(nullable: false),
                    IsAjax = table.Column<bool>(nullable: false),
                    AccessType = table.Column<int>(nullable: false),
                    IsAccessTypeChanged = table.Column<bool>(nullable: false),
                    AuditOperationEnabled = table.Column<bool>(nullable: false),
                    AuditEntityEnabled = table.Column<bool>(nullable: false),
                    CacheExpirationSeconds = table.Column<int>(nullable: false),
                    IsCacheSliding = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeGen_CodeProject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    NamespacePrefix = table.Column<string>(nullable: true),
                    SiteUrl = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    Copyright = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGen_CodeProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Systems_KeyValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ValueJson = table.Column<string>(nullable: true),
                    ValueType = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems_KeyValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeGen_CodeModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    PrimaryKeyTypeFullName = table.Column<string>(nullable: true),
                    IsDataAuth = table.Column<bool>(nullable: false),
                    ModuleId = table.Column<Guid>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TypeName = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: true),
                    MaxLength = table.Column<int>(nullable: true),
                    MinLength = table.Column<int>(nullable: true),
                    IsNullable = table.Column<bool>(nullable: false),
                    IsVirtual = table.Column<bool>(nullable: false),
                    IsForeignKey = table.Column<bool>(nullable: false),
                    IsInputDto = table.Column<bool>(nullable: false),
                    IsOutputDto = table.Column<bool>(nullable: false),
                    EntityId = table.Column<Guid>(nullable: false)
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
