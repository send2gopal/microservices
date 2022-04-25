using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.catalog.Migrations
{
    public partial class Variantidauto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantvarivariant_id",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "varivariant_id",
                table: "ProductVariant",
                newName: "variant_id");

            migrationBuilder.RenameColumn(
                name: "ProductVariantvarivariant_id",
                table: "ProductImages",
                newName: "ProductVariantvariant_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductVariantvarivariant_id",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductVariantvariant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantvariant_id",
                table: "ProductImages",
                column: "ProductVariantvariant_id",
                principalTable: "ProductVariant",
                principalColumn: "variant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantvariant_id",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "variant_id",
                table: "ProductVariant",
                newName: "varivariant_id");

            migrationBuilder.RenameColumn(
                name: "ProductVariantvariant_id",
                table: "ProductImages",
                newName: "ProductVariantvarivariant_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductVariantvariant_id",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductVariantvarivariant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantvarivariant_id",
                table: "ProductImages",
                column: "ProductVariantvarivariant_id",
                principalTable: "ProductVariant",
                principalColumn: "varivariant_id");
        }
    }
}
