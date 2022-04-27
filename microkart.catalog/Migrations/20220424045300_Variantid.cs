using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.catalog.Migrations
{
    public partial class Variantid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantId",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductVariantId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "CraetedDate",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "variant_id",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "varivariant_id",
                table: "ProductVariant",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductVariantvarivariant_id",
                table: "ProductImages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "varivariant_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductVariantvarivariant_id",
                table: "ProductImages",
                column: "ProductVariantvarivariant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantvarivariant_id",
                table: "ProductImages",
                column: "ProductVariantvarivariant_id",
                principalTable: "ProductVariant",
                principalColumn: "varivariant_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_ProductVariant_ProductVariantvarivariant_id",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductVariantvarivariant_id",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "varivariant_id",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "ProductVariantvarivariant_id",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CraetedDate",
                table: "ProductVariant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductVariant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "variant_id",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductVariantId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "Id");

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
    }
}
