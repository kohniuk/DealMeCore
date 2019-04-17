using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DealMeCore.DB.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Disabled = table.Column<bool>(nullable: true),
                    Image = table.Column<string>(maxLength: 250, nullable: false),
                    Url = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANDS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BrandId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    OriginalPrice = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DealValidFrom = table.Column<DateTime>(nullable: false),
                    DealValidTo = table.Column<DateTime>(nullable: false),
                    MarketUrl = table.Column<string>(maxLength: 250, nullable: false),
                    BuyNowUrl = table.Column<string>(maxLength: 250, nullable: false),
                    Gender = table.Column<byte>(nullable: false),
                    DealType = table.Column<byte>(nullable: false),
                    CouponCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deals_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 400, nullable: false),
                    Location = table.Column<IPoint>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: true),
                    BrandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DealImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(maxLength: 400, nullable: false),
                    DealId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEALIMAGES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealImages_Deals_DealId",
                        column: x => x.DealId,
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NAME",
                table: "Brands",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_DealImages_DealId",
                table: "DealImages",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_BrandId",
                table: "Deals",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_BrandId",
                table: "Stores",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_NAME",
                table: "Stores",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealImages");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
