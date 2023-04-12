using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomePilot.Db.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Leases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    RentInCents = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leases", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LeaseTenants",
                columns: table => new
                {
                    LeaseId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TenantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseTenants", x => new { x.LeaseId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_LeaseTenants_Leases_LeaseId",
                        column: x => x.LeaseId,
                        principalTable: "Leases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaseTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseTenants_TenantId_LeaseId",
                table: "LeaseTenants",
                columns: new[] { "TenantId", "LeaseId" },
                unique: true);

            // Fill the db
            migrationBuilder.Sql(
                "INSERT INTO tenants (Id, FirstName , LastName) VALUES (UUID(), 'John', 'Doe'), (UUID(), 'Jane', 'Doe'), (UUID(), 'John', 'Smith'), (UUID(), 'Jane', 'Smith'), (UUID(), 'John', 'Jones'), (UUID(), 'Jane', 'Jones');");

            migrationBuilder.Sql(
                "INSERT INTO leases (id, name, StartDate, EndDate, RentInCents) VALUES (UUID(), 'Lease 1', '2019-01-01', '2020-12-01', 100000), (UUID(), 'Lease 2', '2019-01-01', null, 110000),  (UUID(), 'Lease 3', '2019-01-01', null, 120000);");

            migrationBuilder.Sql(
                "INSERT INTO leasetenants(LeaseId, TenantId) select l.Id, t.Id from tenants t join leases l limit 2;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaseTenants");

            migrationBuilder.DropTable(
                name: "Leases");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
