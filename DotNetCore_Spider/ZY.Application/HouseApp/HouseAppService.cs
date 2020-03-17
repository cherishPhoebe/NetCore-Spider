using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ZY.Application.HouseApp.Dtos;
using ZY.Domain.Entities;
using ZY.Domain.IRepositories;

namespace ZY.Application.MenuApp
{
    public class HouseAppService : IHouseAppService
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IMapper _mapper;
        public HouseAppService(IHouseRepository houseRepository,IMapper mapper)
        {
            _houseRepository = houseRepository;
            _mapper = mapper;
        }

        public List<HouseDto> GetAllList()
        {
            var menus = _houseRepository.GetAllList().OrderBy(it => it.UpdateTime);
            //使用AutoMapper进行实体转换
            return _mapper.Map<List<HouseDto>>(menus);
        }


        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public HouseDto InsertOrUpdate(HouseDto dto)
        {
            if (Get(dto.Id) != null)
                _houseRepository.Delete(dto.Id);
            var house = _houseRepository.InsertOrUpdate(_mapper.Map<House>(dto));
            return _mapper.Map<HouseDto>(house);
        }

        public void DeleteBatch(List<Guid> ids)
        {
            _houseRepository.Delete(it => ids.Contains(it.Id));
        }

        public void Delete(Guid id)
        {
            _houseRepository.Delete(id);
        }

        public HouseDto Get(Guid id)
        {
            return _mapper.Map<HouseDto>(_houseRepository.Get(id));
        }
    }
}
