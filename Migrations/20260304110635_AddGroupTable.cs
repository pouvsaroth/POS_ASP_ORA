using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GROUP_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GROUPNAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    REMARK = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    COMPANYID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUP_TBL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GROUP_TBL_COMPANY_TBL_COMPANYID",
                        column: x => x.COMPANYID,
                        principalTable: "COMPANY_TBL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GROUP_TBL_COMPANYID",
                table: "GROUP_TBL",
                column: "COMPANYID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GROUP_TBL");
        }
    }
}
