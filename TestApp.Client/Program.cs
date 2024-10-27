using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Net.Http;
using TestApp.Client;
using TestApp.Client.Services;
using TestApp.Client.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();


builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:2345/api/");
})
.AddHttpMessageHandler<AuthHandler>();

builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorageAsSingleton();


await builder.Build().RunAsync();
