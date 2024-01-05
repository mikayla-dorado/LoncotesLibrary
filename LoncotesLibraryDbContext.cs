using Microsoft.EntityFrameworkCore;
using LoncotesLibrary.Models.DTOs;
using LoncotesLibrary.Models;
using System.Security.Cryptography.X509Certificates;


public class LoncotesLibraryDbContext : DbContext
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    public LoncotesLibraryDbContext(DbContextOptions<LoncotesLibraryDbContext> context) : base(context)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            new Genre {Id = 1, Name = "Romance"},
            new Genre {Id = 2, Name = "Fiction"},
            new Genre {Id = 3, Name = "Religion"},
            new Genre {Id = 4, Name = "Sci-fi"},
            new Genre {Id = 5, Name = "History"}
        });

        modelBuilder.Entity<Material>().HasData(new Material[]
        {
        new Material {Id = 1, MaterialName = "The Bodyguard", MaterialTypeId = 1, GenreId = 1, OutOfCirculationSince = new DateTime(2024, 01, 01)},
        new Material {Id = 2, MaterialName = "ACOTAR", MaterialTypeId = 2, GenreId = 2, OutOfCirculationSince = null},
        new Material {Id = 3, MaterialName = "New Morning Mercies", MaterialTypeId = 2, GenreId = 3, OutOfCirculationSince = null},
        new Material {Id = 4, MaterialName = "Age of Adeline", MaterialTypeId = 1, GenreId = 2, OutOfCirculationSince = null},
        new Material {Id = 5, MaterialName = "Animal Planet", MaterialTypeId = 2, GenreId = 5, OutOfCirculationSince = null},
        new Material {Id = 6, MaterialName = "Cloverfields", MaterialTypeId = 1, GenreId = 4, OutOfCirculationSince = null},
        new Material {Id = 7, MaterialName = "Reedeming Love", MaterialTypeId = 2, GenreId = 3, OutOfCirculationSince = null},
        new Material {Id = 8, MaterialName = "Can't Buy Me Love", MaterialTypeId = 1, GenreId = 1, OutOfCirculationSince = null},
        new Material {Id = 9, MaterialName = "Robots", MaterialTypeId = 1, GenreId = 2, OutOfCirculationSince = null},
        new Material {Id = 10, MaterialName = "Cars", MaterialTypeId = 1, GenreId = 2, OutOfCirculationSince = null}
        });

        modelBuilder.Entity<MaterialType>().HasData(new MaterialType[]
        {
        new MaterialType {Id = 1, Name = "Movie", CheckoutDays = 30},
        new MaterialType {Id = 2, Name = "Book", CheckoutDays = 31},
        new MaterialType {Id = 3, Name = "CD", CheckoutDays =7}
        });

        modelBuilder.Entity<Patron>().HasData(new Patron[]
        {
        new Patron {Id = 1, FirstName = "Ely", LastName = "Dorado", Address = "123 Ct Rd", Email = "E@gmail.com", IsActive = false},
        new Patron {Id = 2, FirstName = "Amanda", LastName = "Dorado", Address = "123 Ct Rd", Email = "A@gmail.com",  IsActive = true},
        new Patron {Id = 3, FirstName = "Zeke", LastName = "Dorado", Address = "123 Ct Rd", Email = "Z@gmail.com", IsActive = true},
        });

        modelBuilder.Entity<Checkout>().HasData(new Checkout[]
        {
            new Checkout {Id = 1, MaterialId = 1, PatronId = 2, CheckoutDate = new DateTime(2023, 11, 11), ReturnDate = new DateTime(2023, 11, 21)},
            new Checkout {Id = 2, MaterialId = 2, PatronId = 1, CheckoutDate = new DateTime(2023, 12, 11)},
            new Checkout {Id = 3, MaterialId = 3, PatronId = 3, CheckoutDate = new DateTime(2023, 10, 11)},
            new Checkout {Id = 4, MaterialId = 4, PatronId = 2, CheckoutDate = new DateTime(2024, 1, 11), ReturnDate = new DateTime(2024, 1, 17)},
            new Checkout {Id = 5, MaterialId = 5, PatronId = 1, CheckoutDate = new DateTime(2023, 8, 11), ReturnDate = new DateTime(2023, 8, 16)},
            new Checkout {Id = 6, MaterialId = 6, PatronId = 3, CheckoutDate = new DateTime(2023, 1, 11), ReturnDate = new DateTime(2023, 1, 15)},
            new Checkout {Id = 7, MaterialId = 7, PatronId = 3, CheckoutDate = new DateTime(2024, 2, 11), ReturnDate = new DateTime(2024, 2, 16)},
            new Checkout {Id = 8, MaterialId = 8, PatronId = 2, CheckoutDate = new DateTime(2023, 6, 15), ReturnDate = new DateTime(2023, 6, 24)},
            new Checkout {Id = 9, MaterialId = 9, PatronId = 1, CheckoutDate = new DateTime(2023, 7, 17), ReturnDate = new DateTime(2023, 7, 17)},
            new Checkout {Id = 10, MaterialId = 10, PatronId = 1, CheckoutDate = new DateTime(2023, 9, 11), ReturnDate = new DateTime(2023, 9, 21)}
        });
    }
};