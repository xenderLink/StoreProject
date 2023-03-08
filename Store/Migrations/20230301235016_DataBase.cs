using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class DataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<string>(type: "varchar(30)", nullable: false),
                    icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    shipped = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "childs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    categoryid = table.Column<int>(name: "category_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "varchar(30)", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_childs", x => x.id);
                    table.ForeignKey(
                        name: "FK_childs_categories_category_id",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typeid = table.Column<int>(name: "type_id", type: "integer", nullable: false),
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    discount = table.Column<decimal>(type: "numeric", nullable: true),
                    quantity = table.Column<short>(type: "smallint", nullable: true),
                    SKU = table.Column<string>(type: "varchar(100)", nullable: true),
                    description = table.Column<string>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_childs_type_id",
                        column: x => x.typeid,
                        principalTable: "childs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cartline",
                columns: table => new
                {
                    orderid = table.Column<long>(name: "order_id", type: "bigint", nullable: false),
                    productid = table.Column<long>(name: "product_id", type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartline", x => new { x.orderid, x.productid });
                    table.ForeignKey(
                        name: "FK_cartline_orders_order_id",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartline_products_product_id",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    productid = table.Column<long>(name: "product_id", type: "bigint", nullable: false),
                    imageid = table.Column<long>(name: "image_id", type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_images", x => new { x.productid, x.imageid });
                    table.ForeignKey(
                        name: "FK_product_images_images_image_id",
                        column: x => x.imageid,
                        principalTable: "images",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_images_products_product_id",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartline_product_id",
                table: "cartline",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_childs_category_id",
                table: "childs",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_images_image_id",
                table: "product_images",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_type_id",
                table: "products",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartline");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "childs");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
