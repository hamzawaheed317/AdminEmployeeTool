using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminEmployeeTool.Migrations
{
    /// <inheritdoc />
    public partial class iniativestablesetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Initiative",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_manager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    objective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    benefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    files_paths = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Initiative", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Initiative");
        }
    }
}
