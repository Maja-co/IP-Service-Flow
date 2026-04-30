using Business_Logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using GUI.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using Business_Logic_Layer.Services;
using GUI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<MaskinContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMedarbejderRepository, MedarbejderRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHostedService<ServicePåmindelsesWorker>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Vi fortæller appen, at den skal bruge MaskinContext med SQL Server
builder.Services.AddDbContext<MaskinContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<KundeService>();
builder.Services.AddScoped<PåmindelsesService>();
builder.Services.AddScoped<MaskineService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();