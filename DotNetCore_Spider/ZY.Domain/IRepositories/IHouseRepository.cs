using System;
using ZY.Domain.Entities;

namespace ZY.Domain.IRepositories
{
    /// <summary>
    /// 用户管理仓储接口
    /// </summary>
    public interface IHouseRepository : IRepository<House>
    {
        House Get(string houseKey);

        House InsertOrUpdateByHouseKey(House house,bool autoSave = true);
    }
}
