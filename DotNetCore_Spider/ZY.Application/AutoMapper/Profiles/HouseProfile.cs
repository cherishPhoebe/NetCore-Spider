using AutoMapper;
using ZY.Application.DepartmentApp.Dtos;
using ZY.Application.HouseApp.Dtos;
using ZY.Application.MenuApp.Dtos;
using ZY.Application.RoleApp.Dtos;
using ZY.Application.UserApp.Dtos;
using ZY.Domain.Entities;

namespace ZY.Application.AutoMapper.Profiles
{
    public class HouseProfile : Profile, IProfile
    {
        public HouseProfile()
        {
            //CreateMap<Post, PostResource>().ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.LastModified));
            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();
            CreateMap<PerSaleInfo, PerSaleInfoDto>();
            CreateMap<PerSaleInfoDto, PerSaleInfo>();
            CreateMap<PriceInfo, PriceInfoDto>();
            CreateMap<PriceInfoDto, PriceInfo>();

        }
    }
}
