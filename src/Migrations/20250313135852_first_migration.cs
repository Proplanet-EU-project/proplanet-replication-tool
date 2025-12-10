using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProplanetReplicationTool.Migrations
{
    /// <inheritdoc />
    public partial class first_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    formula = table.Column<string>(type: "text", nullable: false),
                    cas = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    total_carbon_atoms = table.Column<int>(type: "integer", nullable: false),
                    molecular_weight = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ecosystem_toxicities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    aquatic_freshwater = table.Column<float>(type: "real", nullable: false),
                    aquatic_marinewater = table.Column<float>(type: "real", nullable: false),
                    aquatic_stp = table.Column<float>(type: "real", nullable: false),
                    aquatic_sediment_freshwater = table.Column<float>(type: "real", nullable: false),
                    aquatic_sediment_marinewater = table.Column<float>(type: "real", nullable: false),
                    air = table.Column<float>(type: "real", nullable: false),
                    terrestrial_soil = table.Column<float>(type: "real", nullable: false),
                    predators_oral_poisoning = table.Column<float>(type: "real", nullable: false),
                    material_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ecosystem_toxicities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ecosystem_toxicities_materials_material_id",
                        column: x => x.material_id,
                        principalTable: "materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "human_toxicities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    w_inhalation_systemic_long = table.Column<float>(type: "real", nullable: false),
                    w_inhalation_systemic_short = table.Column<float>(type: "real", nullable: false),
                    w_inhalation_local_long = table.Column<float>(type: "real", nullable: false),
                    w_inhalation_local_short = table.Column<float>(type: "real", nullable: false),
                    w_dermal_systemic_long = table.Column<float>(type: "real", nullable: false),
                    w_dermal_systemic_short = table.Column<float>(type: "real", nullable: false),
                    w_dermal_local_long = table.Column<float>(type: "real", nullable: false),
                    w_dermal_local_short = table.Column<float>(type: "real", nullable: false),
                    w_eyes_local = table.Column<float>(type: "real", nullable: false),
                    p_inhalation_systemic_long = table.Column<float>(type: "real", nullable: false),
                    p_inhalation_systemic_short = table.Column<float>(type: "real", nullable: false),
                    p_inhalation_local_long = table.Column<float>(type: "real", nullable: false),
                    p_inhalation_local_short = table.Column<float>(type: "real", nullable: false),
                    p_dermal_systemic_long = table.Column<float>(type: "real", nullable: false),
                    p_dermal_systemic_short = table.Column<float>(type: "real", nullable: false),
                    p_dermal_local_long = table.Column<float>(type: "real", nullable: false),
                    p_dermal_local_short = table.Column<float>(type: "real", nullable: false),
                    p_eyes_local = table.Column<float>(type: "real", nullable: false),
                    p_oral_systemic_long = table.Column<float>(type: "real", nullable: false),
                    p_oral_systemic_short = table.Column<float>(type: "real", nullable: false),
                    material_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_human_toxicities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_human_toxicities_materials_material_id",
                        column: x => x.material_id,
                        principalTable: "materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rina_tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    log_pow = table.Column<float>(type: "real", nullable: false),
                    log_kow = table.Column<float>(type: "real", nullable: false),
                    lumo_homo = table.Column<float>(type: "real", nullable: false),
                    distance = table.Column<float>(type: "real", nullable: false),
                    interaction_energy = table.Column<float>(type: "real", nullable: false),
                    wca = table.Column<float>(type: "real", nullable: false),
                    toxicity = table.Column<float>(type: "real", nullable: false),
                    material_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rina_tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rina_tests_materials_material_id",
                        column: x => x.material_id,
                        principalTable: "materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sb_tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    result = table.Column<string>(type: "jsonb", nullable: false),
                    material_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sb_tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sb_tests_materials_material_id",
                        column: x => x.material_id,
                        principalTable: "materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sb_tests_inputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    scenario_name = table.Column<string>(type: "text", nullable: false),
                    nanomaterial_name = table.Column<string>(type: "text", nullable: false),
                    molecular_weight = table.Column<float>(type: "real", nullable: false),
                    regional_emission_air = table.Column<float>(type: "real", nullable: false),
                    regional_emission_water0 = table.Column<float>(type: "real", nullable: false),
                    regional_emission_water1 = table.Column<float>(type: "real", nullable: false),
                    regional_emission_water2 = table.Column<float>(type: "real", nullable: false),
                    regional_emission_natural_soil = table.Column<float>(type: "real", nullable: false),
                    regional_emission_agricultural_soil = table.Column<float>(type: "real", nullable: false),
                    regional_emission_other_soil = table.Column<float>(type: "real", nullable: false),
                    continental_emission_air = table.Column<float>(type: "real", nullable: false),
                    continental_emission_water0 = table.Column<float>(type: "real", nullable: false),
                    continental_emission_water1 = table.Column<float>(type: "real", nullable: false),
                    continental_emission_water2 = table.Column<float>(type: "real", nullable: false),
                    continental_emission_natural_soil = table.Column<float>(type: "real", nullable: false),
                    continental_emission_agricultural_soil = table.Column<float>(type: "real", nullable: false),
                    continental_emission_other_soil = table.Column<float>(type: "real", nullable: false),
                    artic_emission_air = table.Column<float>(type: "real", nullable: false),
                    artic_emission_water2 = table.Column<float>(type: "real", nullable: false),
                    artic_emission_soil = table.Column<float>(type: "real", nullable: false),
                    tropical_emission_air = table.Column<float>(type: "real", nullable: false),
                    tropical_emission_water2 = table.Column<float>(type: "real", nullable: false),
                    tropical_emission_soil = table.Column<float>(type: "real", nullable: false),
                    moderate_emission_air = table.Column<float>(type: "real", nullable: false),
                    moderate_emission_water2 = table.Column<float>(type: "real", nullable: false),
                    moderate_emission_soil = table.Column<float>(type: "real", nullable: false),
                    sb_test_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sb_tests_inputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sb_tests_inputs_sb_tests_sb_test_id",
                        column: x => x.sb_test_id,
                        principalTable: "sb_tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ecosystem_toxicities_material_id",
                table: "ecosystem_toxicities",
                column: "material_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_human_toxicities_material_id",
                table: "human_toxicities",
                column: "material_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rina_tests_material_id",
                table: "rina_tests",
                column: "material_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sb_tests_material_id",
                table: "sb_tests",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_sb_tests_name",
                table: "sb_tests",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sb_tests_inputs_sb_test_id",
                table: "sb_tests_inputs",
                column: "sb_test_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ecosystem_toxicities");

            migrationBuilder.DropTable(
                name: "human_toxicities");

            migrationBuilder.DropTable(
                name: "rina_tests");

            migrationBuilder.DropTable(
                name: "sb_tests_inputs");

            migrationBuilder.DropTable(
                name: "sb_tests");

            migrationBuilder.DropTable(
                name: "materials");
        }
    }
}
