using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowy.Core.Migrations
{
    /// <inheritdoc />
    public partial class camundaRefFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyProcessDefinition",
                table: "Processes",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "KeyProcessInstance",
                table: "Instances",
                newName: "Key");

            migrationBuilder.AddColumn<string>(
                name: "BpmnProcessId",
                table: "Processes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Processes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Processes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Instances",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Instances",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InsatnceDatas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdInsatnce = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsatnceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsatnceDatas_Instances_IdInsatnce",
                        column: x => x.IdInsatnce,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_InsatnceDatas_IdInsatnce",
                table: "InsatnceDatas",
                column: "IdInsatnce");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsatnceDatas");

            migrationBuilder.DropColumn(
                name: "BpmnProcessId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Instances");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Instances");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Processes",
                newName: "KeyProcessDefinition");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Instances",
                newName: "KeyProcessInstance");
        }
    }
}
