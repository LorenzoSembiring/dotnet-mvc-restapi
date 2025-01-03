using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Add services to the container
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DataContext");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

RegisteredServices(builder.Services, Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Apply the CORS policy
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void RegisteredServices(IServiceCollection services, Assembly assembly)
{
    var scopedServiceType = typeof(IScopedService);

    var types = assembly.GetExportedTypes();

    foreach (var type in types)
    {
        if (scopedServiceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
        {
            services.AddScoped(type);
        }
    }
}
