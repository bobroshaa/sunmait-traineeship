using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "clothing_store");

            migrationBuilder.CreateTable(
                name: "brands",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    parent_category_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parent_category_id",
                        column: x => x.parent_category_id,
                        principalSchema: "clothing_store",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "section_categories",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    section_id = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_section_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_section_categories_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "clothing_store",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_section_categories_sections_section_id",
                        column: x => x.section_id,
                        principalSchema: "clothing_store",
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "clothing_store",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<int>(type: "integer", nullable: false),
                    district = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    postcode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    address_line1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_line2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_addresses_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "clothing_store",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_orders",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    current_status = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_orders_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "clothing_store",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    price = table.Column<double>(type: "numeric", nullable: false),
                    add_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    section_category_id = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    brand_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.CheckConstraint("CK_product_price", "price > 0");
                    table.CheckConstraint("CK_product_quantity", "Quantity >= 0");
                    table.ForeignKey(
                        name: "fk_products_brands_brand_id",
                        column: x => x.brand_id,
                        principalSchema: "clothing_store",
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_products_section_categories_section_category_id",
                        column: x => x.section_category_id,
                        principalSchema: "clothing_store",
                        principalTable: "section_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_histories",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_histories", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_histories_customer_orders_customer_order_id",
                        column: x => x.order_id,
                        principalSchema: "clothing_store",
                        principalTable: "customer_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_products",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "numeric", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_products", x => x.id);
                    table.CheckConstraint("CK_order_product_price", "price > 0");
                    table.CheckConstraint("CK_order_product_quantity", "quantity > 0");
                    table.ForeignKey(
                        name: "fk_order_products_customer_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "clothing_store",
                        principalTable: "customer_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_products_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "clothing_store",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                schema: "clothing_store",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    review_title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    add_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.CheckConstraint("CK_review_rating", "rating >= 0 AND rating <= 5");
                    table.ForeignKey(
                        name: "fk_reviews_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "clothing_store",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "clothing_store",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_brands_name",
                schema: "clothing_store",
                table: "brands",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_categories_parent_category_id",
                schema: "clothing_store",
                table: "categories",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_customer_orders_user_id",
                schema: "clothing_store",
                table: "customer_orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_histories_order_id",
                schema: "clothing_store",
                table: "order_histories",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_products_order_id",
                schema: "clothing_store",
                table: "order_products",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_products_product_id",
                schema: "clothing_store",
                table: "order_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_brand_id",
                schema: "clothing_store",
                table: "products",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_section_category_id",
                schema: "clothing_store",
                table: "products",
                column: "section_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_product_id",
                schema: "clothing_store",
                table: "reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                schema: "clothing_store",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_section_categories_category_id",
                schema: "clothing_store",
                table: "section_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_section_categories_section_id",
                schema: "clothing_store",
                table: "section_categories",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_sections_name",
                schema: "clothing_store",
                table: "sections",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "clothing_store",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_phone",
                schema: "clothing_store",
                table: "users",
                column: "phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "order_histories",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "order_products",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "reviews",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "customer_orders",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "products",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "users",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "brands",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "section_categories",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "clothing_store");

            migrationBuilder.DropTable(
                name: "sections",
                schema: "clothing_store");
        }
    }
}
