using Microsoft.EntityFrameworkCore;
using testing.Infrastructure.Data;
using testing.Application.Interfaces.Information;
using testing.Application.Interfaces.Hobby;
using testing.Application.Services.Information;
using testing.Application.Services.Hobby;
using testing.Infrastructure.Repository.Hobby;
using testing.Infrastructure.Repository.Information;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAddInfo, AddInfoRepository>();
builder.Services.AddScoped<AddInfoAsyncServices>();

builder.Services.AddScoped<IGetInfo, GetInfoRepository>();
builder.Services.AddScoped<GetInfoServices>();


builder.Services.AddScoped<IHobby, AddHobbyRepository>();
builder.Services.AddScoped<HobbyServices>();

builder.Services.AddScoped<IHobbyGetAllRecords, GellAllHobbyRepository>();
builder.Services.AddScoped<GetAllHobbyServices>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name:"default",
    pattern: "{controller=Home}/{action=Home}/{id?}"
);

app.Run();
