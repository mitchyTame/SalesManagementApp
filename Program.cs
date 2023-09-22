using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Data;
using SalesManagementApp.Services;
using SalesManagementApp.Services.Contracts;
using Syncfusion.Blazor;

namespace SalesManagementApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("SalesManagementDbConnection")
                ?? throw new InvalidOperationException("Connection 'SalesManagementDb' does not exist");

            builder.Services.AddDbContext<SalesManagementDbContext>(
                options => options.UseSqlServer(connectionString)
                );

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();

            builder.Services.AddSyncfusionBlazor();

            var app = builder.Build();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWXZccXRRQ2JdV0B3W0s=");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}