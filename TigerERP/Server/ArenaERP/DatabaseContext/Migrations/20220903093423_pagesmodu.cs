using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    public partial class pagesmodu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Modules_ModulesId",
                table: "Pages");

            migrationBuilder.AlterColumn<int>(
                name: "ModulesId",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Modules_ModulesId",
                table: "Pages",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Modules_ModulesId",
                table: "Pages");

            migrationBuilder.AlterColumn<int>(
                name: "ModulesId",
                table: "Pages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Modules_ModulesId",
                table: "Pages",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id");
        }
    }
}
