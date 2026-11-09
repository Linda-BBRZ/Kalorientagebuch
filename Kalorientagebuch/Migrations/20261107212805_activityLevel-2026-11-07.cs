using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalorientagebuch.Migrations
{
    /// <inheritdoc />
    public partial class activityLevel20261107 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityLevelId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Factor = table.Column<double>(type: "float", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActivityLevelId",
                table: "Users",
                column: "ActivityLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ActivityLevels_ActivityLevelId",
                table: "Users",
                column: "ActivityLevelId",
                principalTable: "ActivityLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ActivityLevels_ActivityLevelId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ActivityLevels");

            migrationBuilder.DropIndex(
                name: "IX_Users_ActivityLevelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActivityLevelId",
                table: "Users");
        }
    }
}
