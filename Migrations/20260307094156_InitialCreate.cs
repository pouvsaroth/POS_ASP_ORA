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

            migrationBuilder.CreateTable(
                name: "CATEGORY_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CATEGORYNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORY_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COMPANY_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    COMPANYNAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    LOCATION = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    REMARK = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCheckin_tbl",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CustomerId = table.Column<decimal>(type: "NUMBER", nullable: false),
                    CheckinDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    status = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCheckin_tbl", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EXPENSETYPE_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TYPENAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXPENSETYPE_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GROUPMEMBER_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GROUPID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    USERID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUPMEMBER_TBL", x => x.ID);
                });

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

            migrationBuilder.CreateTable(
                name: "INCOME_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    COMEDATE = table.Column<DateTime>(type: "DATE", nullable: false),
                    PAYMENTMETHODID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    REMARK = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    INCOMETYPEID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    USERACCESSID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCOME_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IncomeType_tbl",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    typename = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    status = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeType_tbl", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "OBJECT_TBL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OBJECTNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBJECT_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OWNERDRAWING_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DRAWINGDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    FROMACCOUNTID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    DRAWINGAMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false),
                    REMARK = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    USERACCESSID = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OWNERDRAWING_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENTMETHOD_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    METHODNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENTMETHOD_TBL", x => x.ID);
                });

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

            migrationBuilder.CreateTable(
                name: "PURCHASEPAYMENT_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PURCHASEID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYMENTDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    PAYMENTMETHOD = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYAMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASEPAYMENT_TBL", x => x.ID);
                });

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

            migrationBuilder.CreateTable(
                name: "SALEPAYMENT_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SALEID = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYMENTDATE = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    PAYMENTMETHOD = table.Column<decimal>(type: "NUMBER", nullable: false),
                    PAYAMOUNT = table.Column<decimal>(type: "NUMBER(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALEPAYMENT_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SUPPLIER_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SUPPLIERNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SEX = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ADDRESS = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIER_TBL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UNITTYPE_TBL",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UNITTYPENAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    STATUS = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNITTYPE_TBL", x => x.ID);
                });

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
                name: "IX_GROUP_TBL_COMPANYID",
                table: "GROUP_TBL",
                column: "COMPANYID");

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
                name: "CASHTRANSFER_TBL");

            migrationBuilder.DropTable(
                name: "CustomerCheckin_tbl");

            migrationBuilder.DropTable(
                name: "EXPENSETYPE_TBL");

            migrationBuilder.DropTable(
                name: "GROUP_TBL");

            migrationBuilder.DropTable(
                name: "GROUPMEMBER_TBL");

            migrationBuilder.DropTable(
                name: "GROUPOBJECT_TBL");

            migrationBuilder.DropTable(
                name: "INCOME_TBL");

            migrationBuilder.DropTable(
                name: "IncomeType_tbl");

            migrationBuilder.DropTable(
                name: "MoreCapital_tbl");

            migrationBuilder.DropTable(
                name: "OBJECT_TBL");

            migrationBuilder.DropTable(
                name: "OWNERDRAWING_TBL");

            migrationBuilder.DropTable(
                name: "PAYMENTMETHOD_TBL");

            migrationBuilder.DropTable(
                name: "PRODUCT_TBL");

            migrationBuilder.DropTable(
                name: "PURCHASE_TBL");

            migrationBuilder.DropTable(
                name: "PURCHASEPAYMENT_TBL");

            migrationBuilder.DropTable(
                name: "SALE_TBL");

            migrationBuilder.DropTable(
                name: "SALEDETAIL_TBL");

            migrationBuilder.DropTable(
                name: "SALEPAYMENT_TBL");

            migrationBuilder.DropTable(
                name: "UNITTYPE_TBL");

            migrationBuilder.DropTable(
                name: "USERS_TBL");

            migrationBuilder.DropTable(
                name: "COMPANY_TBL");

            migrationBuilder.DropTable(
                name: "CATEGORY_TBL");

            migrationBuilder.DropTable(
                name: "SUPPLIER_TBL");
        }
    }
}
