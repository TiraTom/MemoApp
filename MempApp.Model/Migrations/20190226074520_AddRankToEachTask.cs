using Microsoft.EntityFrameworkCore.Migrations;

namespace MempApp.Model.Migrations
{
    public partial class AddRankToEachTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "EachTasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "EachTasks");
        }
    }
}
