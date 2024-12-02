using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuración de SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agrega Identity (si lo necesitas)
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Registra los servicios de MVC (controladores y vistas)
builder.Services.AddControllersWithViews();

// Opcional: si usas Razor Pages también
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware
app.UseStaticFiles(); // Para servir archivos estáticos (CSS, JS, etc.)
app.UseRouting();
app.UseAuthentication(); // Si tienes autenticación
app.UseAuthorization();

// Rutas para controladores y Razor Pages
app.MapDefaultControllerRoute();
app.MapRazorPages(); // Si tienes Razor Pages

app.Run();
