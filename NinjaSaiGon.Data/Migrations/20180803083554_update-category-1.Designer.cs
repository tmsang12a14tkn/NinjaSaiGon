﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NinjaSaiGon.Data;

namespace NinjaSaiGon.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180803083554_update-category-1")]
    partial class updatecategory1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.CategoryIceOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkCategoryId");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DrinkCategoryId");

                    b.ToTable("CategoryIceOption");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.CategorySugarOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkCategoryId");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DrinkCategoryId");

                    b.ToTable("CategorySugarOption");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("EnglishName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("DrinkCategories");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkCategoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("DrinkCategoryTypes");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkId");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("PhysicalPath");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("DrinkId");

                    b.ToTable("DrinkPhotos");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkId");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Name");

                    b.Property<int>("Prize");

                    b.HasKey("Id");

                    b.HasIndex("DrinkId");

                    b.ToTable("DrinkSize");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkTopping", b =>
                {
                    b.Property<int>("DrinkId");

                    b.Property<int>("ToppingId");

                    b.HasKey("DrinkId", "ToppingId");

                    b.HasIndex("ToppingId");

                    b.ToTable("DrinkToppings");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.IceOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkId");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DrinkId");

                    b.ToTable("IceOption");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("OrderPlaced");

                    b.Property<decimal>("OrderTotal");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int>("DrinkId");

                    b.Property<int>("OrderId");

                    b.Property<decimal>("Price");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("DrinkId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.OrderDetailTopping", b =>
                {
                    b.Property<int>("OrderDetailId");

                    b.Property<int>("ToppingId");

                    b.HasKey("OrderDetailId", "ToppingId");

                    b.HasIndex("ToppingId");

                    b.ToTable("OrderDetailToppings");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int?>("DrinkId");

                    b.Property<string>("ShoppingCartId");

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("DrinkId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.SugarOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrinkId");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsPrimary");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DrinkId");

                    b.ToTable("SugarOption");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.Topping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<string>("EnglishName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Toppings");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.ToppingCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("EnglishName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ToppingCategories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NinjaSaiGon.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.CategoryIceOption", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.DrinkCategory", "DrinkCategory")
                        .WithMany("CategoryIceOptions")
                        .HasForeignKey("DrinkCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.CategorySugarOption", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.DrinkCategory", "DrinkCategory")
                        .WithMany("CategorySugarOptions")
                        .HasForeignKey("DrinkCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.Drink", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.DrinkCategory", "Category")
                        .WithMany("Drinks")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkCategory", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.DrinkCategory", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkPhoto", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.Drink", "Drink")
                        .WithMany("Photos")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkSize", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.Drink", "Drink")
                        .WithMany("DrinkSizes")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.DrinkTopping", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.Drink", "Drink")
                        .WithMany("DrinkToppings")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NinjaSaiGon.Data.Models.Topping", "Topping")
                        .WithMany("DrinkToppings")
                        .HasForeignKey("ToppingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.IceOption", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.Drink", "Drink")
                        .WithMany("IceOptions")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.OrderDetail", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.Drink", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NinjaSaiGon.Data.Models.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.OrderDetailTopping", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.OrderDetail", "OrderDetail")
                        .WithMany("OrderDetailToppings")
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NinjaSaiGon.Data.Models.Topping", "Topping")
                        .WithMany("OrderDetailToppings")
                        .HasForeignKey("ToppingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.Drink", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId");
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.SugarOption", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.Drink", "Drink")
                        .WithMany("SugarOptions")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.Topping", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.ToppingCategory", "Category")
                        .WithMany("Toppings")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NinjaSaiGon.Data.Models.ToppingCategory", b =>
                {
                    b.HasOne("NinjaSaiGon.Data.Models.ToppingCategory", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
