using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    public partial class AccChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccChart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowerGroupId = table.Column<int>(type: "int", nullable: true),
                    GroupAccountId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNamed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AliasName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    AccType = table.Column<int>(type: "int", nullable: true),
                    IsSubledger = table.Column<int>(type: "int", nullable: true),
                    IsBillbyBill = table.Column<int>(type: "int", nullable: true),
                    IsInventory = table.Column<int>(type: "int", nullable: true),
                    IsCostCenter = table.Column<int>(type: "int", nullable: true),
                    HeadGroupId = table.Column<int>(type: "int", nullable: true),
                    HeadGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceType = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true),
                    DepriciationRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoteToHeadId = table.Column<int>(type: "int", nullable: true),
                    AccountGroupId = table.Column<int>(type: "int", nullable: true),
                    IsSalesAccount = table.Column<int>(type: "int", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FCAcc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FCAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreditDays = table.Column<int>(type: "int", nullable: true),
                    IsBothBill = table.Column<int>(type: "int", nullable: true),
                    PrintPorder = table.Column<int>(type: "int", nullable: true),
                    DiscountTypeId = table.Column<int>(type: "int", nullable: true),
                    CommissionTypeId = table.Column<int>(type: "int", nullable: true),
                    isFixed = table.Column<int>(type: "int", nullable: true),
                    IsIndependSubledger = table.Column<int>(type: "int", nullable: true),
                    SubLeadeger = table.Column<int>(type: "int", nullable: true),
                    IsEmployee = table.Column<int>(type: "int", nullable: true),
                    TIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccChart", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccChart");
        }
    }
}
