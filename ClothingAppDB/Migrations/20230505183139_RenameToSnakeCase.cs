using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingAppDB.Migrations
{
    /// <inheritdoc />
    public partial class RenameToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserID",
                schema: "clothing_store",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                schema: "clothing_store",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Users_UserID",
                schema: "clothing_store",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistories_CustomerOrders_OrderID",
                schema: "clothing_store",
                table: "OrderHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_CustomerOrders_OrderID",
                schema: "clothing_store",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                schema: "clothing_store",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandID",
                schema: "clothing_store",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID",
                schema: "clothing_store",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SectionCategories_SectionCategoryID",
                schema: "clothing_store",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductID",
                schema: "clothing_store",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserID",
                schema: "clothing_store",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionCategories_Categories_CategoryID",
                schema: "clothing_store",
                table: "SectionCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionCategories_Sections_SectionID",
                schema: "clothing_store",
                table: "SectionCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "clothing_store",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                schema: "clothing_store",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                schema: "clothing_store",
                table: "Reviews");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Review_Rating",
                schema: "clothing_store",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "clothing_store",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_Price",
                schema: "clothing_store",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_Quantity",
                schema: "clothing_store",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                schema: "clothing_store",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                schema: "clothing_store",
                table: "Brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "clothing_store",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionCategories",
                schema: "clothing_store",
                table: "SectionCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                schema: "clothing_store",
                table: "OrderProducts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderProduct_Price",
                schema: "clothing_store",
                table: "OrderProducts");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderProduct_Quantity",
                schema: "clothing_store",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHistories",
                schema: "clothing_store",
                table: "OrderHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerOrders",
                schema: "clothing_store",
                table: "CustomerOrders");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "clothing_store",
                newName: "users",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "Sections",
                schema: "clothing_store",
                newName: "sections",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "Reviews",
                schema: "clothing_store",
                newName: "reviews",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "clothing_store",
                newName: "products",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "clothing_store",
                newName: "categories",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "Brands",
                schema: "clothing_store",
                newName: "brands",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "clothing_store",
                newName: "addresses",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "SectionCategories",
                schema: "clothing_store",
                newName: "section_categories",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                schema: "clothing_store",
                newName: "order_products",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "OrderHistories",
                schema: "clothing_store",
                newName: "order_histories",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "CustomerOrders",
                schema: "clothing_store",
                newName: "customer_orders",
                newSchema: "clothing_store");

            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "clothing_store",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Phone",
                schema: "clothing_store",
                table: "users",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "clothing_store",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "clothing_store",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "clothing_store",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "clothing_store",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Phone",
                schema: "clothing_store",
                table: "users",
                newName: "ix_users_phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                schema: "clothing_store",
                table: "users",
                newName: "ix_users_email");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "clothing_store",
                table: "sections",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "sections",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_Name",
                schema: "clothing_store",
                table: "sections",
                newName: "ix_sections_name");

            migrationBuilder.RenameColumn(
                name: "Rating",
                schema: "clothing_store",
                table: "reviews",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Comment",
                schema: "clothing_store",
                table: "reviews",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "reviews",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "clothing_store",
                table: "reviews",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ReviewTitle",
                schema: "clothing_store",
                table: "reviews",
                newName: "review_title");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                schema: "clothing_store",
                table: "reviews",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "AddDate",
                schema: "clothing_store",
                table: "reviews",
                newName: "add_date");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserID",
                schema: "clothing_store",
                table: "reviews",
                newName: "ix_reviews_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductID",
                schema: "clothing_store",
                table: "reviews",
                newName: "ix_reviews_product_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "clothing_store",
                table: "products",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "clothing_store",
                table: "products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "clothing_store",
                table: "products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "clothing_store",
                table: "products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SectionCategoryID",
                schema: "clothing_store",
                table: "products",
                newName: "section_category_id");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                schema: "clothing_store",
                table: "products",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                schema: "clothing_store",
                table: "products",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                schema: "clothing_store",
                table: "products",
                newName: "brand_id");

            migrationBuilder.RenameColumn(
                name: "AddDate",
                schema: "clothing_store",
                table: "products",
                newName: "add_date");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SectionCategoryID",
                schema: "clothing_store",
                table: "products",
                newName: "ix_products_section_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryID",
                schema: "clothing_store",
                table: "products",
                newName: "ix_products_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandID",
                schema: "clothing_store",
                table: "products",
                newName: "ix_products_brand_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "clothing_store",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryID",
                schema: "clothing_store",
                table: "categories",
                newName: "parent_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryID",
                schema: "clothing_store",
                table: "categories",
                newName: "ix_categories_parent_category_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "clothing_store",
                table: "brands",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "brands",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_Name",
                schema: "clothing_store",
                table: "brands",
                newName: "ix_brands_name");

            migrationBuilder.RenameColumn(
                name: "Postcode",
                schema: "clothing_store",
                table: "addresses",
                newName: "postcode");

            migrationBuilder.RenameColumn(
                name: "District",
                schema: "clothing_store",
                table: "addresses",
                newName: "district");

            migrationBuilder.RenameColumn(
                name: "Country",
                schema: "clothing_store",
                table: "addresses",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "clothing_store",
                table: "addresses",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                schema: "clothing_store",
                table: "addresses",
                newName: "address_line2");

            migrationBuilder.RenameColumn(
                name: "AddressLine1",
                schema: "clothing_store",
                table: "addresses",
                newName: "address_line1");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "clothing_store",
                table: "addresses",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "section_categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SectionID",
                schema: "clothing_store",
                table: "section_categories",
                newName: "section_id");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                schema: "clothing_store",
                table: "section_categories",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_SectionCategories_SectionID",
                schema: "clothing_store",
                table: "section_categories",
                newName: "ix_section_categories_section_id");

            migrationBuilder.RenameIndex(
                name: "IX_SectionCategories_CategoryID",
                schema: "clothing_store",
                table: "section_categories",
                newName: "ix_section_categories_category_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "clothing_store",
                table: "order_products",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "clothing_store",
                table: "order_products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "order_products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                schema: "clothing_store",
                table: "order_products",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                schema: "clothing_store",
                table: "order_products",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductID",
                schema: "clothing_store",
                table: "order_products",
                newName: "ix_order_products_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderID",
                schema: "clothing_store",
                table: "order_products",
                newName: "ix_order_products_order_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "clothing_store",
                table: "order_histories",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "clothing_store",
                table: "order_histories",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "order_histories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                schema: "clothing_store",
                table: "order_histories",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHistories_OrderID",
                schema: "clothing_store",
                table: "order_histories",
                newName: "ix_order_histories_order_id");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "clothing_store",
                table: "customer_orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "clothing_store",
                table: "customer_orders",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                schema: "clothing_store",
                table: "customer_orders",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "CurrentStatus",
                schema: "clothing_store",
                table: "customer_orders",
                newName: "current_status");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerOrders_UserID",
                schema: "clothing_store",
                table: "customer_orders",
                newName: "ix_customer_orders_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                schema: "clothing_store",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sections",
                schema: "clothing_store",
                table: "sections",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_reviews",
                schema: "clothing_store",
                table: "reviews",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_products",
                schema: "clothing_store",
                table: "products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_categories",
                schema: "clothing_store",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_brands",
                schema: "clothing_store",
                table: "brands",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_addresses",
                schema: "clothing_store",
                table: "addresses",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_section_categories",
                schema: "clothing_store",
                table: "section_categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_products",
                schema: "clothing_store",
                table: "order_products",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_histories",
                schema: "clothing_store",
                table: "order_histories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_customer_orders",
                schema: "clothing_store",
                table: "customer_orders",
                column: "id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_review_rating",
                schema: "clothing_store",
                table: "reviews",
                sql: "rating >= 0 AND rating <= 5");

            migrationBuilder.AddCheckConstraint(
                name: "CK_product_price",
                schema: "clothing_store",
                table: "products",
                sql: "price > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_product_quantity",
                schema: "clothing_store",
                table: "products",
                sql: "Quantity >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_order_product_price",
                schema: "clothing_store",
                table: "order_products",
                sql: "price > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_order_product_quantity",
                schema: "clothing_store",
                table: "order_products",
                sql: "quantity > 0");

            migrationBuilder.AddForeignKey(
                name: "fk_addresses_users_user_id",
                schema: "clothing_store",
                table: "addresses",
                column: "user_id",
                principalSchema: "clothing_store",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_categories_categories_parent_category_id",
                schema: "clothing_store",
                table: "categories",
                column: "parent_category_id",
                principalSchema: "clothing_store",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_customer_orders_users_user_id",
                schema: "clothing_store",
                table: "customer_orders",
                column: "user_id",
                principalSchema: "clothing_store",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_order_histories_customer_orders_customer_order_id",
                schema: "clothing_store",
                table: "order_histories",
                column: "order_id",
                principalSchema: "clothing_store",
                principalTable: "customer_orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_products_customer_orders_order_id",
                schema: "clothing_store",
                table: "order_products",
                column: "order_id",
                principalSchema: "clothing_store",
                principalTable: "customer_orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_order_products_products_product_id",
                schema: "clothing_store",
                table: "order_products",
                column: "product_id",
                principalSchema: "clothing_store",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_products_brands_brand_id",
                schema: "clothing_store",
                table: "products",
                column: "brand_id",
                principalSchema: "clothing_store",
                principalTable: "brands",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                schema: "clothing_store",
                table: "products",
                column: "category_id",
                principalSchema: "clothing_store",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_section_categories_section_category_id",
                schema: "clothing_store",
                table: "products",
                column: "section_category_id",
                principalSchema: "clothing_store",
                principalTable: "section_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_products_product_id",
                schema: "clothing_store",
                table: "reviews",
                column: "product_id",
                principalSchema: "clothing_store",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_users_user_id",
                schema: "clothing_store",
                table: "reviews",
                column: "user_id",
                principalSchema: "clothing_store",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_section_categories_categories_category_id",
                schema: "clothing_store",
                table: "section_categories",
                column: "category_id",
                principalSchema: "clothing_store",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_section_categories_sections_section_id",
                schema: "clothing_store",
                table: "section_categories",
                column: "section_id",
                principalSchema: "clothing_store",
                principalTable: "sections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_addresses_users_user_id",
                schema: "clothing_store",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "fk_categories_categories_parent_category_id",
                schema: "clothing_store",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_customer_orders_users_user_id",
                schema: "clothing_store",
                table: "customer_orders");

            migrationBuilder.DropForeignKey(
                name: "fk_order_histories_customer_orders_customer_order_id",
                schema: "clothing_store",
                table: "order_histories");

            migrationBuilder.DropForeignKey(
                name: "fk_order_products_customer_orders_order_id",
                schema: "clothing_store",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "fk_order_products_products_product_id",
                schema: "clothing_store",
                table: "order_products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_brands_brand_id",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_products_section_categories_section_category_id",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_products_product_id",
                schema: "clothing_store",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_users_user_id",
                schema: "clothing_store",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_section_categories_categories_category_id",
                schema: "clothing_store",
                table: "section_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_section_categories_sections_section_id",
                schema: "clothing_store",
                table: "section_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                schema: "clothing_store",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sections",
                schema: "clothing_store",
                table: "sections");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reviews",
                schema: "clothing_store",
                table: "reviews");

            migrationBuilder.DropCheckConstraint(
                name: "CK_review_rating",
                schema: "clothing_store",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "pk_products",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_product_price",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_product_quantity",
                schema: "clothing_store",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "pk_categories",
                schema: "clothing_store",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_brands",
                schema: "clothing_store",
                table: "brands");

            migrationBuilder.DropPrimaryKey(
                name: "pk_addresses",
                schema: "clothing_store",
                table: "addresses");

            migrationBuilder.DropPrimaryKey(
                name: "pk_section_categories",
                schema: "clothing_store",
                table: "section_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_products",
                schema: "clothing_store",
                table: "order_products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_order_product_price",
                schema: "clothing_store",
                table: "order_products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_order_product_quantity",
                schema: "clothing_store",
                table: "order_products");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_histories",
                schema: "clothing_store",
                table: "order_histories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_customer_orders",
                schema: "clothing_store",
                table: "customer_orders");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "clothing_store",
                newName: "Users",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "sections",
                schema: "clothing_store",
                newName: "Sections",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "reviews",
                schema: "clothing_store",
                newName: "Reviews",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "products",
                schema: "clothing_store",
                newName: "Products",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "categories",
                schema: "clothing_store",
                newName: "Categories",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "brands",
                schema: "clothing_store",
                newName: "Brands",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "addresses",
                schema: "clothing_store",
                newName: "Addresses",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "section_categories",
                schema: "clothing_store",
                newName: "SectionCategories",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "order_products",
                schema: "clothing_store",
                newName: "OrderProducts",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "order_histories",
                schema: "clothing_store",
                newName: "OrderHistories",
                newSchema: "clothing_store");

            migrationBuilder.RenameTable(
                name: "customer_orders",
                schema: "clothing_store",
                newName: "CustomerOrders",
                newSchema: "clothing_store");

            migrationBuilder.RenameColumn(
                name: "role",
                schema: "clothing_store",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "phone",
                schema: "clothing_store",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "password",
                schema: "clothing_store",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "clothing_store",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "clothing_store",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                schema: "clothing_store",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameIndex(
                name: "ix_users_phone",
                schema: "clothing_store",
                table: "Users",
                newName: "IX_Users_Phone");

            migrationBuilder.RenameIndex(
                name: "ix_users_email",
                schema: "clothing_store",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "clothing_store",
                table: "Sections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "Sections",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "ix_sections_name",
                schema: "clothing_store",
                table: "Sections",
                newName: "IX_Sections_Name");

            migrationBuilder.RenameColumn(
                name: "rating",
                schema: "clothing_store",
                table: "Reviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "comment",
                schema: "clothing_store",
                table: "Reviews",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "Reviews",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "clothing_store",
                table: "Reviews",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "review_title",
                schema: "clothing_store",
                table: "Reviews",
                newName: "ReviewTitle");

            migrationBuilder.RenameColumn(
                name: "product_id",
                schema: "clothing_store",
                table: "Reviews",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "add_date",
                schema: "clothing_store",
                table: "Reviews",
                newName: "AddDate");

            migrationBuilder.RenameIndex(
                name: "ix_reviews_user_id",
                schema: "clothing_store",
                table: "Reviews",
                newName: "IX_Reviews_UserID");

            migrationBuilder.RenameIndex(
                name: "ix_reviews_product_id",
                schema: "clothing_store",
                table: "Reviews",
                newName: "IX_Reviews_ProductID");

            migrationBuilder.RenameColumn(
                name: "quantity",
                schema: "clothing_store",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                schema: "clothing_store",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "clothing_store",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "clothing_store",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "Products",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "section_category_id",
                schema: "clothing_store",
                table: "Products",
                newName: "SectionCategoryID");

            migrationBuilder.RenameColumn(
                name: "image_url",
                schema: "clothing_store",
                table: "Products",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "category_id",
                schema: "clothing_store",
                table: "Products",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "brand_id",
                schema: "clothing_store",
                table: "Products",
                newName: "BrandID");

            migrationBuilder.RenameColumn(
                name: "add_date",
                schema: "clothing_store",
                table: "Products",
                newName: "AddDate");

            migrationBuilder.RenameIndex(
                name: "ix_products_section_category_id",
                schema: "clothing_store",
                table: "Products",
                newName: "IX_Products_SectionCategoryID");

            migrationBuilder.RenameIndex(
                name: "ix_products_category_id",
                schema: "clothing_store",
                table: "Products",
                newName: "IX_Products_CategoryID");

            migrationBuilder.RenameIndex(
                name: "ix_products_brand_id",
                schema: "clothing_store",
                table: "Products",
                newName: "IX_Products_BrandID");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "clothing_store",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "parent_category_id",
                schema: "clothing_store",
                table: "Categories",
                newName: "ParentCategoryID");

            migrationBuilder.RenameIndex(
                name: "ix_categories_parent_category_id",
                schema: "clothing_store",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryID");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "clothing_store",
                table: "Brands",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "Brands",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "ix_brands_name",
                schema: "clothing_store",
                table: "Brands",
                newName: "IX_Brands_Name");

            migrationBuilder.RenameColumn(
                name: "postcode",
                schema: "clothing_store",
                table: "Addresses",
                newName: "Postcode");

            migrationBuilder.RenameColumn(
                name: "district",
                schema: "clothing_store",
                table: "Addresses",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "country",
                schema: "clothing_store",
                table: "Addresses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                schema: "clothing_store",
                table: "Addresses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address_line2",
                schema: "clothing_store",
                table: "Addresses",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "address_line1",
                schema: "clothing_store",
                table: "Addresses",
                newName: "AddressLine1");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "clothing_store",
                table: "Addresses",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "SectionCategories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "section_id",
                schema: "clothing_store",
                table: "SectionCategories",
                newName: "SectionID");

            migrationBuilder.RenameColumn(
                name: "category_id",
                schema: "clothing_store",
                table: "SectionCategories",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "ix_section_categories_section_id",
                schema: "clothing_store",
                table: "SectionCategories",
                newName: "IX_SectionCategories_SectionID");

            migrationBuilder.RenameIndex(
                name: "ix_section_categories_category_id",
                schema: "clothing_store",
                table: "SectionCategories",
                newName: "IX_SectionCategories_CategoryID");

            migrationBuilder.RenameColumn(
                name: "quantity",
                schema: "clothing_store",
                table: "OrderProducts",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                schema: "clothing_store",
                table: "OrderProducts",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "OrderProducts",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "product_id",
                schema: "clothing_store",
                table: "OrderProducts",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "order_id",
                schema: "clothing_store",
                table: "OrderProducts",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "ix_order_products_product_id",
                schema: "clothing_store",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductID");

            migrationBuilder.RenameIndex(
                name: "ix_order_products_order_id",
                schema: "clothing_store",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderID");

            migrationBuilder.RenameColumn(
                name: "status",
                schema: "clothing_store",
                table: "OrderHistories",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "date",
                schema: "clothing_store",
                table: "OrderHistories",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "OrderHistories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "order_id",
                schema: "clothing_store",
                table: "OrderHistories",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "ix_order_histories_order_id",
                schema: "clothing_store",
                table: "OrderHistories",
                newName: "IX_OrderHistories_OrderID");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "clothing_store",
                table: "CustomerOrders",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "clothing_store",
                table: "CustomerOrders",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "order_date",
                schema: "clothing_store",
                table: "CustomerOrders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "current_status",
                schema: "clothing_store",
                table: "CustomerOrders",
                newName: "CurrentStatus");

            migrationBuilder.RenameIndex(
                name: "ix_customer_orders_user_id",
                schema: "clothing_store",
                table: "CustomerOrders",
                newName: "IX_CustomerOrders_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "clothing_store",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                schema: "clothing_store",
                table: "Sections",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                schema: "clothing_store",
                table: "Reviews",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "clothing_store",
                table: "Products",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                schema: "clothing_store",
                table: "Categories",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                schema: "clothing_store",
                table: "Brands",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "clothing_store",
                table: "Addresses",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionCategories",
                schema: "clothing_store",
                table: "SectionCategories",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                schema: "clothing_store",
                table: "OrderProducts",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHistories",
                schema: "clothing_store",
                table: "OrderHistories",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerOrders",
                schema: "clothing_store",
                table: "CustomerOrders",
                column: "ID");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Review_Rating",
                schema: "clothing_store",
                table: "Reviews",
                sql: "\"Rating\" >= 0 AND \"Rating\" <= 5");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_Price",
                schema: "clothing_store",
                table: "Products",
                sql: "\"Price\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_Quantity",
                schema: "clothing_store",
                table: "Products",
                sql: "\"Quantity\" >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderProduct_Price",
                schema: "clothing_store",
                table: "OrderProducts",
                sql: "\"Price\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderProduct_Quantity",
                schema: "clothing_store",
                table: "OrderProducts",
                sql: "\"Quantity\" > 0");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserID",
                schema: "clothing_store",
                table: "Addresses",
                column: "UserID",
                principalSchema: "clothing_store",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryID",
                schema: "clothing_store",
                table: "Categories",
                column: "ParentCategoryID",
                principalSchema: "clothing_store",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Users_UserID",
                schema: "clothing_store",
                table: "CustomerOrders",
                column: "UserID",
                principalSchema: "clothing_store",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistories_CustomerOrders_OrderID",
                schema: "clothing_store",
                table: "OrderHistories",
                column: "OrderID",
                principalSchema: "clothing_store",
                principalTable: "CustomerOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_CustomerOrders_OrderID",
                schema: "clothing_store",
                table: "OrderProducts",
                column: "OrderID",
                principalSchema: "clothing_store",
                principalTable: "CustomerOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductID",
                schema: "clothing_store",
                table: "OrderProducts",
                column: "ProductID",
                principalSchema: "clothing_store",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandID",
                schema: "clothing_store",
                table: "Products",
                column: "BrandID",
                principalSchema: "clothing_store",
                principalTable: "Brands",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID",
                schema: "clothing_store",
                table: "Products",
                column: "CategoryID",
                principalSchema: "clothing_store",
                principalTable: "Categories",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SectionCategories_SectionCategoryID",
                schema: "clothing_store",
                table: "Products",
                column: "SectionCategoryID",
                principalSchema: "clothing_store",
                principalTable: "SectionCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductID",
                schema: "clothing_store",
                table: "Reviews",
                column: "ProductID",
                principalSchema: "clothing_store",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserID",
                schema: "clothing_store",
                table: "Reviews",
                column: "UserID",
                principalSchema: "clothing_store",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionCategories_Categories_CategoryID",
                schema: "clothing_store",
                table: "SectionCategories",
                column: "CategoryID",
                principalSchema: "clothing_store",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionCategories_Sections_SectionID",
                schema: "clothing_store",
                table: "SectionCategories",
                column: "SectionID",
                principalSchema: "clothing_store",
                principalTable: "Sections",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
