using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using VirtualDeviceTestingManage.Domain;

namespace VirtualDeviceTestingManage.Dal
{
    public class VirtualDeviceManageContext:DbContext
    {
        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information)
                   .AddConsole();
            });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                connectionString: "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=VirtualDeviceManageDB");
        }

        public DbSet<VirtualNetworkDevice> VirtualDevices { get; set; }
    }
}
