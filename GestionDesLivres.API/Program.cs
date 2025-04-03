using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Infrastructure.Persistence;
using GestionDesLivres.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using GestionDesLivres.Application.Mappings;
using Microsoft.OpenApi.Models;
using GestionDesLivres.Application.Commands.Livres;
using AutoMapper;
using GestionDesLivres.Application.Queries.Livres;
using GestionDesLivres.Application.Commands.Categories;
using MediatR;
using GestionDesLivres.Application.Queries.Categories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GestBibliothequeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestBibliothequeConnect")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestion Bibliothèque API", Version = "v1" });
});


builder.Services.AddMediatR(mdt =>
{
    // Spécifiez l'assemblage où se trouvent vos handlers
    mdt.RegisterServicesFromAssembly(typeof(AjouterLivreCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(MettreAJourLivreCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(SupprimerLivreCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(MettreAJourStockLivreCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(ObtenirLivreParIdQuery).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(ObtenirLivresEnStockQuery).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(ObtenirLivresParCategorieQuery).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(VerifierDisponibiliteLivreQuery).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(VerifierDisponibiliteLivreQuery).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(ObtenirTousLivresQuery).Assembly);

    mdt.RegisterServicesFromAssembly(typeof(AjouterCategorieCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(MettreAJourCategorieCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(SupprimerCategorieCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(ObtenirCategorieParCodeQuery).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(ObtenirTousCategoriesQuery).Assembly);

});


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IEntityValidationService<>), typeof(EntityValidationService<>));
builder.Services.AddScoped(typeof(IRecherche<>), typeof(Recherche<>));
builder.Services.AddScoped<ILivreRepository, LivreRepository>();
builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();


builder.Services.AddAutoMapper(typeof(GestionDesLivresProfile).Assembly);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestion Bibliothèque API v1"));

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
