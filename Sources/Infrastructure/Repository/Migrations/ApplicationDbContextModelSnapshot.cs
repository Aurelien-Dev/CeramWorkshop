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
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.MainDomain.Firing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CostKwH")
                        .HasColumnType("double precision");

                    b.Property<double>("Duration")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TotalKwH")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Firings");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.ImageInstruction", b =>
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

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdProduct");

                    b.ToTable("ImageInstruction");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<bool?>("IsHomeMade")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UniteMesure")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.Product", b =>
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

                    b.Property<string>("GlazingInstruction")
                        .HasColumnType("text");

                    b.Property<double?>("Height")
                        .HasColumnType("double precision");

                    b.Property<int>("IdWorkshop")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("ShrinkingCoeficient")
                        .HasColumnType("double precision");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<double?>("TopDiameter")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("IdWorkshop");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.ProductFiring", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CostKwH")
                        .HasColumnType("double precision");

                    b.Property<int>("IdFiring")
                        .HasColumnType("integer");

                    b.Property<int>("IdProduct")
                        .HasColumnType("integer");

                    b.Property<int>("NumberProducts")
                        .HasColumnType("integer");

                    b.Property<double>("TotalKwH")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("IdFiring");

                    b.HasIndex("IdProduct");

                    b.ToTable("ProductFirings");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.ProductMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<int>("IdMaterial")
                        .HasColumnType("integer");

                    b.Property<int>("IdProduct")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("IdMaterial");

                    b.HasIndex("IdProduct");

                    b.ToTable("ProductMaterials");
                });

            modelBuilder.Entity("Domain.Models.WorkshopDomaine.Workshop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Workshops");
                });

            modelBuilder.Entity("Domain.Models.WorkshopDomaine.WorkshopParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdWorkshop")
                        .HasColumnType("integer");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WorksĥopId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WorksĥopId");

                    b.ToTable("WorkshopParameters");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.ImageInstruction", b =>
                {
                    b.HasOne("Domain.Models.MainDomain.Product", "ProductAssociate")
                        .WithMany("ImageInstructions")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductAssociate");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.Product", b =>
                {
                    b.HasOne("Domain.Models.WorkshopDomaine.Workshop", "Workshop")
                        .WithMany("Products")
                        .HasForeignKey("IdWorkshop")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workshop");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.ProductFiring", b =>
                {
                    b.HasOne("Domain.Models.MainDomain.Firing", "Firing")
                        .WithMany("ProductFiring")
                        .HasForeignKey("IdFiring")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.MainDomain.Product", "Product")
                        .WithMany("ProductFiring")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Firing");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.ProductMaterial", b =>
                {
                    b.HasOne("Domain.Models.MainDomain.Material", "Material")
                        .WithMany("ProductMaterial")
                        .HasForeignKey("IdMaterial")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.MainDomain.Product", "Product")
                        .WithMany("ProductMaterial")
                        .HasForeignKey("IdProduct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.WorkshopDomaine.WorkshopParameter", b =>
                {
                    b.HasOne("Domain.Models.WorkshopDomaine.Workshop", "Worksĥop")
                        .WithMany("WorkshopParameters")
                        .HasForeignKey("WorksĥopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worksĥop");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.Firing", b =>
                {
                    b.Navigation("ProductFiring");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.Material", b =>
                {
                    b.Navigation("ProductMaterial");
                });

            modelBuilder.Entity("Domain.Models.MainDomain.Product", b =>
                {
                    b.Navigation("ImageInstructions");

                    b.Navigation("ProductFiring");

                    b.Navigation("ProductMaterial");
                });

            modelBuilder.Entity("Domain.Models.WorkshopDomaine.Workshop", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("WorkshopParameters");
                });
#pragma warning restore 612, 618
        }
    }
}
