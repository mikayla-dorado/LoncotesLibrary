using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using LoncotesLibrary.Models;
using LoncotesLibrary.Models.DTOs;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.Authentication.Cookies;


// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddNpgsql<LoncotesLibraryDbContext>(builder.Configuration["LoncotesLibraryDbConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//get a list of all the circulating materials, 
//include the genre and material type. exclude materials that have a outofcirculationsice value
app.MapGet("/api/materials", (LoncotesLibraryDbContext db) =>
{
    return db.Materials
    .Where(m => m.OutOfCirculationSince == null)
    .Include(m => m.Genre)
    .Include(m => m.MaterialType)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        GenreId = m.GenreId,
        OutOfCirculationSince = m.OutOfCirculationSince,
        MaterialType = new MaterialTypeDTO
        {
            Id = m.MaterialType.Id,
            Name = m.MaterialType.Name,
            CheckoutDays = m.MaterialType.CheckoutDays
        },
        Genre = new GenreDTO
        {
            Id = m.Genre.Id,
            Name = m.Genre.Name
        }
    });
});

//get materials by genre and/or material type
//have to account for possibility that either type them will be missing
app.MapGet("/api/materials/bygenrematerialtype", (LoncotesLibraryDbContext db, int? genreId, int? materialTypeId) =>
{
    return db.Materials
    .Where(m => (!genreId.HasValue || m.GenreId == genreId) && (!materialTypeId.HasValue || m.MaterialTypeId == materialTypeId))
    .Include(m => m.Genre)
    .Include(m => m.MaterialType)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        GenreId = m.GenreId,
        OutOfCirculationSince = m.OutOfCirculationSince,
        MaterialType = new MaterialTypeDTO
        {
            Id = m.MaterialType.Id,
            Name = m.MaterialType.Name,
            CheckoutDays = m.MaterialType.CheckoutDays
        },
        Genre = new GenreDTO
        {
            Id = m.Genre.Id,
            Name = m.Genre.Name
        }
    });
});

//get materials, include genre, materialtype, and checkouts(patron associated with checkout)
//DO NOT add material and materialtype to each checkout
app.MapGet("/api/materials/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    Material foundMaterial = db.Materials
    .Include(m => m.Genre)
    .Include(m => m.MaterialType)
    .Include(m => m.Checkouts)
    .ThenInclude(m => m.Patron)
    .FirstOrDefault(c => c.Id == id);

    if (foundMaterial == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new MaterialDTO
    {
        Id = foundMaterial.Id,
        MaterialName = foundMaterial.MaterialName,
        MaterialTypeId = foundMaterial.MaterialTypeId,
        GenreId = foundMaterial.GenreId,
        OutOfCirculationSince = foundMaterial.OutOfCirculationSince,
        MaterialType = new MaterialTypeDTO
        {
            Id = foundMaterial.MaterialType.Id,
            Name = foundMaterial.MaterialType.Name,
            CheckoutDays = foundMaterial.MaterialType.CheckoutDays
        },
        Genre = new GenreDTO
        {
            Id = foundMaterial.Genre.Id,
            Name = foundMaterial.Genre.Name
        },
        Checkouts = foundMaterial.Checkouts.Select(c => new CheckoutDTO
        {
            Id = c.Id,
            MaterialId = c.MaterialId,
            PatronId = c.PatronId,
            CheckoutDate = c.CheckoutDate,
            ReturnDate = c.ReturnDate,
            Patron = new PatronDTO
            {
                Id = c.Patron.Id,
                FirstName = c.Patron.FirstName,
                LastName = c.Patron.LastName,
                Address = c.Patron.Address,
                Email = c.Patron.Email,
                IsActive = c.Patron.IsActive
            }
        }).ToList()
    });
});

app.MapPost("/api/materials", (LoncotesLibraryDbContext db, Material newMat) =>
{
    try
    {
        db.Materials.Add(newMat);
        db.SaveChanges();
        return Results.Created($"/api/materials/{newMat.Id}", newMat);
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Invalid data submitted");
    }
});

//delete a material from circulation
//a soft delete
app.MapPost("/api/materials/{id}/remove", (LoncotesLibraryDbContext db, int id) =>
{
    Material materialRemove = db.Materials.SingleOrDefault(m => m.Id == id);
    if (materialRemove == null)
    {
        return Results.NotFound();
    }
    materialRemove.OutOfCirculationSince = DateTime.Now;
    db.SaveChanges();
    return Results.Ok($"{materialRemove.MaterialName} has been removed");
});

//get material types
app.MapGet("/api/materialTypes", (LoncotesLibraryDbContext db) =>
{
    return db.MaterialTypes
    .Select(mt => new MaterialTypeDTO
    {
        Id = mt.Id,
        Name = mt.Name,
        CheckoutDays = mt.CheckoutDays
    });
});

//get genres
app.MapGet("/api/genres", (LoncotesLibraryDbContext db) =>
{
    return db.Genres
    .Select(g => new GenreDTO
    {
        Id = g.Id,
        Name = g.Name
    });
});


//get patrons
app.MapGet("/api/patrons", (LoncotesLibraryDbContext db) =>
{
    return db.Patrons
    .Select(p => new PatronDTO
    {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        Address = p.Address,
        Email = p.Email,
        IsActive = p.IsActive
    });
});


//get patrons details by id
app.MapGet("/api/patrons/{id}", (LoncotesLibraryDbContext db, int id) =>
{
     Patron foundPatron = db.Patrons
        .Include(p => p.Checkouts)
        .FirstOrDefault(p => p.Id == id);

    if (foundPatron == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new PatronDTO
    {
        Id = foundPatron.Id,
        FirstName = foundPatron.FirstName,
        LastName = foundPatron.LastName,
        Address = foundPatron.Address,
        Email = foundPatron.Email,
        IsActive = foundPatron.IsActive,

        Checkouts = db.Checkouts
            .Where(pc => pc.PatronId == foundPatron.Id)
            .Select(c => new CheckoutDTO
            {
                Id = c.Id,
                MaterialId = c.MaterialId,
                PatronId = c.PatronId,
                CheckoutDate = c.CheckoutDate,
                ReturnDate = c.ReturnDate
            })
            .ToList(),
        CheckoutsWithLateFee = db.Checkouts
            .Where(pc => pc.PatronId == foundPatron.Id)
            .Select(c => new CheckoutWithLateFeeDTO
            {
                Id = c.Id,
                MaterialId = c.MaterialId,
                PatronId = c.PatronId,
                CheckoutDate = c.CheckoutDate,
                ReturnDate = c.ReturnDate,
                Material = new MaterialDTO
                {
                    Id = c.Material.Id,
                    MaterialName = c.Material.MaterialName,
                    MaterialTypeId = c.Material.MaterialTypeId,
                    GenreId = c.Material.GenreId,
                    OutOfCirculationSince = c.Material.OutOfCirculationSince,
                    MaterialType = new MaterialTypeDTO
                    {
                        Id = c.Material.MaterialType.Id,
                        Name = c.Material.MaterialType.Name,
                        CheckoutDays = c.Material.MaterialType.CheckoutDays
                    }
                }
            })
            .ToList()
    });
});


//get patrons with checkouts, include materials and their material types
app.MapGet("/api/patrons/withcheckouts", (LoncotesLibraryDbContext db) =>
{
    return db.Patrons
    .Where(p => p.Checkouts.Any())
    .Select(p => new PatronDTO
    {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        Address = p.Address,
        Email = p.Email,
        IsActive = p.IsActive,
        Checkouts = p.Checkouts.Select(c => new CheckoutDTO
        {
            Id = c.Id,
            MaterialId = c.MaterialId,
            PatronId = c.PatronId,
            CheckoutDate = c.CheckoutDate,
            ReturnDate = c.ReturnDate
        }).ToList()
    });
});


//update patrons
app.MapPut("/api/patrons/{id}", (LoncotesLibraryDbContext db, int id, Patron patron) =>
{
    Patron patronUpdate = db.Patrons.SingleOrDefault(p => p.Id == id);
    if (patronUpdate == null)
    {
        return Results.NotFound();
    }
    patronUpdate.FirstName = patron.FirstName;
    patronUpdate.LastName = patron.LastName;
    patronUpdate.Address = patron.Address;
    patronUpdate.Email = patron.Email;
    patronUpdate.IsActive = patron.IsActive;

    db.SaveChanges();
    return Results.NoContent();
});


//deactivate a patron (soft delete again)
app.MapPost("/api/patrons/{id}/deactivate", (LoncotesLibraryDbContext db, int id) =>
{
    Patron patronDeactivate = db.Patrons.SingleOrDefault(p => p.Id == id);
    if (patronDeactivate == null)
    {
        return Results.NotFound();
    }
    patronDeactivate.IsActive = false;
    db.SaveChanges();
    return Results.Ok($"{patronDeactivate.FirstName} {patronDeactivate.LastName} has been deactivated");
});


//librarian needs to be able to checkout items for patrons
//create a new checkout for a material & a patron
//automatically set the checkout date to DateTime.Today
app.MapPost("/api/checkouts", (LoncotesLibraryDbContext db, Checkout checkout) =>
{
    try
    {
        checkout.CheckoutDate = DateTime.Today;
        db.Checkouts.Add(checkout);
        db.SaveChanges();
        return Results.Created($"/api/checkouts/{checkout.Id}", checkout);
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Invalid data submitted");
    }
});


//return a material
app.MapPost("/api/checkouts/{id}/return", (LoncotesLibraryDbContext db, int id) =>
{
    Checkout checkoutReturn = db.Checkouts.SingleOrDefault(c => c.Id == id);
    if (checkoutReturn == null)
    {
        return Results.NotFound();
    }
    checkoutReturn.ReturnDate = DateTime.Today;
    db.SaveChanges();
    return Results.Ok("Material has been returned");
});


//get all materials with their genre & material type that are currently available(not checkout)
app.MapGet("/api/materials/available", (LoncotesLibraryDbContext db) =>
{
    return db.Materials
    .Where(m => m.OutOfCirculationSince == null)
    .Where(m => m.Checkouts.All(co => co.ReturnDate != null))
    .Select(Material => new MaterialDTO
    {
        Id = Material.Id,
        MaterialName = Material.MaterialName,
        MaterialTypeId = Material.MaterialTypeId,
        GenreId = Material.GenreId,
        OutOfCirculationSince = Material.OutOfCirculationSince,
        MaterialType = new MaterialTypeDTO
        {
            Id = Material.MaterialType.Id,
            Name = Material.MaterialType.Name,
            CheckoutDays = Material.MaterialType.CheckoutDays
        },
        Genre = new GenreDTO
        {
            Id = Material.Genre.Id,
            Name = Material.Genre.Name
        }
    }).ToList();
});


//calculating late fees
app.MapGet("/api/checkouts/overdue", (LoncotesLibraryDbContext db) =>
{
    return db.Checkouts
    .Include(p => p.Patron)
    .Include(co => co.Material)
    .ThenInclude(m => m.MaterialType)
    .Where(co =>
        (DateTime.Today - co.CheckoutDate).Days > co.Material.MaterialType.CheckoutDays
        &&
        co.ReturnDate == null)
        .Select(co => new CheckoutWithLateFeeDTO
        {
            Id = co.Id,
            MaterialId = co.MaterialId,
            Material = new MaterialDTO
            {
                Id = co.Material.Id,
                MaterialName = co.Material.MaterialName,
                MaterialTypeId = co.Material.MaterialTypeId,
                MaterialType = new MaterialTypeDTO
                {
                    Id = co.Material.MaterialTypeId,
                    Name = co.Material.MaterialType.Name,
                    CheckoutDays = co.Material.MaterialType.CheckoutDays
                },
                GenreId = co.Material.GenreId,
                OutOfCirculationSince = co.Material.OutOfCirculationSince
            },
            PatronId = co.PatronId,
            Patron = new PatronDTO
            {
                Id = co.Patron.Id,
                FirstName = co.Patron.FirstName,
                LastName = co.Patron.LastName,
                Address = co.Patron.Address,
                Email = co.Patron.Email,
                IsActive = co.Patron.IsActive
            },
            CheckoutDate = co.CheckoutDate,
            ReturnDate = co.ReturnDate
        })
        .ToList();
});



app.Run();