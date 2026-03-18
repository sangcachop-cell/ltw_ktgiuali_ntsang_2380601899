using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenTanSang_1899.Migrations
{
    /// <inheritdoc />
    public partial class AddSoLuongToHocPhan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "HocPhans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ChiTietDangKys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "HocPhans");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChiTietDangKys");
        }
    }
}
