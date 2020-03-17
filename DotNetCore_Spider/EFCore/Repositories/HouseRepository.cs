using ZY.Domain.Entities;
using ZY.Domain.IRepositories;

namespace ZY.EFCore.Repositories
{
    /// <summary>
    /// 用户管理仓储实现
    /// </summary>
    public class HouseRepository : ZYRepositoryBase<House>, IHouseRepository
    {
        public HouseRepository(ZYContext dbcontext) : base(dbcontext)
        {

        }
    }

}
