using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewEntitesIdentityAndTypeFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OverallDesignParameters");

            migrationBuilder.CreateTable(
                name: "Blades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    length = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    width = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    thickness = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    total_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    angle_attack = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    voltage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    current = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    capacity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptualDesign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    design_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    payload_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FlightTimeRequirementInMinutes = table.Column<double>(type: "double precision", nullable: false),
                    power_required = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    battery_capacity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    battery_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    motor_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    horsepower = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptualDesign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuselages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuselages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inverters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    voltage_rating = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    current_rating = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    total_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    power_to_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inverters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasureandQuantity",
                columns: table => new
                {
                    Unit = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "MissionParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    calculated_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    payload_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FlightTimeRequirementInMinutes = table.Column<double>(type: "double precision", nullable: false),
                    calculated_power_required = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    calculated_battery_capacity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    calculated_battery_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    calculated_motor_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Calculated_horsepower_required = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    voltage_rating = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    current_rating = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    total_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Rpm = table.Column<double>(type: "double precision", maxLength: 15, nullable: false),
                    Kv = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    power_to_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatteryModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CellId = table.Column<Guid>(type: "uuid", nullable: false),
                    cellc_connected_series = table.Column<int>(type: "integer", nullable: false),
                    cellc_connected_parallel = table.Column<int>(type: "integer", nullable: false),
                    capacity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    voltage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    current = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    power = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatteryModules_Cells_CellId",
                        column: x => x.CellId,
                        principalTable: "Cells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatteryPacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    capacity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    voltage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    current = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    power = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    modules_connected_series = table.Column<int>(type: "integer", nullable: false),
                    modules_connected_parallel = table.Column<int>(type: "integer", nullable: false),
                    miscellaneous_weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    specific_energy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    weight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatteryPacks_BatteryModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "BatteryModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectricVtolDesigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BatteryPackId = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InverterId = table.Column<Guid>(type: "uuid", nullable: false),
                    BladeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FuselageId = table.Column<Guid>(type: "uuid", nullable: false),
                    MissionParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MotorQuantity = table.Column<int>(type: "integer", nullable: false),
                    BladePerMotorQuantity = table.Column<int>(type: "integer", nullable: false),
                    FlightTimeInMinutes = table.Column<double>(type: "double precision", nullable: false),
                    PayloadWeight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ThrustArea = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LiftOffWeight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DiscLoading = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PowerLoading = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Thrust = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricVtolDesigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectricVtolDesigns_BatteryPacks_BatteryPackId",
                        column: x => x.BatteryPackId,
                        principalTable: "BatteryPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectricVtolDesigns_Blades_BladeId",
                        column: x => x.BladeId,
                        principalTable: "Blades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectricVtolDesigns_Fuselages_FuselageId",
                        column: x => x.FuselageId,
                        principalTable: "Fuselages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectricVtolDesigns_Inverters_InverterId",
                        column: x => x.InverterId,
                        principalTable: "Inverters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectricVtolDesigns_MissionParameters_MissionParameterId",
                        column: x => x.MissionParameterId,
                        principalTable: "MissionParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectricVtolDesigns_Motors_MotorId",
                        column: x => x.MotorId,
                        principalTable: "Motors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatteryModules_CellId",
                table: "BatteryModules",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryPacks_ModuleId",
                table: "BatteryPacks",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVtolDesigns_BatteryPackId",
                table: "ElectricVtolDesigns",
                column: "BatteryPackId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVtolDesigns_BladeId",
                table: "ElectricVtolDesigns",
                column: "BladeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVtolDesigns_FuselageId",
                table: "ElectricVtolDesigns",
                column: "FuselageId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVtolDesigns_InverterId",
                table: "ElectricVtolDesigns",
                column: "InverterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVtolDesigns_MissionParameterId",
                table: "ElectricVtolDesigns",
                column: "MissionParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVtolDesigns_MotorId",
                table: "ElectricVtolDesigns",
                column: "MotorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptualDesign");

            migrationBuilder.DropTable(
                name: "ElectricVtolDesigns");

            migrationBuilder.DropTable(
                name: "MeasureandQuantity");

            migrationBuilder.DropTable(
                name: "BatteryPacks");

            migrationBuilder.DropTable(
                name: "Blades");

            migrationBuilder.DropTable(
                name: "Fuselages");

            migrationBuilder.DropTable(
                name: "Inverters");

            migrationBuilder.DropTable(
                name: "MissionParameters");

            migrationBuilder.DropTable(
                name: "Motors");

            migrationBuilder.DropTable(
                name: "BatteryModules");

            migrationBuilder.DropTable(
                name: "Cells");

            migrationBuilder.CreateTable(
                name: "OverallDesignParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    battery_capacity = table.Column<string>(type: "text", nullable: false),
                    battery_weight = table.Column<string>(type: "text", nullable: false),
                    FlightTimeRequirementInMinutes = table.Column<double>(type: "double precision", nullable: false),
                    horsepower = table.Column<string>(type: "text", nullable: false),
                    motor_weight = table.Column<string>(type: "text", nullable: false),
                    payload_weight = table.Column<string>(type: "text", nullable: false),
                    power_required = table.Column<string>(type: "text", nullable: false),
                    design_weight = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverallDesignParameters", x => x.Id);
                });
        }
    }
}
