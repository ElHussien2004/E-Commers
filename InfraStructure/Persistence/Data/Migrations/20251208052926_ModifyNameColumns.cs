using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyNameColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "orders",
                newName: "shipToAddress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_LastName",
                table: "orders",
                newName: "shipToAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "Address_FirstName",
                table: "orders",
                newName: "shipToAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "orders",
                newName: "shipToAddress_Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "orders",
                newName: "shipToAddress_City");

            migrationBuilder.RenameColumn(
                name: "UserAddress",
                table: "orders",
                newName: "BuyerEmail");

            migrationBuilder.RenameColumn(
                name: "OrderState",
                table: "orders",
                newName: "status");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_Street",
                table: "orders",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_LastName",
                table: "orders",
                newName: "Address_LastName");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_FirstName",
                table: "orders",
                newName: "Address_FirstName");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_Country",
                table: "orders",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "shipToAddress_City",
                table: "orders",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "orders",
                newName: "OrderState");

            migrationBuilder.RenameColumn(
                name: "BuyerEmail",
                table: "orders",
                newName: "UserAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");
        }
    }
}
