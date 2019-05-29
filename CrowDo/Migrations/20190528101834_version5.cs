using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowDo.Migrations
{
    public partial class version5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    CreatedProjectsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProjectCategory = table.Column<string>(nullable: true),
                    ProjectGoal = table.Column<decimal>(nullable: false),
                    Funds = table.Column<decimal>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    IsSuccessful = table.Column<bool>(nullable: false),
                    Visits = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    EstimatedDurationInMonths = table.Column<int>(nullable: false),
                    StatusUpdate = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RewardPackage",
                columns: table => new
                {
                    RewardPackageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageName = table.Column<string>(nullable: true),
                    RewardName = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardPackage", x => x.RewardPackageId);
                    table.ForeignKey(
                        name: "FK_RewardPackage_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BackerReward",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RewardPackageId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackerReward", x => new { x.UserId, x.RewardPackageId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_BackerReward_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackerReward_RewardPackage_RewardPackageId",
                        column: x => x.RewardPackageId,
                        principalTable: "RewardPackage",
                        principalColumn: "RewardPackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackerReward_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackerReward_ProjectId",
                table: "BackerReward",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BackerReward_RewardPackageId",
                table: "BackerReward",
                column: "RewardPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardPackage_ProjectId",
                table: "RewardPackage",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackerReward");

            migrationBuilder.DropTable(
                name: "RewardPackage");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
