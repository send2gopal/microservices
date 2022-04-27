using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.catalog.Migrations
{
    public partial class productUpdate8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_productId",
                table: "ProductVariant",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Products_productId",
                table: "ProductVariant",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Products_productId",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariant_productId",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "ProductVariant");
        }
    }
}
