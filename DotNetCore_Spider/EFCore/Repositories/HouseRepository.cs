using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public House Get(string houseKey)
        {
            var house = _dbContext.Set<House>().Where(it => it.HouseKey == houseKey).Include(h => h.PerSaleList).Include(h => h.PriceList).FirstOrDefault();
            return house;
        }

        public House InsertOrUpdateByHouseKey(House house,bool autoSave = true)
        {
            if (Get(house.HouseKey) != null)
                Delete(house);
            return Insert(house, autoSave);
        }

    }

}
