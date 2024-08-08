using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningLINQWithSQL.Migrations
{
    /// <inheritdoc />
    public partial class salaryamountAdditioninSalarytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryAmount",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryAmount",
                table: "Salaries");
        }
    }
}
