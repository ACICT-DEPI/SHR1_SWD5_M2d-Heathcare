using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.DAL.Context.Migrations
{
    /// <inheritdoc />
    public partial class updateAdminTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Admins",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Admins",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Admins",
                newName: "Photo");
        }
    }
}
