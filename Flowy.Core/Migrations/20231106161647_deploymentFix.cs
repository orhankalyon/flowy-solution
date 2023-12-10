using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowy.Core.Migrations
{
    /// <inheritdoc />
    public partial class deploymentFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BpmnProcessId",
                table: "Deployments",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BpmnProcessId",
                table: "Deployments");
        }
    }
}
