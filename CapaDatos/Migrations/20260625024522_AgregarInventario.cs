using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
             //   name: "Facturas",
              //  columns: table => new
               // {
                 //   Id = table.Column<int>(type: "int", nullable: false)
                   //     .Annotation("SqlServer:Identity", "1, 1"),
                  //  ClienteId = table.Column<int>(type: "int", nullable: false),
                    //NumeroFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  //  Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                   // Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
               // },
               // constraints: table =>
               // {
                 //   table.PrimaryKey("PK_Facturas", x => x.Id);
                //});

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    IdInventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.IdInventario);
                });

            //migrationBuilder.CreateTable(
              //  name: "Productos",
                //columns: table => new
               // {
                  //  Id = table.Column<int>(type: "int", nullable: false)
                 //       .Annotation("SqlServer:Identity", "1, 1"),
                   // CategoriaID = table.Column<int>(type: "int", nullable: false),
               //     Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                 //   Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   // Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                  //  Activo = table.Column<bool>(type: "bit", nullable: false),
                   // Stock = table.Column<int>(type: "int", nullable: false)
               // },
               // constraints: table =>
               // {
                 //   table.PrimaryKey("PK_Productos", x => x.Id);
                   // table.ForeignKey(
                     //   name: "FK_Productos_Categorias_CategoriaID",
                       // column: x => x.CategoriaID,
             //           principalTable: "Categorias",
               //         principalColumn: "Id",
                 //       onDelete: ReferentialAction.Cascade);
               // });

           // migrationBuilder.CreateTable(
             //   name: "DetalleFacturas",
               // columns: table => new
               // {
                 //   Id = table.Column<int>(type: "int", nullable: false)
                   //     .Annotation("SqlServer:Identity", "1, 1"),
                   // FacturaId = table.Column<int>(type: "int", nullable: false),
                   // ProductoId = table.Column<int>(type: "int", nullable: false),
                   // Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   // Cantidad = table.Column<int>(type: "int", nullable: false),
                   // PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    //SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                //    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                  //  Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
              //  },
              //  constraints: table =>
                //{
                  //  table.PrimaryKey("PK_DetalleFacturas", x => x.Id);
                 //   table.ForeignKey(
                   //     name: "FK_DetalleFacturas_Facturas_FacturaId",
                    //    column: x => x.FacturaId,
                      //  principalTable: "Facturas",
                        //principalColumn: "Id",
              //          onDelete: ReferentialAction.Cascade);
                //    table.ForeignKey(
                  //      name: "FK_DetalleFacturas_Productos_ProductoId",
                    //    column: x => x.ProductoId,
                      //  principalTable: "Productos",
                        //principalColumn: "Id",
                        //onDelete: ReferentialAction.Cascade);
               // });

            //migrationBuilder.CreateIndex(
              //  name: "IX_DetalleFacturas_FacturaId",
                //table: "DetalleFacturas",
                //column: "FacturaId");

            //migrationBuilder.CreateIndex(
              //  name: "IX_DetalleFacturas_ProductoId",
              //  table: "DetalleFacturas",
                //column: "ProductoId");

            //migrationBuilder.CreateIndex(
              //  name: "IX_Productos_CategoriaID",
                //table: "Productos",
                //column: "CategoriaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleFacturas");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
