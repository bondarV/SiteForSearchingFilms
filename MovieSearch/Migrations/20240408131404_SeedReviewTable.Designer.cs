﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieSearch.Data;

#nullable disable

namespace MovieSearch.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240408131404_SeedReviewTable")]
    partial class SeedReviewTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieSearch.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Animation"
                        });
                });

            modelBuilder.Entity("MovieSearch.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SpoilersConsist")
                        .HasColumnType("bit");

                    b.Property<string>("TextReview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Headline = "Beautiful Landscapes",
                            SpoilersConsist = false,
                            TextReview = "If that makes any sense. What I'm trying to say while pointing Aristotle's quote into a mirror, is that this is worth watching simply for all of the outstanding individual performances. There are many other reasons to tune in, but the acting clinic on parade here is a lot of fun."
                        });
                });
#pragma warning restore 612, 618
        }
    }
}