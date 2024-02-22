using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBTL1.Migrations
{
    public partial class fixProvinceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Province",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "District",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Province",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "District",
                newName: "id");
        }
    }
}
