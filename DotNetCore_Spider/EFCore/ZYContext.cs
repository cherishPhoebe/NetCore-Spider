﻿using Microsoft.EntityFrameworkCore;
using ZY.Domain.Entities;

namespace ZY.EFCore
{
    public class ZYContext : DbContext
    {
        public ZYContext(DbContextOptions<ZYContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<House> Houses { get; set; }
        public DbSet<PerSaleInfo> PerSaleInfos { get; set; }
        public DbSet<PriceInfo> PriceInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //UserRole关联配置
            builder.Entity<UserRole>()
              .HasKey(ur => new { ur.UserId, ur.RoleId });

            //RoleMenu关联配置
            builder.Entity<RoleMenu>()
              .HasKey(rm => new { rm.RoleId, rm.MenuId });


            base.OnModelCreating(builder);
        }

    }
}
