using Microsoft.AspNetCore.Authentication;
using Repositories.Data;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;

namespace Assignment1.ServiceExtension
{
    public static class ApplicationServiceExtension
    {
        const string MyAllowSpecificOrigins = "myAllowSpecificOrigins";

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add your application services here
            services
                .AddRepositories()
                .AddServices()
                .AddCorsConfiguration()
                .AddSerControllers()
                .AddDbContext()
                ;

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Add your repositories here
            services.AddScoped<IBranchAccountRepository, BranchAccountRepository>();
            services.AddScoped<ISilverJewelryRepository, SilverJewelryRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }

        private static IServiceCollection AddDbContext (this IServiceCollection services)
        {
            services.AddDbContext<SilverJewelry2023DbContext>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Add your services here
            services.AddScoped<IBranchAccountService, BranchAccountService>();
            services.AddScoped<ISilverJewelryService, SilverJewelryService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }

        private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            // CORS config
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        }
                    );
            });

            return services;
        }

        private static IServiceCollection AddSerControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // Configure JSON options to handle circular references
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error;
                });


            return services;
        }
    }
}
