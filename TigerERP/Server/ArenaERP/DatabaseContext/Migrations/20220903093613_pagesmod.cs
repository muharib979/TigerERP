using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    public partial class pagesmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModulesId",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ModulesId",
                table: "Pages",
                column: "ModulesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Modules_ModulesId",
                table: "Pages",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
