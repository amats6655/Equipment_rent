using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Equipment_rent.Migrations
{
    public partial class manytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentOrder",
                columns: table => new
                {
                    EquipmentsEquipmentId = table.Column<int>(type: "int", nullable: false),
                    OrdersOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentOrder", x => new { x.EquipmentsEquipmentId, x.OrdersOrderId });
                    table.ForeignKey(
                        name: "FK_EquipmentOrder_Equipments_EquipmentsEquipmentId",
                        column: x => x.EquipmentsEquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentOrder_Orders_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentOrder_OrdersOrderId",
                table: "EquipmentOrder",
                column: "OrdersOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentOrder");
        }
    }
}
