using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReservedQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_product_quantity",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "quantity",
                schema: "clothing_store",
                table: "products",
                newName: "in_stock_quantity");

            migrationBuilder.AddColumn<int>(
                name: "reserved_quantity",
                schema: "clothing_store",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_product_in_stock_quantity",
                schema: "clothing_store",
                table: "products",
                sql: "in_stock_quantity >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_product_reserved_quantity",
                schema: "clothing_store",
                table: "products",
                sql: "reserved_quantity >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_product_in_stock_quantity",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_product_reserved_quantity",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropColumn(
                name: "reserved_quantity",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "in_stock_quantity",
                schema: "clothing_store",
                table: "products",
                newName: "quantity");

            migrationBuilder.AddCheckConstraint(
                name: "CK_product_quantity",
                schema: "clothing_store",
                table: "products",
                sql: "Quantity >= 0");
        }
    }
}
