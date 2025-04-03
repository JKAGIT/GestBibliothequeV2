
using GestionDesUsagers.Application.Commands;
using GestionDesUsagers.Application.Mappings;
using GestionDesUsagers.Application.Queries;
using GestionDesUsagers.Domain;
using GestionDesUsagers.Domain.Common;

using GestionDesUsagers.Domain.Repositories;
using GestionDesUsagers.Infrastructure.Persistence;
using GestionDesUsagers.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GestBibliothequeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestBibliothequeConnect")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestion Biblioth�que API", Version = "v1" });
});


builder.Services.AddMediatR(mdt =>
{
    // Sp�cifiez l'assemblage o� se trouvent vos handlers
    mdt.RegisterServicesFromAssembly(typeof(AjouterUsagerCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(MettreAJourUsagerCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(SupprimerUsagerCommand).Assembly);
    mdt.RegisterServicesFromAssembly(typeof(ObtenirUsagerParIdQuery).Assembly);
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IEntityValidationService<>), typeof(EntityValidationService<>));
builder.Services.AddScoped(typeof(IRecherche<>), typeof(Recherche<>));
builder.Services.AddScoped<IUsagerRepository, UsagerRepository>();


builder.Services.AddAutoMapper(typeof(GestionUsagersProfile).Assembly);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestion Biblioth�que API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
