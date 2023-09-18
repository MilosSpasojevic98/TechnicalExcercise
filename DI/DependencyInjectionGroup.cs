using Service.Services;

namespace DI
{
    public static class DependencyInjectionGroup
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }

        public static IServiceCollection AddMyServiceDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();

            return services;
        }
    }
}