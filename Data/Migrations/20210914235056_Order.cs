using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderProduct_GetOrderProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceAtOrder",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "GetOrderProductId",
                table: "Orders",
                newName: "OrderProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_GetOrderProductId",
                table: "Orders",
                newName: "IX_Orders_OrderProductId");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceWhenOrdered",
                table: "OrderProduct",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderProduct_OrderProductId",
                table: "Orders",
                column: "OrderProductId",
                principalTable: "OrderProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderProduct_OrderProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceWhenOrdered",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "OrderProductId",
                table: "Orders",
                newName: "GetOrderProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderProductId",
                table: "Orders",
                newName: "IX_Orders_GetOrderProductId");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAtOrder",
                table: "OrderProduct",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderProduct_GetOrderProductId",
                table: "Orders",
                column: "GetOrderProductId",
                principalTable: "OrderProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
