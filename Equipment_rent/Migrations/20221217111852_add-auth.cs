using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equipment_rent.Migrations
{
    public partial class addauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auth_user_Auth_role_Auth_roleId",
                table: "Auth_user");

            migrationBuilder.AlterColumn<Guid>(
                name: "Auth_roleId",
                table: "Auth_user",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Auth_user_Auth_role_Auth_roleId",
                table: "Auth_user",
                column: "Auth_roleId",
                principalTable: "Auth_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auth_user_Auth_role_Auth_roleId",
                table: "Auth_user");

            migrationBuilder.AlterColumn<Guid>(
                name: "Auth_roleId",
                table: "Auth_user",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Auth_user_Auth_role_Auth_roleId",
                table: "Auth_user",
                column: "Auth_roleId",
                principalTable: "Auth_role",
                principalColumn: "Id");
        }
    }
}
