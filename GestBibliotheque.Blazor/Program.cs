
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GestBibliotheque.Blazor;
using Microsoft.AspNetCore.Components.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuration de l'URL de l'API

var apiBaseUrl = builder.Configuration.GetSection("ApiSettings:BaseUrl")?.Value ?? "http://localhost:5000";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

await builder.Build().RunAsync();

