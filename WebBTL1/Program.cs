using Microsoft.EntityFrameworkCore;
using WebBTL1.Data;
using WebBTL1.Services;
using WebBTL1.Repository;
using WebBTL1.Repository.Interface;
using WebBTL1.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")
	));
// Employees
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IEmployeesRepo, EmployeesRepo>();

// Provinces
builder.Services.AddScoped<IProvinceRepo, ProvinceRepo>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();

// Districts
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IDistrictRepo, DistrictRepo>();

// Communes
builder.Services.AddScoped<ICommuneService, CommuneService>();
builder.Services.AddScoped<ICommuneRepo, CommuneRepo>();



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

app.Run();
