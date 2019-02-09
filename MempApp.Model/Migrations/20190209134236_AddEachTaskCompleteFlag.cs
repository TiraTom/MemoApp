using Microsoft.EntityFrameworkCore.Migrations;

namespace MempApp.Model.Migrations
{
    public partial class AddEachTaskCompleteFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompleteFlag",
                table: "EachTasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteFlag",
                table: "EachTasks");
        }
    }
}
