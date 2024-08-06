using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using News.Server.Models;
using News.Server.Repositories;
using News.Server.Repositories.Interfaces;
using News.Shared.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtSetting = builder.Configuration.GetSection("JwtSetting");

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<NewsDbContext>(option =>
   option.UseSqlServer(builder.Configuration.GetConnectionString("NewsConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    //option.SignIn.RequireConfirmedAccount = true;
}).AddEntityFrameworkStores<NewsDbContext>().
   AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSetting["ValidIssuer"],
        ValidAudience = jwtSetting["ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["SecurityKey"]))
    };
});

builder.Services.AddScoped<GenericInterface<Category>, GenericRepository<Category>>();
builder.Services.AddScoped<GenericInterface<NewsList>, GenericRepository<NewsList>>();
builder.Services.AddScoped<GenericInterface<Comment>, GenericRepository<Comment>>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
