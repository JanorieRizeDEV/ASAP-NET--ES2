using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Amphoreus.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "coffes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    price = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    cateogoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    image = table.Column<string>(type: "TEXT", nullable: true),
                    CoffeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coffes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_coffes_categories_cateogoryId",
                        column: x => x.cateogoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_coffes_coffes_CoffeId",
                        column: x => x.CoffeId,
                        principalTable: "coffes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "coffeIngredient",
                columns: table => new
                {
                    ingredientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    coffeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coffeIngredient", x => new { x.coffeId, x.ingredientId });
                    table.ForeignKey(
                        name: "FK_coffeIngredient_coffes_coffeId",
                        column: x => x.coffeId,
                        principalTable: "coffes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_coffeIngredient_ingredients_ingredientId",
                        column: x => x.ingredientId,
                        principalTable: "ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Name", "description" },
                values: new object[,]
                {
                    { 1, "iceCoffe", null },
                    { 2, "hotCoffe", null },
                    { 3, "blendedCoffe", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_coffeIngredient_ingredientId",
                table: "coffeIngredient",
                column: "ingredientId");

            migrationBuilder.CreateIndex(
                name: "IX_coffes_cateogoryId",
                table: "coffes",
                column: "cateogoryId");

            migrationBuilder.CreateIndex(
                name: "IX_coffes_CoffeId",
                table: "coffes",
                column: "CoffeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coffeIngredient");

            migrationBuilder.DropTable(
                name: "coffes");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
