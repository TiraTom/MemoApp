using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MempApp.Model.Migrations
{
    public partial class DeleteHashItemAndModifyOnetoManySetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HashItems",
                columns: table => new
                {
                    HashItemId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UsedTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashItems", x => x.HashItemId);
                });
        }
    }
}
