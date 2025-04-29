using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Infrastructure.Persistence;
using GestionDesLivres.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using GestionDesLivres.Application.Mappings;
using Microsoft.OpenApi.Models;
using GestionDesLivres.Application.Commands.Livres;
using GestionDesLivres.Application.Queries.Livres;
using GestionDesLivres.Application.Commands.Categories;
using GestionDesLivres.Application.Queries.Categories;
using GestionDesLivres.Application.Commands.Usagers;
using GestionDesUsagers.Application.Queries.Usagers;
using GestionDesLivres.Application.Queries;
using GestionDesLivres.Application.Commands.Retours;
using GestionDesLivres.Application.Commands.Emprunts;
using GestionDesLivres.Application.Commands.Reservations;
using GestionDesLivres.Application.Queries.Emprunts;
using GestionDesLivres.Application.Queries.Reservations;
using Serilog;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

try
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    Log.Information("Démarrage du microservice Gestion Bibliothèque");
    builder.Host.UseSerilog();

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
        mdt.RegisterServicesFromAssembly(typeof(AjouterUsagerCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(MettreAJourUsagerCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(SupprimerUsagerCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(ObtenirUsagerParIdQuery).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(ObtenirTousUsagersQuery).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(AjouterRetourCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(SupprimerEmpruntCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(ModifierEmpruntCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(AjouterEmpruntCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(ObtenirTousLesEmpruntsQuery).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(ObtenirEmpruntsParUsagerQuery).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(ObtenirToutesReservationsQuery).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(AjouterReservationCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(MettreAJourReservationCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(AnnulerReservationCommand).Assembly);
        mdt.RegisterServicesFromAssembly(typeof(EmprunterLivreReserveCommand).Assembly);
    });

    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IEntityValidationService<>), typeof(EntityValidationService<>));
    builder.Services.AddScoped(typeof(IRecherche<>), typeof(Recherche<>));
    builder.Services.AddScoped<ILivreRepository, LivreRepository>();
    builder.Services.AddScoped<ICategorieRepository, CategorieRepository>();
    builder.Services.AddScoped<IUsagerRepository, UsagerRepository>();
    builder.Services.AddScoped<IRetourRepository, RetourRepository>();
    builder.Services.AddScoped<IEmpruntRepository, EmpruntRepository>();
    builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
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

    app.UseSerilogRequestLogging(options =>
   {
       options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
       {
           // Obtenir l'ID utilisateur si disponible (ajustez selon votre système d'authentification)
           var userId = httpContext.User.Identity?.IsAuthenticated == true ?
               httpContext.User.Identity.Name : "Anonymous";

           diagnosticContext.Set("UserId", userId);
           diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
           diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());

           // Log structuré de chaque requête
           Log.Information("Requête API reçue {Method} {Path} par {UserId}",
               httpContext.Request.Method,
               httpContext.Request.Path,
               userId);
       };
   });


    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Le microservice Gestion Bibliothèque n'a pas pu démarrer correctement");
}
finally
{
    Log.CloseAndFlush();
}