using proyectoAgencia.Interfaces;
using proyectoAgencia.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPaquetesModel, PaquetesModel>();
builder.Services.AddScoped<IAgenciaViajesModel, AgenciaViajesModel>();
builder.Services.AddScoped<IUsuariosModel, UsuariosModel>();
builder.Services.AddScoped<ICarritoModel, CarritoModel>();
 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


