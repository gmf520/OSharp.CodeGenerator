using Microsoft.EntityFrameworkCore.Migrations;

namespace OSharp.CodeGenerator.Migrations
{
    public partial class AddOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Display",
                table: "CodeGen_CodeProperty",
                type: "TEXT",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "DataAuthFlag",
                table: "CodeGen_CodeProperty",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultValue",
                table: "CodeGen_CodeProperty",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Filterable",
                table: "CodeGen_CodeProperty",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNavigation",
                table: "CodeGen_CodeProperty",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CodeGen_CodeProperty",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RelateEntity",
                table: "CodeGen_CodeProperty",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sortable",
                table: "CodeGen_CodeProperty",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Updatable",
                table: "CodeGen_CodeProperty",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CodeGen_CodeModule",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Addable",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deletable",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCreatedTime",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCreationAudited",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLocked",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSoftDeleted",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasUpdateAudited",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Listable",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Updatable",
                table: "CodeGen_CodeEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAuthFlag",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "DefaultValue",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "Filterable",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "IsNavigation",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "RelateEntity",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "Sortable",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "Updatable",
                table: "CodeGen_CodeProperty");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CodeGen_CodeModule");

            migrationBuilder.DropColumn(
                name: "Addable",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "Deletable",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "HasCreatedTime",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "HasCreationAudited",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "HasLocked",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "HasSoftDeleted",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "HasUpdateAudited",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "Listable",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CodeGen_CodeEntity");

            migrationBuilder.DropColumn(
                name: "Updatable",
                table: "CodeGen_CodeEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Display",
                table: "CodeGen_CodeProperty",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
