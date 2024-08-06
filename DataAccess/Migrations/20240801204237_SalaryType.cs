using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SalaryType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Jobs");

            migrationBuilder.AddColumn<decimal>(
                name: "ExactSalary",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxSalary",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinSalary",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "SalaryType",
                table: "Jobs",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExactSalary",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "MaxSalary",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "MinSalary",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "SalaryType",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
