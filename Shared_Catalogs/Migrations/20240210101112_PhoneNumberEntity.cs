using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_Catalogs.Migrations
{
    /// <inheritdoc />
    public partial class PhoneNumberEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPhoneNumbers_ContactInformation_ContactId",
                table: "CustomerPhoneNumbers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CustomerPhoneNumbers",
                newName: "ContactInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhoneNumbers_ContactInformationId",
                table: "CustomerPhoneNumbers",
                column: "ContactInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPhoneNumbers_ContactInformation_ContactInformationId",
                table: "CustomerPhoneNumbers",
                column: "ContactInformationId",
                principalTable: "ContactInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPhoneNumbers_ContactInformation_ContactInformationId",
                table: "CustomerPhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPhoneNumbers_ContactInformationId",
                table: "CustomerPhoneNumbers");

            migrationBuilder.RenameColumn(
                name: "ContactInformationId",
                table: "CustomerPhoneNumbers",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPhoneNumbers_ContactInformation_ContactId",
                table: "CustomerPhoneNumbers",
                column: "ContactId",
                principalTable: "ContactInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
