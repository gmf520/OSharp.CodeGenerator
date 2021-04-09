using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSharp.CodeGenerator.Migrations
{
    public partial class SettingProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "CodeGen_CodeSetting",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeGen_CodeSetting_ProjectId",
                table: "CodeGen_CodeSetting",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeGen_CodeSetting_CodeGen_CodeProject_ProjectId",
                table: "CodeGen_CodeSetting",
                column: "ProjectId",
                principalTable: "CodeGen_CodeProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeGen_CodeSetting_CodeGen_CodeProject_ProjectId",
                table: "CodeGen_CodeSetting");

            migrationBuilder.DropIndex(
                name: "IX_CodeGen_CodeSetting_ProjectId",
                table: "CodeGen_CodeSetting");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "CodeGen_CodeSetting");
        }
    }
}
