using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class SalePaymentCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SALEPAYMENT_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SALEID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYMENTDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    PAYMENTMETHOD = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYAMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALEPAYMENT_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SALEPAYMENT_TBL");
        }
    }
}
