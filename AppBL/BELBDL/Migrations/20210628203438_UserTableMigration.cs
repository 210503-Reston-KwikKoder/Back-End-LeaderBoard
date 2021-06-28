using Microsoft.EntityFrameworkCore.Migrations;

namespace BELBDL.Migrations
{
    public partial class UserTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LeaderBoards");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "LeaderBoards");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    AuthId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.AuthId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LeaderBoards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "LeaderBoards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
