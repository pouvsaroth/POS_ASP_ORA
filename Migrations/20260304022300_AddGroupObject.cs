using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GROUPOBJECT_TBL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GROUPID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    OBJECTID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUPOBJECT_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GROUPOBJECT_TBL");
        }
    }
}
