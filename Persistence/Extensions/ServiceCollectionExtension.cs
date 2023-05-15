using Application.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ServiceCollectionExtension
{
     public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AutoCert-ConnectionString");
        services.AddDbContext<ApplicationContext>(option => 
        option.UseNpgsql(connectionString));
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
       services.AddScoped<ITrainingRepository, TrainingRepository>()
        .AddScoped<ITrainingCategoryRepository, TrainingCategoryRepository>()
         .AddScoped<IParticipantRepository, ParticipantRepository>()
        .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
     
   
}

