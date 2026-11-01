using System;
using CapaEntidad;
using CapaDatos.Contexto;
using CapaNegocio.Servicio.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicio
{
    public class FacturaServicio : IFacturaServicio
    {
        private readonly DbContextAparrel _dbContextAparrel;

        public FacturaServicio(DbContextAparrel contextAparrel)
        {
            this._dbContextAparrel = contextAparrel;
        }

        public async Task<IEnumerable<FacturaDTO>> ObtenerTodas()
        {
            var listaFacturas = await _dbContextAparrel.Facturas
                .Include(f => f.DetalleFactura)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();

            return listaFacturas.Select(f => new FacturaDTO
            {
                Id = f.Id,
                ClienteId = f.ClienteId,
                Fecha = f.Fecha,
                Total = f.Total,
                DetalleFacturaDTOs = f.DetalleFactura?.Select(d => new DetalleFacturaDTO
                {
                    Id = d.Id,
                    FacturaId = d.FacturaId,
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Nombre = d.Producto != null ? d.Producto.Nombre : "Sin Nombre",
                    Descuento = d.Descuento,
                    Descripcion = d.Descripcion
                }).ToList() ?? new List<DetalleFacturaDTO>()
            }).ToList();
        }

        public async Task<MessageDTO> CrearFactura(FacturaDTO factura)
        {
            var messageDTO = new MessageDTO();
            using (var transaction = await _dbContextAparrel.Database.BeginTransactionAsync())
            {
                try
                {
                    messageDTO.IsSuccess = true;
                    decimal totalFactura = 0;
                    var detalleFactura = new List<DetalleFactura>();

                    foreach (var detalle in factura.DetalleFacturaDTOs)
                    {
                        var producto = await _dbContextAparrel.Productos
                        .FirstOrDefaultAsync(x => x.Id == detalle.ProductoId);

                        if (producto == null)
                        {
                            messageDTO.IsSuccess = false;
                            messageDTO.Message.Add($"El producto con ID {detalle.ProductoId} no existe.");
                            await transaction.RollbackAsync();
                            return messageDTO;
                        }

                        if (producto.Stock < detalle.Cantidad)
                        {
                            messageDTO.IsSuccess = false;
                            messageDTO.Message.Add($"No hay suficiente stock para {producto.Nombre}.");
                            await transaction.RollbackAsync();
                            return messageDTO;
                        }

                        // Descontar stock
                        producto.Stock -= detalle.Cantidad;
                        var descuento = detalle.Cantidad * detalle.PrecioUnitario * (detalle.Descuento / 100);
                        var subTotalDetalle = detalle.Cantidad * detalle.PrecioUnitario - descuento;
                        totalFactura += subTotalDetalle;

                        detalleFactura.Add(new DetalleFactura
                        {
                            Fecha = DateTime.Now,
                            ProductoId = detalle.ProductoId,
                            Descripcion = detalle.Descripcion,
                            Cantidad = detalle.Cantidad,
                            PrecioUnitario = detalle.PrecioUnitario,
                            SubTotal = subTotalDetalle,
                            Descuento = detalle.Descuento
                        });
                    }

                    var nuevaFactura = new Factura
                    {
                        NumeroFactura = await GenerarNumeroFactura(),
                        Fecha = DateTime.Now,
                        Total = totalFactura,
                        DetalleFactura = detalleFactura
                    };

                    _dbContextAparrel.Facturas.Add(nuevaFactura);
                    var result = await _dbContextAparrel.SaveChangesAsync();
                    await transaction.CommitAsync();

                    messageDTO.IsSuccess = true;
                    messageDTO.Message.Add("La factura se creo correctamente.");
                    return messageDTO;
                }
                catch (Exception ex)
                {
                    messageDTO.Message.Add(ex.Message);
                    messageDTO.IsSuccess = false;
                    await transaction.RollbackAsync();
                    return messageDTO;
                }
            }
        }

        public async Task<string> GenerarNumeroFactura()
        {
            var fecha = DateTime.Now;
            int siguienteNumero = 1;
            var numeroFactura = $"FAC-{fecha:yyyyMMdd}";

            var ultimoNumero = await _dbContextAparrel.Facturas
                .Where(f => f.NumeroFactura.StartsWith(numeroFactura))
                .OrderByDescending(f => f.Id)
                .FirstOrDefaultAsync();

            if (ultimoNumero != null)
            {
                var partes = ultimoNumero.NumeroFactura.Split('-');
                if (partes.Length >= 3 && int.TryParse(partes[2], out int numero))
                {
                    siguienteNumero = numero + 1;
                }
            }
            return $"{numeroFactura}-{siguienteNumero:D4}";
        }
    }
}