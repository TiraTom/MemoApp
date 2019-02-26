using Microsoft.EntityFrameworkCore.Migrations;

namespace MempApp.Model.Migrations
{
    public partial class AddValidFlagToEachTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ValidFlag",
                table: "EachTasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidFlag",
                table: "EachTasks");
        }
    }
}
