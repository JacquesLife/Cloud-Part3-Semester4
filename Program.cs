using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add configuration for ProductTable
            builder.Services.AddScoped<ProductTable>();

            // Add configuration for UserTable
            builder.Configuration.AddJsonFile("appsettings.json");

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Register FileShareService with dependency injection
            builder.Services.AddScoped<FileShareService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                string connectionString = configuration.GetConnectionString("AzureFileShare");
                return new FileShareService(connectionString, "jacquesfileshare");
            });

            // Register BlobStorageService with dependency injection
            builder.Services.AddScoped<BlobStorageService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                string connectionString = configuration.GetConnectionString("AzureBlobStorage");
                return new BlobStorageService(connectionString);
            });

            // Register QueueStorageService with dependency injection
            builder.Services.AddScoped<QueueStorageService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                string connectionString = configuration.GetConnectionString("AzureQueueStorage");
                string queueName = "jacquesqueue";
                return new QueueStorageService(connectionString, queueName);
            });

            // Register TableStorageService with dependency injection
            builder.Services.AddScoped<TableStorageService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                string connectionString = configuration.GetConnectionString("MyDatabaseConnection");
                string tableName = "productTable";
                return new TableStorageService(connectionString, tableName);
            });

            // Register TableStorageService with dependency injection
            builder.Services.AddScoped<TableStorageService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                string connectionString = configuration.GetConnectionString("AzureTableStorage"); //
                string tableName = "jacquestable"; 
                return new TableStorageService(connectionString, tableName);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
        }
    }
}
