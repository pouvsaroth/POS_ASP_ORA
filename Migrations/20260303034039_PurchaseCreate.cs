using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class PurchaseCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PURCHASE_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BILLNO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PURCHASEDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    SUPPLIERID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    TOTALAMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    DISCOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    USERACCESSID = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASE_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PURCHASE_TBL");
        }
    }
}
