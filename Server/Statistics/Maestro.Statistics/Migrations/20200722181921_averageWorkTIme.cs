using Microsoft.EntityFrameworkCore.Migrations;

namespace Maestro.Statistics.Migrations
{
    public partial class averageWorkTIme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AverageWorkTime",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    AverageTime = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AverageWorkTime", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AverageWorkTime");
        }
    }
}
