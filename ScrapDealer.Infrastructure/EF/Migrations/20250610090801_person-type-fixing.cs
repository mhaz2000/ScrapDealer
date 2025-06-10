using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrapDealer.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class persontypefixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Buyers");

            migrationBuilder.AddColumn<int>(
                name: "PersonType",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Sellers");

            migrationBuilder.AddColumn<int>(
                name: "PersonType",
                table: "Buyers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
