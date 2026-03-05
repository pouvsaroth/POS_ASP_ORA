using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class AddIncome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INCOME_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    COMEDATE = table.Column<DateTime>(type: "DATE", nullable: false),
                    PAYMENTMETHODID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    REMARK = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    INCOMETYPEID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    USERACCESSID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCOME_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "INCOME_TBL");
        }
    }
}
