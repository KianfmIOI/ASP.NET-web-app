using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class staffSSN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocialSecurity",
                table: "Staffs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialSecurity",
                table: "Staffs");
        }
    }
}
