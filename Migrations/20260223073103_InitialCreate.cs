using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_ASP_ORA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS_TBL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false, defaultValueSql: "SYS_GUID()"),
                    USERNAME = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE"),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_TBL", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS_TBL");
        }
    }
}
