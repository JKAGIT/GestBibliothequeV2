using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Authentication.WebAssembly.Msal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using GestBibliotheque.Blazor;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Http;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuration de l'URL de l'API
var apiBaseUrl = builder.Configuration.GetSection("ApiSettings:BaseUrl")?.Value ?? "https://localhost:5000";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

//builder.Services.AddHttpClient("BibliothequeAPI", client =>
//{
//    client.BaseAddress = new Uri(apiBaseUrl);
//})
//.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BibliothequeAPI"));


// Configuration de l'authentification MSAL
builder.Services.AddMsalAuthentication(options =>
{
    var azureAd = builder.Configuration.GetSection("AzureAd");

    var instance = azureAd["Instance"]; 
    var tenantId = azureAd["TenantId"]; 
    var clientId = azureAd["ClientId"]; 

    if (string.IsNullOrWhiteSpace(instance) || string.IsNullOrWhiteSpace(tenantId) || string.IsNullOrWhiteSpace(clientId))
    {
        throw new InvalidOperationException("L'instance, le tenantId ou le clientId est vide ou null.");
    }

     var authority = $"{instance}{tenantId}"; 

    if (!Uri.IsWellFormedUriString(authority, UriKind.Absolute))
    {
        throw new InvalidOperationException($"L'URL de l'autorité Azure AD est mal formée : {authority}");
    }

    // Applique la configuration pour l'authentification MSAL
    options.ProviderOptions.Authentication.Authority = authority;
    options.ProviderOptions.Authentication.ClientId = clientId;

    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("profile");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("email");

    //options.ProviderOptions.DefaultAccessTokenScopes.Add("api://bibliotheque-api/api.read");
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("api://bibliotheque-api/api.write");

    options.ProviderOptions.LoginMode = "redirect"; 
});

await builder.Build().RunAsync();
