using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class CreateGroundTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GROUND_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GROUPNAME = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: false),
                    REMARK = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    COMPANYID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUND_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GROUND_TBL");
        }
    }
}
