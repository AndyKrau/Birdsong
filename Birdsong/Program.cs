using Birdsong.Models;
using Birdsong.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();
builder.Services.AddTransient<JsonFileProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Configure 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();
#region Endpoint for API. Bad example
//app.MapGet("/birds", (context) => {

//    var options = new JsonSerializerOptions
//    {
//        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
//        WriteIndented = true,
//        AllowTrailingCommas = true
//    };
//    var birds = app.Services.GetService<JsonFileProductService>().GetBirds();
//    var json = JsonSerializer.Serialize<IEnumerable<Bird>>(birds, options);
//    return context.Response.WriteAsJsonAsync(json);
//});
#endregion

app.Run();
