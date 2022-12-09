using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ScSoMeBlazorServer.Data;
using ScSoMeBlazorServer.Data.ConnectionService;
using ScSoMeBlazorServer.Data.LogActivityService;
using ScSoMeBlazorServer.Data.TimelineService;
using ScSoMeBlazorServer.Network;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ILogActivityService, LogActivityService>();
builder.Services.AddSingleton<IConnectionService,ConnectionService>();
builder.Services.AddSingleton<ITimelineService, TimelineService>();
builder.Services.AddHttpClient<IAPIClient, APIClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5003");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();