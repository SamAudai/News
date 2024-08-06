using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using News.Client;
using News.Client.Authentication;
using News.Client.Services;
using News.Shared.DTOs;
using News.Shared.DTOs.Adminstration;
using News.Shared.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IMainService<Category>, MainService<Category>>();
builder.Services.AddScoped<IMainService<NewsList>, MainService<NewsList>>();
builder.Services.AddScoped<IMainService<NewsList_DTO>, MainService<NewsList_DTO>>();
builder.Services.AddScoped<IMainService<Comment>, MainService<Comment>>();

builder.Services.AddScoped<IMainService<RolesDto>, MainService<RolesDto>>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();
