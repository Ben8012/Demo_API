
using Demo1_ASP_MVC.Service;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddScoped<SessionManager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



// correction
app.MapControllerRoute(
    name: "Monsite",
    pattern: "Monsite/{action = Index}",
    defaults: new { Controller = "Home" }
    );
app.MapControllerRoute(
    name: "no-prefix",
    pattern: "{action = Index}",
    defaults: new { Controller = "Home" }
    );
app.MapControllerRoute(
    name: "contrat",
    pattern: "Contrat-de-confidentialite",
    defaults: new { Controller = "Home", Action = "Privacy" }
    );

// erreur 
app.MapControllerRoute(
    name: "erreur-slide",
    pattern: "Home/17",
    defaults: new { Controller = "Home", Action = "Privacy" }
    );

// Default
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





app.Run();
