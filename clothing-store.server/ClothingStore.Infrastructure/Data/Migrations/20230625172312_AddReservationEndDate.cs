using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationEndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                schema: "clothing_store",
                table: "cart_items");

            migrationBuilder.AddColumn<DateTime>(
                name: "reservation_end_date",
                schema: "clothing_store",
                table: "cart_items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reservation_end_date",
                schema: "clothing_store",
                table: "cart_items");

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "clothing_store",
                table: "cart_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
