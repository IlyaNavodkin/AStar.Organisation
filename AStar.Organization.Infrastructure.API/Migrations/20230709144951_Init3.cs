using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AStar.Organisation.Infrastructure.API.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Positions_Name",
                table: "Positions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Name_DepartmentId",
                table: "Positions",
                columns: new[] { "Name", "DepartmentId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Positions_Name_DepartmentId",
                table: "Positions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Positions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Name",
                table: "Positions",
                column: "Name");
        }
    }
}
