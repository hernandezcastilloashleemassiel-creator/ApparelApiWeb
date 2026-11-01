using CapaNegocio.Servicio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Contexto;
using CapaEntidad;
using Microsoft.EntityFrameworkCore;
using CapaEntidad.DTOs;

namespace CapaNegocio.Servicio
{
    public class ProveedoresServicio : IProveedoresServicio
    {
        private readonly DbContextAparrel _dbContextApparel;

        public ProveedoresServicio(DbContextAparrel dbContextApparel)
        {
            _dbContextApparel = dbContextApparel;
        }

        async Task<bool> IProveedoresServicio.ActualizarProveedor(ProveedoresDTO proveedor)
        {
            {
                if (proveedor == null) return false;

                var proveedorExistente = await _dbContextApparel.Proveedores.FirstOrDefaultAsync(p => p.Id == proveedor.Id);

                if (proveedorExistente == null) return false;

                // Pasamos los datos modificados del DTO a la entidad de la BD
                proveedorExistente.Empresa = proveedor.Empresa;
                proveedorExistente.Vendedor = proveedor.Vendedor;
                proveedorExistente.Correo = proveedor.Correo;
                proveedorExistente.Telefono = proveedor.Telefono;
                proveedorExistente.Direccion = proveedor.Direccion;
                int resultado = await _dbContextApparel.SaveChangesAsync();
                return resultado > 0;
            }

        }

        private object NewMethod(ProveedoresDTO proveedor)
        {
            return _dbContextApparel.Proveedores.FirstOrDefaultAsync(p => p.Id == proveedor.Id);
        }

        public async Task<bool> EliminarProveedor(ProveedoresDTO proveedor)
        {
            {
                if (proveedor == null) return false;

                var proveedorExistente = await _dbContextApparel.Proveedores.FirstOrDefaultAsync(p => p.Id == proveedor.Id);

                if (proveedorExistente == null) return false;

                _dbContextApparel.Proveedores.Remove(proveedorExistente);
                int resultado = await _dbContextApparel.SaveChangesAsync();
                return resultado > 0;
            }
        }

        public async Task<bool> InsertarProveedor(ProveedoresDTO proveedor)
        {
            {
                if (proveedor == null) return false;

                var nuevoProveedor = new Proveedores
                {
                    Empresa = proveedor.Empresa,
                    Vendedor = proveedor.Vendedor,
                    Correo = proveedor.Correo,
                    Telefono = proveedor.Telefono,
                    Direccion = proveedor.Direccion
                };

                await _dbContextApparel.Proveedores.AddAsync(nuevoProveedor);
                int resultado = await _dbContextApparel.SaveChangesAsync();

                return resultado > 0;
            }

        }

        public async Task<List<ProveedoresDTO>> ListarProveedores()
        {

            var proveedores = await _dbContextApparel.Proveedores
                .Select(p => new ProveedoresDTO
                {
                    Id = p.Id,
                    Empresa = p.Empresa,
                    Vendedor = p.Vendedor,
                    Correo = p.Correo,
                    Telefono = p.Telefono,
                    Direccion = p.Direccion
                })
                .ToListAsync();

            return proveedores;

        }

        async Task<ProveedoresDTO> IProveedoresServicio.ObtenerProveedorPorId(int id)
        {
            var proveedor = await _dbContextApparel.Proveedores
             .Where(p => p.Id == id)
             .Select(p => new ProveedoresDTO
             {
                 Id = p.Id,
                 Empresa = p.Empresa,
                 Vendedor = p.Vendedor,
                 Correo = p.Correo,
                 Telefono = p.Telefono,
                 Direccion = p.Direccion
             })
             .FirstOrDefaultAsync();

            return proveedor;
        }
    }
}