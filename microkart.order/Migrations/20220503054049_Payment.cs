using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microkart.order.Migrations
{
    public partial class Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentInformation_PaymentInformationId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentInformation",
                table: "PaymentInformation");

            migrationBuilder.RenameTable(
                name: "PaymentInformation",
                newName: "PaymentInformations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentInformations",
                table: "PaymentInformations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentInformations_PaymentInformationId",
                table: "Orders",
                column: "PaymentInformationId",
                principalTable: "PaymentInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentInformations_PaymentInformationId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentInformations",
                table: "PaymentInformations");

            migrationBuilder.RenameTable(
                name: "PaymentInformations",
                newName: "PaymentInformation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentInformation",
                table: "PaymentInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentInformation_PaymentInformationId",
                table: "Orders",
                column: "PaymentInformationId",
                principalTable: "PaymentInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
