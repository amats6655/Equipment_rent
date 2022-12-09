using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equipment_rent.Migrations
{
    public partial class auth_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Auth_user_user_respId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "user_respId",
                table: "Orders",
                newName: "Auth_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_user_respId",
                table: "Orders",
                newName: "IX_Orders_Auth_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Auth_user_Auth_userId",
                table: "Orders",
                column: "Auth_userId",
                principalTable: "Auth_user",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Auth_user_Auth_userId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Auth_userId",
                table: "Orders",
                newName: "user_respId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Auth_userId",
                table: "Orders",
                newName: "IX_Orders_user_respId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Auth_user_user_respId",
                table: "Orders",
                column: "user_respId",
                principalTable: "Auth_user",
                principalColumn: "Id");
        }
    }
}
