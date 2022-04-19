using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class InitialDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuzzyLogicAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuzzyLogicAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuzzyLogicAreaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rules_FuzzyLogicAreas_FuzzyLogicAreaId",
                        column: x => x.FuzzyLogicAreaId,
                        principalTable: "FuzzyLogicAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TermName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuzzyLogicAreaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terms_FuzzyLogicAreas_FuzzyLogicAreaId",
                        column: x => x.FuzzyLogicAreaId,
                        principalTable: "FuzzyLogicAreas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subsets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsets_Terms_TermId",
                        column: x => x.TermId,
                        principalTable: "Terms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rules_FuzzyLogicAreaId",
                table: "Rules",
                column: "FuzzyLogicAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsets_TermId",
                table: "Subsets",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_FuzzyLogicAreaId",
                table: "Terms",
                column: "FuzzyLogicAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Subsets");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "FuzzyLogicAreas");
        }
    }
}
