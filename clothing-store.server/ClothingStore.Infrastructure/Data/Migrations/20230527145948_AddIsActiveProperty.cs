using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "clothing_store",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "clothing_store",
                table: "reviews",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "clothing_store",
                table: "products",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "clothing_store",
                table: "order_products",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "clothing_store",
                table: "customer_orders",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "clothing_store",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "clothing_store",
                table: "addresses",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "clothing_store",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "clothing_store",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "clothing_store",
                table: "order_products");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "clothing_store",
                table: "customer_orders");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "clothing_store",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "clothing_store",
                table: "addresses");
        }
    }
}
