using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OverallDesignParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    design_weight = table.Column<string>(type: "text", nullable: false),
                    payload_weight = table.Column<string>(type: "text", nullable: false),
                    FlightTimeRequirementInMinutes = table.Column<double>(type: "double precision", nullable: false),
                    power_required = table.Column<string>(type: "text", nullable: false),
                    battery_capacity = table.Column<string>(type: "text", nullable: false),
                    battery_weight = table.Column<string>(type: "text", nullable: false),
                    motor_weight = table.Column<string>(type: "text", nullable: false),
                    horsepower = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverallDesignParameters", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OverallDesignParameters");
        }
    }
}
