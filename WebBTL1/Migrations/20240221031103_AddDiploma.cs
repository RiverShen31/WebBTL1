using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBTL1.Migrations
{
    public partial class AddDiploma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Commune",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Commune",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Diploma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diploma", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Diploma",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Business Management, University Foundation Program" },
                    { 2, "PG Dip Business and Management (Business Analytics)" },
                    { 3, "Blockchain" },
                    { 4, "Digital Content Marketing" },
                    { 5, "Montessori Classroom Assistant" },
                    { 6, "Business Administration" },
                    { 7, "Psychology" },
                    { 8, "Post Graduate In Management" },
                    { 9, "Mechanical Engineering" },
                    { 10, "Library and Information Science Admission, Colleges, Syllabus, Jobs and Scope" },
                    { 11, "Post Graduate Diploma in Genecology & Obstetrics" },
                    { 12, "Labor Laws and Labor Welfare" },
                    { 13, "Hotel Management & Catering Technology" },
                    { 14, "Journalism and Mass Communication" },
                    { 15, "Computerized Accounting" },
                    { 16, "Construction Management" },
                    { 17, "Hotel Management & Catering Technology" },
                    { 18, "Marine Engineering" },
                    { 19, "Medical Lab Technology" },
                    { 20, "Textile Design" },
                    { 21, "Electrical and Electronics Engineering" },
                    { 22, "Electronics & Communication Engineering" },
                    { 23, "Nutrition & Dietetics" },
                    { 24, "Biotechnology" },
                    { 25, "Radiological Therapy" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diploma");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Commune",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Commune",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
