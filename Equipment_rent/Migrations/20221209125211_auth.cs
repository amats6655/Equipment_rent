using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equipment_rent.Migrations
{
    public partial class auth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_respId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Auth_user",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_user", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_user_respId",
                table: "Orders",
                column: "user_respId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Auth_user_user_respId",
                table: "Orders",
                column: "user_respId",
                principalTable: "Auth_user",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Auth_user_user_respId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Auth_user");

            migrationBuilder.DropIndex(
                name: "IX_Orders_user_respId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "user_respId",
                table: "Orders");
        }
    }
}
