using Microsoft.Data.Sqlite;using WarehouseStupid.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<SqliteConnection>(srv => new SqliteConnection("Data Source=warehouse.sqlite"));

var app = builder.Build();

var dbFilePath = "Data Source=" + Path.Combine(app.Environment.ContentRootPath, "warehouse.sqlite"); 
var conn = new SqliteConnection(dbFilePath);
await conn.CreateAndSeedAsync();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();