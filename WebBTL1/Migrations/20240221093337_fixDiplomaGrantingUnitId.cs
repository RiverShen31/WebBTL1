using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBTL1.Migrations
{
    public partial class fixDiplomaGrantingUnitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardDiplomas_Province_DiplomaGrantingUnitId",
                table: "AwardDiplomas");

            migrationBuilder.DropColumn(
                name: "DiplomaGratingUnitId",
                table: "AwardDiplomas");

            migrationBuilder.AlterColumn<int>(
                name: "DiplomaGrantingUnitId",
                table: "AwardDiplomas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AwardDiplomas_Province_DiplomaGrantingUnitId",
                table: "AwardDiplomas",
                column: "DiplomaGrantingUnitId",
                principalTable: "Province",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AwardDiplomas_Province_DiplomaGrantingUnitId",
                table: "AwardDiplomas");

            migrationBuilder.AlterColumn<int>(
                name: "DiplomaGrantingUnitId",
                table: "AwardDiplomas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DiplomaGratingUnitId",
                table: "AwardDiplomas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AwardDiplomas_Province_DiplomaGrantingUnitId",
                table: "AwardDiplomas",
                column: "DiplomaGrantingUnitId",
                principalTable: "Province",
                principalColumn: "id");
        }
    }
}
