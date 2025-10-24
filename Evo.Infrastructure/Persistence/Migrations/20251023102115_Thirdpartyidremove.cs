using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Thirdpartyidremove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyDrivers_ThirdPartyId_PhoneE164",
                table: "ThirdPartyDrivers");

            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyDrivers_ThirdPartyId_Status_IsAvailable",
                table: "ThirdPartyDrivers");

            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyDrivers_ThirdPartyId_WorkEmail",
                table: "ThirdPartyDrivers");

            migrationBuilder.DropColumn(
                name: "ThirdPartyId",
                table: "ThirdPartyDrivers");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_PhoneE164",
                table: "ThirdPartyDrivers",
                column: "PhoneE164",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_Status_IsAvailable",
                table: "ThirdPartyDrivers",
                columns: new[] { "Status", "IsAvailable" });

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_WorkEmail",
                table: "ThirdPartyDrivers",
                column: "WorkEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyDrivers_PhoneE164",
                table: "ThirdPartyDrivers");

            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyDrivers_Status_IsAvailable",
                table: "ThirdPartyDrivers");

            migrationBuilder.DropIndex(
                name: "IX_ThirdPartyDrivers_WorkEmail",
                table: "ThirdPartyDrivers");

            migrationBuilder.AddColumn<Guid>(
                name: "ThirdPartyId",
                table: "ThirdPartyDrivers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_ThirdPartyId_PhoneE164",
                table: "ThirdPartyDrivers",
                columns: new[] { "ThirdPartyId", "PhoneE164" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_ThirdPartyId_Status_IsAvailable",
                table: "ThirdPartyDrivers",
                columns: new[] { "ThirdPartyId", "Status", "IsAvailable" });

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_ThirdPartyId_WorkEmail",
                table: "ThirdPartyDrivers",
                columns: new[] { "ThirdPartyId", "WorkEmail" },
                unique: true);
        }
    }
}
