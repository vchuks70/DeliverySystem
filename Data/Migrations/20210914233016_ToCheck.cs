using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ToCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAndServices_Orders_OrderId",
                table: "ProductAndServices");

            migrationBuilder.RenameColumn(
                name: "RouteName",
                table: "Routes",
                newName: "StartLocation");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ProductAndServices",
                newName: "OrderProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAndServices_OrderId",
                table: "ProductAndServices",
                newName: "IX_ProductAndServices_OrderProductId");

            migrationBuilder.AddColumn<string>(
                name: "EndLocation",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimatedDeliveryTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryCompletedTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "GetOrderProductId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceAtOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GetOrderProductId",
                table: "Orders",
                column: "GetOrderProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderProduct_GetOrderProductId",
                table: "Orders",
                column: "GetOrderProductId",
                principalTable: "OrderProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAndServices_OrderProduct_OrderProductId",
                table: "ProductAndServices",
                column: "OrderProductId",
                principalTable: "OrderProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderProduct_GetOrderProductId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAndServices_OrderProduct_OrderProductId",
                table: "ProductAndServices");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GetOrderProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EndLocation",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "GetOrderProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StartLocation",
                table: "Routes",
                newName: "RouteName");

            migrationBuilder.RenameColumn(
                name: "OrderProductId",
                table: "ProductAndServices",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAndServices_OrderProductId",
                table: "ProductAndServices",
                newName: "IX_ProductAndServices_OrderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimatedDeliveryTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryCompletedTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAndServices_Orders_OrderId",
                table: "ProductAndServices",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
