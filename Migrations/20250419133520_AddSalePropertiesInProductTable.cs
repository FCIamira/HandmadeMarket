using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeMarket.Migrations
{
    /// <inheritdoc />
    public partial class AddSalePropertiesInProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSale",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");
        }
    }
}
