using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZY.Application.HouseApp.Dtos;

namespace ZY.Application.MenuApp
{
    public interface IHouseAppService
    {
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <returns></returns>
        List<HouseDto> GetAllList();
        
        /// <summary>
        /// 新增或修改功能
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        HouseDto InsertOrUpdate(HouseDto dto);

        /// <summary>
        /// 根据Id集合批量删除
        /// </summary>
        /// <param name="ids">功能Id集合</param>
        void DeleteBatch(List<Guid> ids);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">功能Id</param>
        void Delete(Guid id);

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">功能Id</param>
        /// <returns></returns>
        HouseDto Get(Guid id);

        Task<List<HouseDto>> GetHouseData();

    }
}
