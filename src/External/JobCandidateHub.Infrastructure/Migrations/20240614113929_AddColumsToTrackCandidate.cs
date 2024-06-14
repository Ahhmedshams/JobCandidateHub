using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobCandidateHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumsToTrackCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "candidate",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "candidate",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "candidate");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "candidate");
        }
    }
}
