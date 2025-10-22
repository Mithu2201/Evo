using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdminStaffCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.CheckConstraint("CK_Admins_Phone_Length", "LEN([Phone]) BETWEEN 7 AND 15");
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Position = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId1 = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.CheckConstraint("CK_Staff_Phone_Length", "LEN([Phone]) BETWEEN 7 AND 20");
                    table.ForeignKey(
                        name: "FK_Staff_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staff_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId1",
                table: "Admins",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UX_Admins_Email",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId1",
                table: "Staff",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UX_Staff_Email",
                table: "Staff",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
