﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoncotesLibrary.Migrations
{
    [DbContext(typeof(LoncotesLibraryDbContext))]
    partial class LoncotesLibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LoncotesLibrary.Models.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("PatronId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("PatronId");

                    b.ToTable("Checkouts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckoutDate = new DateTime(2023, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 1,
                            PatronId = 2,
                            ReturnDate = new DateTime(2023, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CheckoutDate = new DateTime(2023, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 2,
                            PatronId = 1
                        },
                        new
                        {
                            Id = 3,
                            CheckoutDate = new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 3,
                            PatronId = 3
                        },
                        new
                        {
                            Id = 4,
                            CheckoutDate = new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 4,
                            PatronId = 2,
                            ReturnDate = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CheckoutDate = new DateTime(2023, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 5,
                            PatronId = 1,
                            ReturnDate = new DateTime(2023, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            CheckoutDate = new DateTime(2023, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 6,
                            PatronId = 3,
                            ReturnDate = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            CheckoutDate = new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 7,
                            PatronId = 3,
                            ReturnDate = new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            CheckoutDate = new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 8,
                            PatronId = 2,
                            ReturnDate = new DateTime(2023, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            CheckoutDate = new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 9,
                            PatronId = 1,
                            ReturnDate = new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
                            CheckoutDate = new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 10,
                            PatronId = 1,
                            ReturnDate = new DateTime(2023, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Fiction"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Religion"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Sci-fi"
                        },
                        new
                        {
                            Id = 5,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaterialTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OutofCirculationSince")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreId = 1,
                            MaterialName = "The Bodyguard",
                            MaterialTypeId = 1,
                            OutofCirculationSince = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 2,
                            MaterialName = "ACOTAR",
                            MaterialTypeId = 2
                        },
                        new
                        {
                            Id = 3,
                            GenreId = 3,
                            MaterialName = "New Morning Mercies",
                            MaterialTypeId = 2
                        },
                        new
                        {
                            Id = 4,
                            GenreId = 2,
                            MaterialName = "Age of Adeline",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 5,
                            GenreId = 5,
                            MaterialName = "Animal Planet",
                            MaterialTypeId = 2
                        },
                        new
                        {
                            Id = 6,
                            GenreId = 4,
                            MaterialName = "Cloverfields",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 7,
                            GenreId = 3,
                            MaterialName = "Reedeming Love",
                            MaterialTypeId = 2
                        },
                        new
                        {
                            Id = 8,
                            GenreId = 1,
                            MaterialName = "Can't Buy Me Love",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 9,
                            GenreId = 2,
                            MaterialName = "Robots",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 10,
                            GenreId = 2,
                            MaterialName = "Cars",
                            MaterialTypeId = 1
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CheckoutDays")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MaterialTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckoutDays = 30,
                            Name = "Movie"
                        },
                        new
                        {
                            Id = 2,
                            CheckoutDays = 31,
                            Name = "Book"
                        },
                        new
                        {
                            Id = 3,
                            CheckoutDays = 7,
                            Name = "CD"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Patron", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patrons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Ct Rd",
                            Email = "E@gmail.com",
                            FirstName = "Ely",
                            IsActive = false,
                            LastName = "Dorado"
                        },
                        new
                        {
                            Id = 2,
                            Address = "123 Ct Rd",
                            Email = "A@gmail.com",
                            FirstName = "Amanda",
                            IsActive = true,
                            LastName = "Dorado"
                        },
                        new
                        {
                            Id = 3,
                            Address = "123 Ct Rd",
                            Email = "Z@gmail.com",
                            FirstName = "Zeke",
                            IsActive = true,
                            LastName = "Dorado"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Checkout", b =>
                {
                    b.HasOne("LoncotesLibrary.Models.Material", "Material")
                        .WithMany("Checkouts")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoncotesLibrary.Models.Patron", "Patron")
                        .WithMany("Checkouts")
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Patron");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.HasOne("LoncotesLibrary.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoncotesLibrary.Models.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MaterialTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.Navigation("Checkouts");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Patron", b =>
                {
                    b.Navigation("Checkouts");
                });
#pragma warning restore 612, 618
        }
    }
}
