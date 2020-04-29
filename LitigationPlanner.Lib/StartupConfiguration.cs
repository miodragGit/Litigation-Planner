using LitigationPlanner.Lib.ServiceInterfaces;
using LitigationPlanner.Lib.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LitigationPlanner.Lib
{
    public static class StartupConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            Data.StartupConfiguration.ConfigureServices(services);

            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ILitigationService, LitigationService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IProcessTypeService, ProcessTypeService>();

        }
    }
}
