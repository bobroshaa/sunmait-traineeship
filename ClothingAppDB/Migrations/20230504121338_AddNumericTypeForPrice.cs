using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingAppDB.Migrations
{
    /// <inheritdoc />
    public partial class AddNumericTypeForPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "clothing_store",
                table: "Products",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "clothing_store",
                table: "OrderProducts",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "clothing_store",
                table: "Products",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "clothing_store",
                table: "OrderProducts",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric");
        }
    }
}
