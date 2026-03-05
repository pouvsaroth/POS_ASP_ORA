using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class CreateCashTransferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CASHTRANSFER_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TRANSFERDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    FROMACCOUNTID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    TOACCOUNTID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    REMARK = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    USERACCESSID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CASHTRANSFER_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CASHTRANSFER_TBL");
        }
    }
}
