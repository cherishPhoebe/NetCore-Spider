﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZY.EFCore;

namespace ZY.EFCore.Migrations
{
    [DbContext(typeof(ZYContext))]
    [Migration("20200317133013_house_model")]
    partial class house_model
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZY.Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("Manager");

                    b.Property<string>("Name");

                    b.Property<Guid>("ParentId");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ZY.Domain.Entities.House", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BaseInfoJson");

                    b.Property<string>("BuildingType");

                    b.Property<string>("HomeUrl");

                    b.Property<string>("HouseKey");

                    b.Property<string>("Name");

                    b.Property<string>("Point");

                    b.Property<string>("Price");

                    b.Property<string>("RoomType");

                    b.Property<string>("SaseInfoJson");

                    b.Property<string>("Tel");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("ZY.Domain.Entities.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.Property<Guid>("ParentId");

                    b.Property<string>("Remarks");

                    b.Property<Guid?>("RoleId");

                    b.Property<int>("SerialNumber");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("ZY.Domain.Entities.PerSaleInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BindBuilding");

                    b.Property<Guid>("HouseId");

                    b.Property<string>("IssueDate");

                    b.Property<string>("License");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("PerSaleInfos");
                });

            modelBuilder.Entity("ZY.Domain.Entities.PriceInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvgPrice");

                    b.Property<Guid>("HouseId");

                    b.Property<string>("PriceDescription");

                    b.Property<string>("RecordDate");

                    b.Property<string>("StartingPrice");

                    b.HasKey("Id");

                    b.HasIndex("HouseId");

                    b.ToTable("PriceInfos");
                });

            modelBuilder.Entity("ZY.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<string>("Name");

                    b.Property<string>("Remarks");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ZY.Domain.Entities.RoleMenu", b =>
                {
                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("MenuId");

                    b.HasKey("RoleId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("ZY.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<Guid>("DepartmentId");

                    b.Property<string>("EMail");

                    b.Property<int>("IsDeleted");

                    b.Property<DateTime>("LastLoginTime");

                    b.Property<int>("LoginTimes");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Remarks");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ZY.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ZY.Domain.Entities.Menu", b =>
                {
                    b.HasOne("ZY.Domain.Entities.Role")
                        .WithMany("Menus")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("ZY.Domain.Entities.PerSaleInfo", b =>
                {
                    b.HasOne("ZY.Domain.Entities.House")
                        .WithMany("PerSaleList")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZY.Domain.Entities.PriceInfo", b =>
                {
                    b.HasOne("ZY.Domain.Entities.House")
                        .WithMany("PriceList")
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZY.Domain.Entities.Role", b =>
                {
                    b.HasOne("ZY.Domain.Entities.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ZY.Domain.Entities.RoleMenu", b =>
                {
                    b.HasOne("ZY.Domain.Entities.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZY.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZY.Domain.Entities.User", b =>
                {
                    b.HasOne("ZY.Domain.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZY.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("ZY.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ZY.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
