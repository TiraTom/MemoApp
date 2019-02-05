using Microsoft.EntityFrameworkCore.Migrations;

namespace MempApp.Model.Migrations
{
    public partial class AddEachTaskParentEachTaskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentEachTaskId",
                table: "EachTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentEachTaskId",
                table: "EachTasks");
        }
    }
}
