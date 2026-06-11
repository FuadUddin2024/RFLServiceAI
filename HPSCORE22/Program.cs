using QCMS.Repositories;
using QCMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Rotativa.AspNetCore;
using CSWMS.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Add your DatabaseService and UserRepository
builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<QCCompanyService>();
builder.Services.AddScoped<QCCompanyRepository>();
builder.Services.AddScoped<QCLocationService>();
builder.Services.AddScoped<QCLocationRepository>();
builder.Services.AddScoped<CommonService>();
builder.Services.AddScoped<CommonRepository>();
builder.Services.AddScoped<ComplainListRepository>();
builder.Services.AddScoped<AssignRepository>();
builder.Services.AddScoped<ZoneRepository>();
builder.Services.AddScoped<CompanyRepository>();
builder.Services.AddScoped<SuperVisorRepository>();
builder.Services.AddScoped<StatusRepository>();



builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

builder.Services.AddScoped<IDashboardService, DashboardService>();

// ===============================
// Authentication (Cookie)
// ===============================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Login";
        options.AccessDeniedPath = "/Accounts/Denied";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // match session
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

RotativaConfiguration.Setup(
    Path.Combine(app.Environment.WebRootPath, "Rotativa")
);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UsePathBase("/HPSWeb");
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Enable WebSockets so dotnet-watch browser refresh can complete the handshake
app.UseWebSockets();
// Enable session before authorization
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// ===============================
// SESSION EXPIRY MIDDLEWARE
// ===============================
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();

    // Skip public paths
    if (path != null &&
        (path.StartsWith("/accounts/login") ||
         path.StartsWith("/accounts/logout") ||
         path.StartsWith("/css") ||
         path.StartsWith("/js") ||
         path.StartsWith("/images")))
    {
        await next();
        return;
    }

    if (context.User.Identity?.IsAuthenticated == true)
    {
        var user = context.Session.GetString("USERID");

        // Session expired but cookie still exists
        if (string.IsNullOrEmpty(user))
        {
            await context.SignOutAsync();
            context.Response.Redirect("/Accounts/Login");
            return;
        }
    }

    await next();
});
// AREA ROUTE (if you have Areas)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}"
);

app.MapControllers(); // enable API endpoints

app.Run();
