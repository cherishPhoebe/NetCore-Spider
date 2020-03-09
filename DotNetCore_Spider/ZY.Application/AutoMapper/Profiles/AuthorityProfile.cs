using AutoMapper;
using ZY.Application.DepartmentApp.Dtos;
using ZY.Application.MenuApp.Dtos;
using ZY.Application.RoleApp.Dtos;
using ZY.Application.UserApp.Dtos;
using ZY.Domain.Entities;

namespace ZY.Application.AutoMapper.Profiles
{
    public class AuthorityProfile : Profile, IProfile
    {
        public AuthorityProfile()
        {
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleMenuDto, RoleMenu>();
            CreateMap<RoleMenu, RoleMenuDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserRoleDto, UserRole>();
            CreateMap<UserRole, UserRoleDto>();

        }
    }
}
