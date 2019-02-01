using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MempApp.Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EachTasks",
                columns: table => new
                {
                    EachTaskId = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    DeadLine = table.Column<DateTime>(nullable: false),
                    PlanDate = table.Column<DateTime>(nullable: false),
                    RegisteredDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EachTasks", x => x.EachTaskId);
                });

            migrationBuilder.CreateTable(
                name: "HashItems",
                columns: table => new
                {
                    HashItemId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UsedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashItems", x => x.HashItemId);
                });

            migrationBuilder.CreateTable(
                name: "Memos",
                columns: table => new
                {
                    MemoId = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    EachTaskId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memos", x => x.MemoId);
                    table.ForeignKey(
                        name: "FK_Memos_EachTasks_EachTaskId",
                        column: x => x.EachTaskId,
                        principalTable: "EachTasks",
                        principalColumn: "EachTaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeInfos",
                columns: table => new
                {
                    TimeInfoId = table.Column<string>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Stop = table.Column<DateTime>(nullable: false),
                    EachTaskId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeInfos", x => x.TimeInfoId);
                    table.ForeignKey(
                        name: "FK_TimeInfos_EachTasks_EachTaskId",
                        column: x => x.EachTaskId,
                        principalTable: "EachTasks",
                        principalColumn: "EachTaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memos_EachTaskId",
                table: "Memos",
                column: "EachTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeInfos_EachTaskId",
                table: "TimeInfos",
                column: "EachTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashItems");

            migrationBuilder.DropTable(
                name: "Memos");

            migrationBuilder.DropTable(
                name: "TimeInfos");

            migrationBuilder.DropTable(
                name: "EachTasks");
        }
    }
}
