using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.catalog.Migrations
{
    public partial class productUpdate10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_ProductImages_productImagesId",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariant_productImagesId",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "productImagesId",
                table: "ProductVariant");

            migrationBuilder.AddColumn<int>(
                name: "ProductVariantId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductVariantId",
                table: "ProductImages",
                column: "ProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantId",
                table: "ProductImages",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductVariantId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "productImagesId",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_productImagesId",
                table: "ProductVariant",
                column: "productImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_ProductImages_productImagesId",
                table: "ProductVariant",
                column: "productImagesId",
                principalTable: "ProductImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
