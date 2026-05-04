using Business_Logic_Layer;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using GUI.Components;
using Microsoft.EntityFrameworkCore;
using Business_Logic_Layer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<MaskinContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMedarbejderRepository, MedarbejderRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHostedService<ServicePåmindelsesWorker>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MaskinContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<KundeService>();
builder.Services.AddScoped<PåmindelsesService>();
builder.Services.AddScoped<MaskineService>();
builder.Services.AddScoped<MedarbejderService>();
builder.Services.AddScoped<ServiceOpgaveService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
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