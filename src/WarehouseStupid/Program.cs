using Microsoft.AspNetCore.Authentication.Cookies;
using WarehouseStupid.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped(srv => srv.GetRequiredService<DbConnectionFactory>().CreateConnection());
builder.Services.AddScoped<UserManager>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        opt =>
        {
            opt.LoginPath = "/Account/Login";
            opt.LoginPath = "/Account/Logout";
        });

var app = builder.Build();

// first initialize database
// await app.Services.GetRequiredService<DbConnectionFactory>()
//     .CreateConnection()
//     .CreateAndSeedAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();