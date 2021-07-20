using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaderboardDataLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaderBoards",
                columns: table => new
                {
                    AuthId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CatID = table.Column<int>(type: "int", nullable: false),
                    AverageWPM = table.Column<double>(type: "float", nullable: false),
                    AverageAcc = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderBoards", x => new { x.AuthId, x.CatID });
                });

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
                name: "LeaderBoards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
