using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBatteryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current",
                table: "BatteryPacks");

            migrationBuilder.DropColumn(
                name: "current",
                table: "BatteryModules");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "Cells",
                newName: "mass");

            migrationBuilder.RenameColumn(
                name: "current",
                table: "Cells",
                newName: "energy");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "BatteryPacks",
                newName: "mass");

            migrationBuilder.RenameColumn(
                name: "power",
                table: "BatteryPacks",
                newName: "energy");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "BatteryModules",
                newName: "mass");

            migrationBuilder.RenameColumn(
                name: "power",
                table: "BatteryModules",
                newName: "energy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mass",
                table: "Cells",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "energy",
                table: "Cells",
                newName: "current");

            migrationBuilder.RenameColumn(
                name: "mass",
                table: "BatteryPacks",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "energy",
                table: "BatteryPacks",
                newName: "power");

            migrationBuilder.RenameColumn(
                name: "mass",
                table: "BatteryModules",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "energy",
                table: "BatteryModules",
                newName: "power");

            migrationBuilder.AddColumn<string>(
                name: "current",
                table: "BatteryPacks",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "current",
                table: "BatteryModules",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
