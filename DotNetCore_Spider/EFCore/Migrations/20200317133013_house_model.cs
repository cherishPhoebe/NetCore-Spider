using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZY.EFCore.Migrations
{
    public partial class house_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HouseKey = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    HomeUrl = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    RoomType = table.Column<string>(nullable: true),
                    BuildingType = table.Column<string>(nullable: true),
                    Point = table.Column<string>(nullable: true),
                    BaseInfoJson = table.Column<string>(nullable: true),
                    SaseInfoJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerSaleInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false),
                    License = table.Column<string>(nullable: true),
                    IssueDate = table.Column<string>(nullable: true),
                    BindBuilding = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerSaleInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerSaleInfos_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false),
                    RecordDate = table.Column<string>(nullable: true),
                    AvgPrice = table.Column<string>(nullable: true),
                    StartingPrice = table.Column<string>(nullable: true),
                    PriceDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceInfos_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerSaleInfos_HouseId",
                table: "PerSaleInfos",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceInfos_HouseId",
                table: "PriceInfos",
                column: "HouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerSaleInfos");

            migrationBuilder.DropTable(
                name: "PriceInfos");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
