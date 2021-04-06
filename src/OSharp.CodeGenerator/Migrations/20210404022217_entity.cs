using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OSharp.CodeGenerator.Migrations
{
    public partial class entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "CodeGen_CodeProperty",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "CodeGen_CodeProperty",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "CodeGen_CodeProject",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "CodeGen_CodeModule",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "CodeGen_CodeModule",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "CodeGen_CodeEntity",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "CodeGen_CodeProject");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "CodeGen_CodeModule");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "CodeGen_CodeModule");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "CodeGen_CodeEntity");
        }
    }
}
