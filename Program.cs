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
    .Where(m => m.OutofCirculationSince == null)
    .Include(m => m.Genre)
    .Include(m => m.MaterialType)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        GenreId = m.GenreId,
        OutofCirculationSince = m.OutofCirculationSince,
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
        OutofCirculationSince = m.OutofCirculationSince,
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
        OutofCirculationSince = foundMaterial.OutofCirculationSince,
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
    materialRemove.OutofCirculationSince = DateTime.Now;
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


app.Run();

