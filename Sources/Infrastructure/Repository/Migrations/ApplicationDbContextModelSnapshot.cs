﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Accessory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accessories");
                });

            modelBuilder.Entity("Domain.Models.Firing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalKwH")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Firings");
                });

            modelBuilder.Entity("Domain.Models.ImageInstruction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FileLocation")
                        .HasColumnType("integer");

                    b.Property<int>("IdProduct")
                        .HasColumnType("integer");

                    b.Property<string>("MediumUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ThumbUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdProduct");

                    b.ToTable("ImageInstruction");
                });

            modelBuilder.Entity("Domain.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<double?>("Cost")
                        .HasColumnType("double precision");

                    b.Property<bool>("IsCommercial")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Domain.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double?>("BottomDiameter")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DesignInstruction")
                        .HasColumnType("text");

                    b.Property<double?>("Height")
                        .HasColumnType("double precision");

                    b.Property<int>("MarginRate")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<double?>("TopDiameter")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Models.ProductAccessory", b =>
                {
                    b.Property<int>("IdProduct")
                        .HasColumnType("integer");

                    b.Property<int>("IdAccessory")
                        .HasColumnType("integer");

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.HasKey("IdProduct", "IdAccessory");

                    b.HasIndex("IdAccessory");

                    b.ToTable("ProductAccessories");
                });

            modelBuilder.Entity("Domain.Models.ProductFiring", b =>
                {
                    b.Property<int>("IdProduct")
                        .HasColumnType("integer");

                    b.Property<int>("IdFiring")
                        .HasColumnType("integer");

                    b.Property<int>("CostKwH")
                        .HasColumnType("integer");

                    b.Property<int>("TotalKwH")
                        .HasColumnType("integer");

                    b.HasKey("IdProduct", "IdFiring");

                    b.HasIndex("IdFiring");

                    b.ToTable("ProductFirings");
                });

            modelBuilder.Entity("Domain.Models.ProductMaterial", b =>
                {
                    b.Property<int>("IdProduct")
                        .HasColumnType("integer");

                    b.Property<int>("IdMaterial")
                        .HasColumnType("integer");

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.HasKey("IdProduct", "IdMaterial");

                    b.HasIndex("IdMaterial");

                    b.ToTable("ProductMaterials");
                });

            modelBuilder.Entity("Domain.Models.ImageInstruction", b =>
                {
                    b.HasOne("Domain.Models.Product", "ProductAssociate")
                        .WithMany("ProductImageInstruction")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductAssociate");
                });

            modelBuilder.Entity("Domain.Models.ProductAccessory", b =>
                {
                    b.HasOne("Domain.Models.Accessory", "Accessory")
                        .WithMany("ProductAccessory")
                        .HasForeignKey("IdAccessory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Product", "Product")
                        .WithMany("ProductAccessory")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accessory");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.ProductFiring", b =>
                {
                    b.HasOne("Domain.Models.Firing", "Firing")
                        .WithMany("ProductFiring")
                        .HasForeignKey("IdFiring")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Product", "Product")
                        .WithMany("ProductFiring")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Firing");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.ProductMaterial", b =>
                {
                    b.HasOne("Domain.Models.Material", "Material")
                        .WithMany("ProductMaterial")
                        .HasForeignKey("IdMaterial")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Product", "Product")
                        .WithMany("ProductMaterial")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.Accessory", b =>
                {
                    b.Navigation("ProductAccessory");
                });

            modelBuilder.Entity("Domain.Models.Firing", b =>
                {
                    b.Navigation("ProductFiring");
                });

            modelBuilder.Entity("Domain.Models.Material", b =>
                {
                    b.Navigation("ProductMaterial");
                });

            modelBuilder.Entity("Domain.Models.Product", b =>
                {
                    b.Navigation("ProductAccessory");

                    b.Navigation("ProductFiring");

                    b.Navigation("ProductImageInstruction");

                    b.Navigation("ProductMaterial");
                });
#pragma warning restore 612, 618
        }
    }
}
