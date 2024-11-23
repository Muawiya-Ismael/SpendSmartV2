using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpendSmartV2.Interface;
using SpendSmartV2.Repository;
using SpendSmartV2.Data;
using SpendSmartV2.Services.Expenses;
using SpendSmartV2.Services.Account;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<SpendSmartDbContext>(options =>options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<SpendSmartDbContext>();
builder.Services.AddScoped<IExpensesServices, ExpensesServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountServices, AccountServices>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.Run();