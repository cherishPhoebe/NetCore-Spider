using AutoMapper;
using ZY.Application.HouseApp.Dtos;
using ZY.Application.HouseApp.ViewModel;
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

            CreateMap<HouseDto, HouseViewModel>();
            CreateMap<HouseViewModel, HouseDto>();
            CreateMap<PerSaleInfoDto, PerSaleInfoViewModel>();
            CreateMap<PerSaleInfoViewModel, PerSaleInfoDto>();
            CreateMap<PriceInfoDto, PriceInfoViewModel>();
            CreateMap<PriceInfoViewModel, PriceInfoDto>();

        }
    }
}
