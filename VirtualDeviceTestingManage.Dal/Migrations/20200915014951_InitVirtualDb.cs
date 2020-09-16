using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualDeviceTestingManage.Dal.Migrations
{
    public partial class InitVirtualDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferMode",
                table: "VirtualDevices");

            migrationBuilder.AddColumn<string>(
                name: "CommProtocol",
                table: "VirtualDevices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommProtocol",
                table: "VirtualDevices");

            migrationBuilder.AddColumn<string>(
                name: "TransferMode",
                table: "VirtualDevices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
