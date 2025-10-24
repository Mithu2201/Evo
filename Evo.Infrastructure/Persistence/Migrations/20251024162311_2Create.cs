using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "ServiceProviderId",
                table: "ServiceProviders");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ServiceProviders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ServiceProviders");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceProviderId",
                table: "ServiceProviders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProviders",
                table: "ServiceProviders",
                column: "ServiceProviderId");
        }
    }
}
