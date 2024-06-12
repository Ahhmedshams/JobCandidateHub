using JobCandidateHub.Application.Interfaces.Persistence;
using JobCandidateHub.Infrastructure.Persistence;
using JobCandidateHub.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace JobCandidateHub.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<JobCandidateHubDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<JobCandidateHubDbContext>();
        services.AddScoped<ICandidateRepository, CandidateRepository>();

        return services;
    }
}
