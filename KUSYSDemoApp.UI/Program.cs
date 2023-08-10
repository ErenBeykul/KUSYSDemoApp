using KUSYSDemoApp.DataAccess;
using KUSYSDemoApp.UI.Management;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar();

builder.Host.ConfigureContainer<ServiceRegistry>(services =>
{
    services.Scan(x =>
    {
        x.TheCallingAssembly();
        x.WithDefaultConventions();
        x.Assembly("KUSYSDemoApp.Service");
        x.Assembly("KUSYSDemoApp.DataAccess");
    });

    services.Policies.SetAllProperties(y => y.OfType<IControllerManager>());
});

// Add services to the container.
builder.Services.AddDbContext<KUSYSDemoAppContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("KUSYSDemoAppContext")));
builder.Services.AddControllersWithViews().AddControllersAsServices().AddRazorRuntimeCompilation();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); });
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

