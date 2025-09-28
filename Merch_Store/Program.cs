using merch_store.DB_Layer.Contexts;
using Microsoft.EntityFrameworkCore;
using merch_store.API_Layer;
using merch_store.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var conn = builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// если API нужны отдельно:
builder.Services.AddControllers();

builder.Services.AddScoped<merch_store.API_Layer.ProductRepository>();
builder.Services.AddScoped<merch_store.BusinessLogic.Services.ProductService>();

builder.Services.AddControllersWithViews();


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
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
