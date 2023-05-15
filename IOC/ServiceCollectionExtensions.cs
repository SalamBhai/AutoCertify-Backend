using System.ComponentModel.Design;
using Application.Abstraction.Interfaces.Repositories.Utilities;
using Application.Abstraction.Repositories;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Services.Utilities;

namespace IOC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddApplication()
            .AddScoped<IEventIncrementer, EventIncrementer>();
            return services;
        }
        
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            
            services.AddScoped<IParticipantRepository, ParticipantRepository>()
            .AddScoped<ITrainingCategoryRepository, TrainingCategoryRepository>()
            .AddScoped<ITrainingRepository, TrainingRepository>();

            return services;
        }

       

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connectionString));
            return services;
        }
    }
}