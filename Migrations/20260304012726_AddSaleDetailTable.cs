using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SALEDETAIL_TBL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SALEID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PRODUCTID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    QTY = table.Column<decimal>(type: "NUMBER(18,3)", nullable: false),
                    COST = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    PRICE = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    SUBDISCOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALEDETAIL_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SALEDETAIL_TBL");
        }
    }
}
