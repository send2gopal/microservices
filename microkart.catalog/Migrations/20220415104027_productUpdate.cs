using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.catalog.Migrations
{
    public partial class productUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCatagories_ProductCatagoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Products",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "QuantityInStock",
                table: "Products",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "ProductCatagoryId",
                table: "Products",
                newName: "imagesId");

            migrationBuilder.RenameColumn(
                name: "Pitchures",
                table: "Products",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Products",
                newName: "sale");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCatagoryId",
                table: "Products",
                newName: "IX_Products_imagesId");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "discount",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "collection",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "tags",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image_id = table.Column<int>(type: "int", nullable: false),
                    alt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    src = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CraetedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCatagories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "ProductCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImage_imagesId",
                table: "Products",
                column: "imagesId",
                principalTable: "ProductImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCatagories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImage_imagesId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "collection",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "tags",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "Products",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Products",
                newName: "Pitchures");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "Products",
                newName: "QuantityInStock");

            migrationBuilder.RenameColumn(
                name: "sale",
                table: "Products",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "imagesId",
                table: "Products",
                newName: "ProductCatagoryId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Products_imagesId",
                table: "Products",
                newName: "IX_Products_ProductCatagoryId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Discount",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCatagories_ProductCatagoryId",
                table: "Products",
                column: "ProductCatagoryId",
                principalTable: "ProductCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
