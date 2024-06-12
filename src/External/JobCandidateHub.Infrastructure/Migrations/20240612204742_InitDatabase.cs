using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobCandidateHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidate",
                columns: table => new
                {
                    candidate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    call_time = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    linkedIn_url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    gitHub_url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate", x => x.candidate_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_candidate_email",
                table: "candidate",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate");
        }
    }
}
