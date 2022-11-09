using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equipment_rent.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BgColor",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Character",
                table: "Users",
                type: "nvarchar(1)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Character",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");
        }
    }
}
