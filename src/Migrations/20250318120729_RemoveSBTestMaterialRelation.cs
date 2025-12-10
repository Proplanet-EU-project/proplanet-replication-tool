using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProplanetReplicationTool.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSBTestMaterialRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sb_tests_materials_material_id",
                table: "sb_tests");

            migrationBuilder.DropIndex(
                name: "IX_sb_tests_material_id",
                table: "sb_tests");

            migrationBuilder.DropColumn(
                name: "material_id",
                table: "sb_tests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "material_id",
                table: "sb_tests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_sb_tests_material_id",
                table: "sb_tests",
                column: "material_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sb_tests_materials_material_id",
                table: "sb_tests",
                column: "material_id",
                principalTable: "materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
