﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VirtualDeviceTestingManage.Dal;

namespace VirtualDeviceTestingManage.Dal.Migrations
{
    [DbContext(typeof(VirtualDeviceManageContext))]
    partial class VirtualDeviceManageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VirtualDeviceTestingManage.Domain.CommProtocol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CommProtocol");
                });

            modelBuilder.Entity("VirtualDeviceTestingManage.Domain.DeviceBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceBrand");
                });

            modelBuilder.Entity("VirtualDeviceTestingManage.Domain.DeviceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CommProtocolId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parameters")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CommProtocolId");

                    b.ToTable("DeviceModel");
                });

            modelBuilder.Entity("VirtualDeviceTestingManage.Domain.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceType");
                });

            modelBuilder.Entity("VirtualDeviceTestingManage.Domain.VirtualNetworkDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AddDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BrandModelId")
                        .HasColumnType("int");

                    b.Property<string>("CommProtocol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DeviceTypeId")
                        .HasColumnType("int");

                    b.Property<string>("IpAddrees")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandModelId");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("VirtualDevices");
                });

            modelBuilder.Entity("VirtualDeviceTestingManage.Domain.DeviceModel", b =>
                {
                    b.HasOne("VirtualDeviceTestingManage.Domain.DeviceBrand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");

                    b.HasOne("VirtualDeviceTestingManage.Domain.CommProtocol", "CommProtocol")
                        .WithMany()
                        .HasForeignKey("CommProtocolId");
                });

            modelBuilder.Entity("VirtualDeviceTestingManage.Domain.VirtualNetworkDevice", b =>
                {
                    b.HasOne("VirtualDeviceTestingManage.Domain.DeviceModel", "BrandModel")
                        .WithMany()
                        .HasForeignKey("BrandModelId");

                    b.HasOne("VirtualDeviceTestingManage.Domain.DeviceType", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
