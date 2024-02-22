using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBTL1.Migrations
{
    public partial class Set_AwardDiplomaViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardDiplomas_Diplomas_DiplomaId",
                table: "AwardDiplomas");

            migrationBuilder.DropForeignKey(
                name: "FK_AwardDiplomas_Employees_EmployeeId",
                table: "AwardDiplomas");

            migrationBuilder.DropForeignKey(
                name: "FK_AwardDiplomas_Province_DiplomaGrantingUnitId",
                table: "AwardDiplomas");

            migrationBuilder.DropIndex(
                name: "IX_AwardDiplomas_DiplomaGrantingUnitId",
                table: "AwardDiplomas");

            migrationBuilder.DropIndex(
                name: "IX_AwardDiplomas_DiplomaId",
                table: "AwardDiplomas");

            migrationBuilder.DropIndex(
                name: "IX_AwardDiplomas_EmployeeId",
                table: "AwardDiplomas");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "AwardDiplomas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AwardDiplomas_ProvinceId",
                table: "AwardDiplomas",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AwardDiplomas_Province_ProvinceId",
                table: "AwardDiplomas",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardDiplomas_Province_ProvinceId",
                table: "AwardDiplomas");

            migrationBuilder.DropIndex(
                name: "IX_AwardDiplomas_ProvinceId",
                table: "AwardDiplomas");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "AwardDiplomas");

            migrationBuilder.CreateIndex(
                name: "IX_AwardDiplomas_DiplomaGrantingUnitId",
                table: "AwardDiplomas",
                column: "DiplomaGrantingUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardDiplomas_DiplomaId",
                table: "AwardDiplomas",
                column: "DiplomaId");

            migrationBuilder.CreateIndex(
                name: "IX_AwardDiplomas_EmployeeId",
                table: "AwardDiplomas",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AwardDiplomas_Diplomas_DiplomaId",
                table: "AwardDiplomas",
                column: "DiplomaId",
                principalTable: "Diplomas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AwardDiplomas_Employees_EmployeeId",
                table: "AwardDiplomas",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AwardDiplomas_Province_DiplomaGrantingUnitId",
                table: "AwardDiplomas",
                column: "DiplomaGrantingUnitId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
