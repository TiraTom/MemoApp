using Microsoft.EntityFrameworkCore.Migrations;

namespace MempApp.Model.Migrations
{
	public partial class AddStartedFlagToEachTask : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				name: "StartedFlag",
				table: "EachTasks",
				nullable: false,
				defaultValue: false);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "StartedFlag",
				table: "EachTasks");
		}
	}
}
