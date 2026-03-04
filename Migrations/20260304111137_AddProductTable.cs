using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUCT_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PRODUCTCODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    BARCODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PRODUCTNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PRODUCTNAMEKH = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CATEGORYID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    SUPPLIERID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    QTYONHAND = table.Column<decimal>(type: "NUMBER(18,6)", nullable: true),
                    QTYALERT = table.Column<decimal>(type: "NUMBER", nullable: true),
                    IMAGE = table.Column<byte[]>(type: "BLOB", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    STATUS = table.Column<int>(type: "NUMBER(1)", nullable: true),
                    USERACCESSID = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_TBL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_TBL_CATEGORY_TBL_CATEGORYID",
                        column: x => x.CATEGORYID,
                        principalTable: "CATEGORY_TBL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUCT_TBL_SUPPLIER_TBL_SUPPLIERID",
                        column: x => x.SUPPLIERID,
                        principalTable: "SUPPLIER_TBL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_TBL_CATEGORYID",
                table: "PRODUCT_TBL",
                column: "CATEGORYID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_TBL_SUPPLIERID",
                table: "PRODUCT_TBL",
                column: "SUPPLIERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCT_TBL");
        }
    }
}
