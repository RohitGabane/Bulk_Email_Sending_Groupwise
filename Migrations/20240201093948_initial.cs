using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulk_Email_Sending_Groupwise.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Dept_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dept_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Dept_Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Emp_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email_ID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Emp_ID);
                });

            migrationBuilder.CreateTable(
                name: "DetailsEmp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emp_ID = table.Column<int>(type: "int", nullable: false),
                    Dept_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsEmp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsEmp_Department_Dept_Id",
                        column: x => x.Dept_Id,
                        principalTable: "Department",
                        principalColumn: "Dept_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailsEmp_Employee_Emp_ID",
                        column: x => x.Emp_ID,
                        principalTable: "Employee",
                        principalColumn: "Emp_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailsEmp_Dept_Id",
                table: "DetailsEmp",
                column: "Dept_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsEmp_Emp_ID",
                table: "DetailsEmp",
                column: "Emp_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailsEmp");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
