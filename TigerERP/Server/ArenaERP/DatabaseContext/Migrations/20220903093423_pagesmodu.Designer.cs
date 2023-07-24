﻿// <auto-generated />
using System;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseContext.Migrations
{
    [DbContext(typeof(DatabaseContextDb))]
    [Migration("20220903093423_pagesmodu")]
    partial class pagesmodu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ModelClass.ERP.DTO.Modules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<int?>("IsParent")
                        .HasColumnType("int");

                    b.Property<string>("ModuleRoutePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("SerialNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("ModelClass.ERP.DTO.Pages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int>("ModulesId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageRoutePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SerialNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModulesId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("ModelClass.ERP.DTO.Pages", b =>
                {
                    b.HasOne("ModelClass.ERP.DTO.Modules", "Modules")
                        .WithMany("Pages")
                        .HasForeignKey("ModulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modules");
                });

            modelBuilder.Entity("ModelClass.ERP.DTO.Modules", b =>
                {
                    b.Navigation("Pages");
                });
#pragma warning restore 612, 618
        }
    }
}
