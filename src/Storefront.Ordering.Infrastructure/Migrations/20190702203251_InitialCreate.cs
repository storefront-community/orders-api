using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Storefront.Ordering.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    photo_id = table.Column<string>(maxLength: 50, nullable: true),
                    name = table.Column<string>(maxLength: 80, nullable: true),
                    description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    mobile = table.Column<string>(maxLength: 20, nullable: true),
                    email = table.Column<string>(maxLength: 80, nullable: true),
                    open_at = table.Column<DateTime>(nullable: false),
                    canceled_at = table.Column<DateTime>(nullable: true),
                    started_at = table.Column<DateTime>(nullable: true),
                    completed_at = table.Column<DateTime>(nullable: true),
                    delivered_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "items",
                schema: "ordering",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    category_id = table.Column<long>(nullable: false),
                    photo_id = table.Column<string>(maxLength: 50, nullable: true),
                    price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    is_available = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 80, nullable: true),
                    description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_item__category",
                        column: x => x.category_id,
                        principalSchema: "ordering",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                schema: "ordering",
                columns: table => new
                {
                    order_id = table.Column<long>(nullable: false),
                    item_id = table.Column<long>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => new { x.order_id, x.item_id });
                    table.ForeignKey(
                        name: "fk_order_item__item",
                        column: x => x.item_id,
                        principalSchema: "ordering",
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_item__order",
                        column: x => x.order_id,
                        principalSchema: "ordering",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_category_id",
                schema: "ordering",
                table: "items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_item_id",
                schema: "ordering",
                table: "order_items",
                column: "item_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "items",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "ordering");
        }
    }
}
