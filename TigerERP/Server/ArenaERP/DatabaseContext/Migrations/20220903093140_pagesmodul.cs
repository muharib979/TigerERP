using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    public partial class pagesmodul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModulesId",
                table: "Pages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ModulesId",
                table: "Pages",
                column: "ModulesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Modules_ModulesId",
                table: "Pages",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Modules_ModulesId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_ModulesId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ModulesId",
                table: "Pages");
        }
    }
}
