using AutoMapper;
using LitigationPlanner.Data.DbContexts;
using LitigationPlanner.Data.Mapping;
using LitigationPlanner.Data.Models.Entities;
using LitigationPlanner.Data.Repositories;
using LitigationPlanner.Data.RepositoryInterfaces;
using LitigationPlanner.Data.WorkUnits;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LitigationPlanner.Data
{
    public class StartupConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("litigationplanner.data.json",
                    optional: true)
                .Build();

            services.AddDbContext<LitigationPlannerDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LitigationPlannerDBContext>();

            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntitiesMapper());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ILitigationRepository, LitigationRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IProcessTypeRepository, ProcessTypeRepository>();
            services.AddScoped<ILitigationPlannerUnitOfWork, LitigationPlannerUnitOfWork>();
        }
    }
}
