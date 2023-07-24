using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    public partial class challann : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallanDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyId = table.Column<int>(type: "int", nullable: true),
                    ChallanId = table.Column<int>(type: "int", nullable: true),
                    ChallanNo = table.Column<int>(type: "int", nullable: true),
                    ChallanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallanDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    UnitQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PcsQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UniqueQty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    GodownId = table.Column<int>(type: "int", nullable: true),
                    ChallanNatureId = table.Column<int>(type: "int", nullable: true),
                    TransId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ProductionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UniquePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RefAutoId = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationGownId = table.Column<int>(type: "int", nullable: true),
                    BoxConv = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Factor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QuoteId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallanDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChallanMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallanId = table.Column<int>(type: "int", nullable: false),
                    ChallanNo = table.Column<int>(type: "int", nullable: false),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    SalesPersonId = table.Column<int>(type: "int", nullable: true),
                    PartyId = table.Column<int>(type: "int", nullable: true),
                    NatureId = table.Column<int>(type: "int", nullable: true),
                    RefId = table.Column<int>(type: "int", nullable: true),
                    TransportId = table.Column<int>(type: "int", nullable: true),
                    DeliveryLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransPortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallanMaster", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallanDetail");

            migrationBuilder.DropTable(
                name: "ChallanMaster");
        }
    }
}
