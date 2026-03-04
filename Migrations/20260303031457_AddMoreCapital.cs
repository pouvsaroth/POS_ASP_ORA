using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCapital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoreCapital_tbl",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    moredate = table.Column<DateTime>(type: "DATE", nullable: false),
                    toaccountid = table.Column<decimal>(type: "NUMBER", nullable: false),
                    amount = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    remark = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    useraccessid = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoreCapital_tbl", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoreCapital_tbl");
        }
    }
}
