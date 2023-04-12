using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomePilot.Db.Migrations
{
    /// <inheritdoc />
    public partial class addAmendments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AmendmentEntryId",
                table: "LeaseTenants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "AmendmentExitId",
                table: "LeaseTenants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Amendments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreationDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LeaseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EffectiveDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    OldRentInCents = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amendments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amendments_Leases_LeaseId",
                        column: x => x.LeaseId,
                        principalTable: "Leases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseTenants_AmendmentEntryId",
                table: "LeaseTenants",
                column: "AmendmentEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseTenants_AmendmentExitId",
                table: "LeaseTenants",
                column: "AmendmentExitId");

            migrationBuilder.CreateIndex(
                name: "IX_Amendments_LeaseId",
                table: "Amendments",
                column: "LeaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseTenants_Amendments_AmendmentEntryId",
                table: "LeaseTenants",
                column: "AmendmentEntryId",
                principalTable: "Amendments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaseTenants_Amendments_AmendmentExitId",
                table: "LeaseTenants",
                column: "AmendmentExitId",
                principalTable: "Amendments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaseTenants_Amendments_AmendmentEntryId",
                table: "LeaseTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaseTenants_Amendments_AmendmentExitId",
                table: "LeaseTenants");

            migrationBuilder.DropTable(
                name: "Amendments");

            migrationBuilder.DropIndex(
                name: "IX_LeaseTenants_AmendmentEntryId",
                table: "LeaseTenants");

            migrationBuilder.DropIndex(
                name: "IX_LeaseTenants_AmendmentExitId",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "AmendmentEntryId",
                table: "LeaseTenants");

            migrationBuilder.DropColumn(
                name: "AmendmentExitId",
                table: "LeaseTenants");
        }
    }
}
