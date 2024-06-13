using JobCandidateHub.Application.Interfaces.Cashing;
using JobCandidateHub.Application.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace JobCandidateHub.Application;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddMemoryCache();
        services.AddScoped<ICachingService, MemoryCachingService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));
        return services;
    }


}
