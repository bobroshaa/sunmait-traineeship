﻿// <auto-generated />
using System;
using ClothingStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClothingStore.Infrastructure.Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230603053346_RemoveBrandUniqueIndex")]
    partial class RemoveBrandUniqueIndex
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("clothing_store")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClothingStore.Domain.Entities.Address", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("address_line1");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("address_line2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<int>("Country")
                        .HasColumnType("integer")
                        .HasColumnName("country");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("district");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("postcode");

                    b.HasKey("UserID")
                        .HasName("pk_addresses");

                    b.ToTable("addresses", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_brands");

                    b.ToTable("brands", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<int?>("ParentCategoryID")
                        .HasColumnType("integer")
                        .HasColumnName("parent_category_id");

                    b.HasKey("ID")
                        .HasName("pk_categories");

                    b.HasIndex("ParentCategoryID")
                        .HasDatabaseName("ix_categories_parent_category_id");

                    b.ToTable("categories", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.CustomerOrder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CurrentStatus")
                        .HasColumnType("integer")
                        .HasColumnName("current_status");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("order_date");

                    b.Property<int>("UserID")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ID")
                        .HasName("pk_customer_orders");

                    b.HasIndex("UserID")
                        .HasDatabaseName("ix_customer_orders_user_id");

                    b.ToTable("customer_orders", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.OrderHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("OrderID")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("ID")
                        .HasName("pk_order_histories");

                    b.HasIndex("OrderID")
                        .HasDatabaseName("ix_order_histories_order_id");

                    b.ToTable("order_histories", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.OrderProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("OrderID")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<double>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("ProductID")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.HasKey("ID")
                        .HasName("pk_order_products");

                    b.HasIndex("OrderID")
                        .HasDatabaseName("ix_order_products_order_id");

                    b.HasIndex("ProductID")
                        .HasDatabaseName("ix_order_products_product_id");

                    b.ToTable("order_products", "clothing_store", t =>
                        {
                            t.HasCheckConstraint("price", "price > 0")
                                .HasName("CK_order_product_price");

                            t.HasCheckConstraint("quantity", "quantity > 0")
                                .HasName("CK_order_product_quantity");
                        });
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("add_date");

                    b.Property<int?>("BrandID")
                        .HasColumnType("integer")
                        .HasColumnName("brand_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("image_url");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<int>("SectionCategoryID")
                        .HasMaxLength(100)
                        .HasColumnType("integer")
                        .HasColumnName("section_category_id");

                    b.HasKey("ID")
                        .HasName("pk_products");

                    b.HasIndex("BrandID")
                        .HasDatabaseName("ix_products_brand_id");

                    b.HasIndex("SectionCategoryID")
                        .HasDatabaseName("ix_products_section_category_id");

                    b.ToTable("products", "clothing_store", t =>
                        {
                            t.HasCheckConstraint("price", "price > 0")
                                .HasName("CK_product_price");

                            t.HasCheckConstraint("quantity", "Quantity >= 0")
                                .HasName("CK_product_quantity");
                        });
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("add_date");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("comment");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<int>("ProductID")
                        .HasColumnType("integer")
                        .HasColumnName("product_id");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<string>("ReviewTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("review_title");

                    b.Property<int>("UserID")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("ID")
                        .HasName("pk_reviews");

                    b.HasIndex("ProductID")
                        .HasDatabaseName("ix_reviews_product_id");

                    b.HasIndex("UserID")
                        .HasDatabaseName("ix_reviews_user_id");

                    b.ToTable("reviews", "clothing_store", t =>
                        {
                            t.HasCheckConstraint("rating", "rating >= 0 AND rating <= 5")
                                .HasName("CK_review_rating");
                        });
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Section", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("ID")
                        .HasName("pk_sections");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_sections_name");

                    b.ToTable("sections", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.SectionCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<int>("SectionID")
                        .HasColumnType("integer")
                        .HasColumnName("section_id");

                    b.HasKey("ID")
                        .HasName("pk_section_categories");

                    b.HasIndex("CategoryID")
                        .HasDatabaseName("ix_section_categories_category_id");

                    b.HasIndex("SectionID")
                        .HasDatabaseName("ix_section_categories_section_id");

                    b.ToTable("section_categories", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.UserAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.HasKey("ID")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("Phone")
                        .IsUnique()
                        .HasDatabaseName("ix_users_phone");

                    b.ToTable("users", "clothing_store");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Address", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.UserAccount", "User")
                        .WithOne("Address")
                        .HasForeignKey("ClothingStore.Domain.Entities.Address", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_addresses_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Category", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.Category", "ParentCategory")
                        .WithMany("Categories")
                        .HasForeignKey("ParentCategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_categories_categories_parent_category_id");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.CustomerOrder", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.UserAccount", "User")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_customer_orders_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.OrderHistory", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.CustomerOrder", "CustomerOrder")
                        .WithMany("OrderHistories")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_histories_customer_orders_customer_order_id");

                    b.Navigation("CustomerOrder");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.CustomerOrder", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_order_products_customer_orders_order_id");

                    b.HasOne("ClothingStore.Domain.Entities.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_order_products_products_product_id");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Product", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_products_brands_brand_id");

                    b.HasOne("ClothingStore.Domain.Entities.SectionCategory", "SectionCategory")
                        .WithMany("Products")
                        .HasForeignKey("SectionCategoryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_products_section_categories_section_category_id");

                    b.Navigation("Brand");

                    b.Navigation("SectionCategory");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Review", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_products_product_id");

                    b.HasOne("ClothingStore.Domain.Entities.UserAccount", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_reviews_users_user_id");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.SectionCategory", b =>
                {
                    b.HasOne("ClothingStore.Domain.Entities.Category", "Category")
                        .WithMany("SectionCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_section_categories_categories_category_id");

                    b.HasOne("ClothingStore.Domain.Entities.Section", "Section")
                        .WithMany("SectionCategories")
                        .HasForeignKey("SectionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_section_categories_sections_section_id");

                    b.Navigation("Category");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Category", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("SectionCategories");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.CustomerOrder", b =>
                {
                    b.Navigation("OrderHistories");

                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Product", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.Section", b =>
                {
                    b.Navigation("SectionCategories");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.SectionCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ClothingStore.Domain.Entities.UserAccount", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("CustomerOrders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
