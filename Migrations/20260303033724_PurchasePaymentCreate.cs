using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class PurchasePaymentCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PURCHASEPAYMENT_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PURCHASEID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYMENTDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    PAYMENTMETHOD = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYAMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASEPAYMENT_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PURCHASEPAYMENT_TBL");
        }
    }
}
