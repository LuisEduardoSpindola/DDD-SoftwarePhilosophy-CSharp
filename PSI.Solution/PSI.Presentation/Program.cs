using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PSI.Presentation.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PSIPresentationContextConnection") ?? throw new InvalidOperationException("Connection string 'PSIPresentationContextConnection' not found.");

builder.Services.AddDbContext<PSIPresentationContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PSIUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PSIPresentationContext>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
