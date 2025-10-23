using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Thirdparty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThirdPartyDrivers",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThirdPartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneE164 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NIC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LicenseExpiryUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehicleType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    VehiclePlateNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyDrivers", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_ThirdPartyDrivers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_LicenseNumber",
                table: "ThirdPartyDrivers",
                column: "LicenseNumber");

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

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_UserId",
                table: "ThirdPartyDrivers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyDrivers_VehiclePlateNo",
                table: "ThirdPartyDrivers",
                column: "VehiclePlateNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThirdPartyDrivers");
        }
    }
}
