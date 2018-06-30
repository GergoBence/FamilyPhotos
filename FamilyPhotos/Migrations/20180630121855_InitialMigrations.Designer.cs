﻿// <auto-generated />
using System;
using FamilyPhotos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyPhotos.Migrations
{
    [DbContext(typeof(FamilyPhotosContext))]
    [Migration("20180630121855_InitialMigrations")]
    partial class InitialMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FamilyPhotos.Models.PhotoModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<byte[]>("Picture");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("id");

                    b.ToTable("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
