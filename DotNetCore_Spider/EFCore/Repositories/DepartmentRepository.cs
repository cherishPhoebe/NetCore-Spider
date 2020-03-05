using ZY.Domain.Entities;
using ZY.Domain.IRepositories;

namespace ZY.EFCore.Repositories
{
    public class DepartmentRepository : ZYRepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ZYContext dbcontext) : base(dbcontext)
        {

        }
    }

}
