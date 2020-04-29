using LitigationPlanner.Data.DbContexts;
using LitigationPlanner.Data.RepositoryInterfaces;
using LitigationPlanner.Data.WorkUnitsInterfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.WorkUnits
{
    public class LitigationPlannerUnitOfWork : ILitigationPlannerUnitOfWork
    {
        private readonly LitigationPlannerDBContext dbContext;

        public ICompanyRepository CompanyRepository { get; set; }
        public IContactRepository ContactRepository { get; set; }
        public ILitigationRepository LitigationRepository { get; set; }
        public ILocationRepository LocationRepository { get; set; }
        public IProcessTypeRepository ProcessTypeRepository { get; set; }
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }

        public LitigationPlannerUnitOfWork(LitigationPlannerDBContext dbContext,
                                           ICompanyRepository companyRepository,
                                           IContactRepository contactRepository,
                                           ILitigationRepository litigationRepository,
                                           ILocationRepository locationRepository,
                                           IProcessTypeRepository processTypeRepository,
                                           IApplicationUserRepository applicationUserRepository,
                                           IRoleRepository roleRepository)
        {
            this.dbContext = dbContext;
            this.CompanyRepository = companyRepository;
            this.ContactRepository = contactRepository;
            this.LitigationRepository = litigationRepository;
            this.LocationRepository = locationRepository;
            this.ProcessTypeRepository = processTypeRepository;
            this.ApplicationUserRepository = applicationUserRepository;
            this.RoleRepository = roleRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await dbContext.Database.BeginTransactionAsync();
        }
    }
}
