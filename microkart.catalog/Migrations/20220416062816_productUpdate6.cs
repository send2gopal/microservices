using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.catalog.Migrations
{
    public partial class productUpdate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImages_imagesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_imagesId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "imagesId",
                table: "Products",
                newName: "product_Id");

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_productId",
                table: "ProductImages",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_productId",
                table: "ProductImages",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_productId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_productId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "product_Id",
                table: "Products",
                newName: "imagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_imagesId",
                table: "Products",
                column: "imagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImages_imagesId",
                table: "Products",
                column: "imagesId",
                principalTable: "ProductImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
