﻿//// <auto-generated />
//using System;
//using HandmadeMarket.Context;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//#nullable disable

//namespace HandmadeMarket.Migrations
//{
//    [DbContext(typeof(HandmadeContext))]
//    [Migration("20250416001232_IntialCreate")]
//    partial class IntialCreate
//    {
//        /// <inheritdoc />
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "8.0.15")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

//            modelBuilder.Entity("HandmadeMarket.Models.ApplicationUser", b =>
//                {
//                    b.Property<string>("Id")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<int>("AccessFailedCount")
//                        .HasColumnType("int");

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Email")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<bool>("EmailConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<bool>("LockoutEnabled")
//                        .HasColumnType("bit");

//                    b.Property<DateTimeOffset?>("LockoutEnd")
//                        .HasColumnType("datetimeoffset");

//                    b.Property<string>("NormalizedEmail")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("NormalizedUserName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("PasswordHash")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PhoneNumber")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("PhoneNumberConfirmed")
//                        .HasColumnType("bit");

//                    b.Property<string>("SecurityStamp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<bool>("TwoFactorEnabled")
//                        .HasColumnType("bit");

//                    b.Property<string>("UserName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.HasKey("Id");

//                    b.HasIndex("NormalizedEmail")
//                        .HasDatabaseName("EmailIndex");

//                    b.HasIndex("NormalizedUserName")
//                        .IsUnique()
//                        .HasDatabaseName("UserNameIndex")
//                        .HasFilter("[NormalizedUserName] IS NOT NULL");

//                    b.ToTable("AspNetUsers", (string)null);
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Cart", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<int>("CustomerId")
//                        .HasColumnType("int");

//                    b.Property<int>("ProductId")
//                        .HasColumnType("int");

//                    b.Property<int>("Quantity")
//                        .HasColumnType("int");

//                    b.HasKey("Id");

//                    b.HasIndex("CustomerId");

//                    b.HasIndex("ProductId");

//                    b.ToTable("Carts");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Category", b =>
//                {
//                    b.Property<int>("categoryId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("categoryId"));

//                    b.Property<string>("name")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("categoryId");

//                    b.ToTable("Categories");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Customer", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("Address")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("FirstName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("LastName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Password")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Phone")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Customers");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Order", b =>
//                {
//                    b.Property<int>("OrderId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

//                    b.Property<int>("CustomerId")
//                        .HasColumnType("int");

//                    b.Property<DateTime>("Order_Date")
//                        .HasColumnType("datetime2");

//                    b.Property<int>("ShipmentId")
//                        .HasColumnType("int");

//                    b.Property<decimal>("Total_Price")
//                        .HasColumnType("decimal(18,2)");

//                    b.HasKey("OrderId");

//                    b.HasIndex("CustomerId");

//                    b.HasIndex("ShipmentId");

//                    b.ToTable("Orders");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.OrderItem", b =>
//                {
//                    b.Property<int>("OrderItemId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

//                    b.Property<int>("OrderId")
//                        .HasColumnType("int");

//                    b.Property<decimal>("Price")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<int>("ProductId")
//                        .HasColumnType("int");

//                    b.Property<int>("Quantity")
//                        .HasColumnType("int");

//                    b.HasKey("OrderItemId");

//                    b.HasIndex("OrderId");

//                    b.HasIndex("ProductId");

//                    b.ToTable("Items");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Product", b =>
//                {
//                    b.Property<int>("ProductId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

//                    b.Property<string>("Description")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Image")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Name")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<decimal>("Price")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<string>("SKU")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("Stock")
//                        .HasColumnType("int");

//                    b.Property<int>("categoryId")
//                        .HasColumnType("int");

//                    b.Property<int>("sellerId")
//                        .HasColumnType("int");

//                    b.HasKey("ProductId");

//                    b.HasIndex("categoryId");

//                    b.HasIndex("sellerId");

//                    b.ToTable("Products");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Seller", b =>
//                {
//                    b.Property<int>("sellerId")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sellerId"));

//                    b.Property<DateTime>("createdAt")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("email")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("phoneNumber")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("storeName")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("sellerId");

//                    b.ToTable("Sellers");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Shipment", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("Address")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("City")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Country")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("CustomerId")
//                        .HasColumnType("int");

//                    b.Property<DateTime>("ShipmentDate")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("State")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ZipCode")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("CustomerId");

//                    b.ToTable("Shipments");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Wishlist", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<int>("CustomerId")
//                        .HasColumnType("int");

//                    b.Property<int>("ProductId")
//                        .HasColumnType("int");

//                    b.HasKey("Id");

//                    b.HasIndex("CustomerId");

//                    b.HasIndex("ProductId");

//                    b.ToTable("Wishlists");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
//                {
//                    b.Property<string>("Id")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ConcurrencyStamp")
//                        .IsConcurrencyToken()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Name")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.Property<string>("NormalizedName")
//                        .HasMaxLength(256)
//                        .HasColumnType("nvarchar(256)");

//                    b.HasKey("Id");

//                    b.HasIndex("NormalizedName")
//                        .IsUnique()
//                        .HasDatabaseName("RoleNameIndex")
//                        .HasFilter("[NormalizedName] IS NOT NULL");

//                    b.ToTable("AspNetRoles", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("RoleId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("Id");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetRoleClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("ClaimType")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("ClaimValue")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("Id");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserClaims", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ProviderKey")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("ProviderDisplayName")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("UserId")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("LoginProvider", "ProviderKey");

//                    b.HasIndex("UserId");

//                    b.ToTable("AspNetUserLogins", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.Property<string>("UserId")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("RoleId")
//                        .HasColumnType("nvarchar(450)");

//                    b.HasKey("UserId", "RoleId");

//                    b.HasIndex("RoleId");

//                    b.ToTable("AspNetUserRoles", (string)null);
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.Property<string>("UserId")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("LoginProvider")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("Name")
//                        .HasColumnType("nvarchar(450)");

//                    b.Property<string>("Value")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("UserId", "LoginProvider", "Name");

//                    b.ToTable("AspNetUserTokens", (string)null);
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Cart", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.Customer", "Customer")
//                        .WithMany("Carts")
//                        .HasForeignKey("CustomerId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("HandmadeMarket.Models.Product", "Product")
//                        .WithMany()
//                        .HasForeignKey("ProductId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Customer");

//                    b.Navigation("Product");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Order", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.Customer", "Customer")
//                        .WithMany("Orders")
//                        .HasForeignKey("CustomerId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("HandmadeMarket.Models.Shipment", "Shipment")
//                        .WithMany("Orders")
//                        .HasForeignKey("ShipmentId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Customer");

//                    b.Navigation("Shipment");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.OrderItem", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.Order", "Order")
//                        .WithMany("Order_Items")
//                        .HasForeignKey("OrderId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("HandmadeMarket.Models.Product", "Product")
//                        .WithMany()
//                        .HasForeignKey("ProductId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Order");

//                    b.Navigation("Product");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Product", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.Category", "Category")
//                        .WithMany("Products")
//                        .HasForeignKey("categoryId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("HandmadeMarket.Models.Seller", "Seller")
//                        .WithMany("Products")
//                        .HasForeignKey("sellerId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Category");

//                    b.Navigation("Seller");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Shipment", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.Customer", "Customer")
//                        .WithMany("Shipments")
//                        .HasForeignKey("CustomerId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Customer");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Wishlist", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.Customer", "Customer")
//                        .WithMany("Wishlist")
//                        .HasForeignKey("CustomerId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("HandmadeMarket.Models.Product", "Product")
//                        .WithMany()
//                        .HasForeignKey("ProductId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("Customer");

//                    b.Navigation("Product");
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.ApplicationUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.ApplicationUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
//                {
//                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
//                        .WithMany()
//                        .HasForeignKey("RoleId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("HandmadeMarket.Models.ApplicationUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
//                {
//                    b.HasOne("HandmadeMarket.Models.ApplicationUser", null)
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Category", b =>
//                {
//                    b.Navigation("Products");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Customer", b =>
//                {
//                    b.Navigation("Carts");

//                    b.Navigation("Orders");

//                    b.Navigation("Shipments");

//                    b.Navigation("Wishlist");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Order", b =>
//                {
//                    b.Navigation("Order_Items");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Seller", b =>
//                {
//                    b.Navigation("Products");
//                });

//            modelBuilder.Entity("HandmadeMarket.Models.Shipment", b =>
//                {
//                    b.Navigation("Orders");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
