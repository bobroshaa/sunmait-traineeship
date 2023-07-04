using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBrandUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_brands_name",
                schema: "clothing_store",
                table: "brands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_brands_name",
                schema: "clothing_store",
                table: "brands",
                column: "name",
                unique: true);
        }
    }
}
