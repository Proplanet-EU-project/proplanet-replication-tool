using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProplanetReplicationTool.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexOnSBTestNameAndAddFingerprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sb_tests_name",
                table: "sb_tests");

            migrationBuilder.AddColumn<string>(
                name: "fingerprint",
                table: "sb_tests_inputs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fingerprint",
                table: "sb_tests_inputs");

            migrationBuilder.CreateIndex(
                name: "IX_sb_tests_name",
                table: "sb_tests",
                column: "name",
                unique: true);
        }
    }
}
