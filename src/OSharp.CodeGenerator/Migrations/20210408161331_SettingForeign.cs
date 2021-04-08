using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSharp.CodeGenerator.Migrations
{
    public partial class SettingForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeGen_CodeForeign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SelfNavigation = table.Column<string>(type: "TEXT", nullable: true),
                    SelfForeignKey = table.Column<string>(type: "TEXT", nullable: true),
                    OtherEntity = table.Column<string>(type: "TEXT", nullable: true),
                    OtherNavigation = table.Column<string>(type: "TEXT", nullable: true),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeleteBehavior = table.Column<int>(type: "INTEGER", nullable: true),
                    ForeignRelation = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGen_CodeForeign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeGen_CodeForeign_CodeGen_CodeEntity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "CodeGen_CodeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodeGen_CodeSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    MetadataType = table.Column<int>(type: "INTEGER", nullable: false),
                    TemplateFile = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    OutputFileFormat = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    IsOnce = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGen_CodeSetting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeGen_CodeForeign_EntityId",
                table: "CodeGen_CodeForeign",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeGen_CodeForeign");

            migrationBuilder.DropTable(
                name: "CodeGen_CodeSetting");
        }
    }
}
