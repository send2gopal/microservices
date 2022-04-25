using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.catalog.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_productId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "image_id",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ProductImages",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "src",
                table: "ProductImages",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "alt",
                table: "ProductImages",
                newName: "AltText");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_productId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductImages",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "ProductImages",
                newName: "src");

            migrationBuilder.RenameColumn(
                name: "AltText",
                table: "ProductImages",
                newName: "alt");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_productId");

            migrationBuilder.AddColumn<int>(
                name: "image_id",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_productId",
                table: "ProductImages",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
