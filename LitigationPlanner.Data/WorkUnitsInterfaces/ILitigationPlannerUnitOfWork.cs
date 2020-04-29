using LitigationPlanner.Data.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace LitigationPlanner.Data.WorkUnitsInterfaces
{
    public interface ILitigationPlannerUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; set; }
        IContactRepository ContactRepository { get; set; }
        ILitigationRepository LitigationRepository { get; set; }
        ILocationRepository LocationRepository { get; set; }
        IProcessTypeRepository ProcessTypeRepository { get; set; }
        IApplicationUserRepository ApplicationUserRepository { get; set; }
        IRoleRepository RoleRepository { get; set; }
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
