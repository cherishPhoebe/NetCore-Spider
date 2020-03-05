using ZY.Domain.Entities;
using ZY.Domain.IRepositories;

namespace ZY.EFCore.Repositories
{
    public class MenuRepository : ZYRepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(ZYContext dbcontext) : base(dbcontext)
        {

        }
    }

}
