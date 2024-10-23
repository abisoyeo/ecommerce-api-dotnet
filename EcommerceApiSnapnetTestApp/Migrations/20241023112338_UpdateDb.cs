using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApiSnapnetTestApp.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerOrder",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrderId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrdersId",
                table: "Products",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerOrderId",
                table: "Customers",
                column: "CustomerOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Orders_CustomerOrderId",
                table: "Customers",
                column: "CustomerOrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrdersId",
                table: "Products",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Orders_CustomerOrderId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrdersId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrdersId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerOrderId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerOrderId",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "CustomerOrder",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
