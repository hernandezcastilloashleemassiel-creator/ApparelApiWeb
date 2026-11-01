using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos.Contexto;
using CapaEntidad;
using CapaEntidad.DTOs;
using CapaNegocio.Servicio.Interface;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicio
{
    public class DetalleFacturaServicio : IDetalleFacturaServicio
    {
        private readonly DbContextAparrel _context;

        public DetalleFacturaServicio(DbContextAparrel context)
        {
            _context = context;
        }


        public async Task<DetalleFactura> Crear(DetalleFacturaDTO dto)
        {
            var detalle = new DetalleFactura
            {
                FacturaId = dto.FacturaId,
                ProductoId = dto.ProductoId,
                Descripcion = dto.Descripcion,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                Descuento = dto.Descuento,
                Fecha = DateTime.Now,
                SubTotal = (dto.PrecioUnitario * dto.Cantidad) - dto.Descuento
            };

            _context.Add(detalle);
            await _context.SaveChangesAsync();

            return detalle;
        }

          async Task<IEnumerable<DetalleFacturaDTO>> IDetalleFacturaServicio.ObtenerPorFactura(int facturaId)
        {
            var detalles = await _context.DetalleFacturas
                .Where(df => df.FacturaId == facturaId)
                .Include(df => df.Producto)
                .ToListAsync();

            // Mapeamos de la entidad de base de datos al DTO correspondiente
            return detalles.Select(d => new DetalleFacturaDTO
            {
                Id = d.Id,
                FacturaId = d.FacturaId,
                ProductoId = d.ProductoId,
                Descripcion = d.Descripcion,
                Nombre = d.Producto != null ? d.Producto.Nombre : "Sin Nombre",
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Descuento = d.Descuento
            }).ToList();
        }
    }
}