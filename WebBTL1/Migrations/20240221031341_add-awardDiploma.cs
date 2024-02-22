using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBTL1.Migrations
{
    public partial class addawardDiploma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Diploma",
                table: "Diploma");

            migrationBuilder.RenameTable(
                name: "Diploma",
                newName: "Diplomas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diplomas",
                table: "Diplomas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AwardDiplomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DiplomaId = table.Column<int>(type: "int", nullable: false),
                    DiplomaGratingUnitId = table.Column<int>(type: "int", nullable: false),
                    DiplomaGrantingUnitId = table.Column<int>(type: "int", nullable: true),
                    GrantingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwardDiplomas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AwardDiplomas_Diplomas_DiplomaId",
                        column: x => x.DiplomaId,
                        principalTable: "Diplomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwardDiplomas_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AwardDiplomas_Province_DiplomaGrantingUnitId",
                        column: x => x.DiplomaGrantingUnitId,
                        principalTable: "Province",
                        principalColumn: "id");
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwardDiplomas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diplomas",
                table: "Diplomas");

            migrationBuilder.RenameTable(
                name: "Diplomas",
                newName: "Diploma");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diploma",
                table: "Diploma",
                column: "Id");
        }
    }
}
