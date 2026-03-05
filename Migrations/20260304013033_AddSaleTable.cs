using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SALE_TBL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    INVOICENO = table.Column<decimal>(type: "NUMBER", nullable: false),
                    SALEDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CUSTOMERID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    TOTALAMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    DISCOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    USERACCESSID = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALE_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SALE_TBL");
        }
    }
}
