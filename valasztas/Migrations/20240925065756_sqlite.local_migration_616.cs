using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace valasztas.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_616 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partok",
                columns: table => new
                {
                    RovidNev = table.Column<string>(type: "TEXT", nullable: false),
                    TeljesNev = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partok", x => x.RovidNev);
                });

            migrationBuilder.CreateTable(
                name: "JeloltekListaja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nev = table.Column<string>(type: "TEXT", nullable: false),
                    PartRovidNev = table.Column<string>(type: "TEXT", nullable: false),
                    Kerulet = table.Column<int>(type: "INTEGER", nullable: false),
                    SzavazatokSzama = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JeloltekListaja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JeloltekListaja_Partok_PartRovidNev",
                        column: x => x.PartRovidNev,
                        principalTable: "Partok",
                        principalColumn: "RovidNev",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JeloltekListaja_PartRovidNev",
                table: "JeloltekListaja",
                column: "PartRovidNev");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JeloltekListaja");

            migrationBuilder.DropTable(
                name: "Partok");
        }
    }
}
