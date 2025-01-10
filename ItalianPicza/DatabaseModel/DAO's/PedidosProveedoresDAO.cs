using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.DatabaseModel.DTO;
using ItalianPicza.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class PedidoProveedorDAO
    {
        public PedidoProveedorDAO() { }

        internal static int EliminarRelacionPedidoProveedor(int idPedidoEdicion, int idProducto)
        {
            throw new NotImplementedException();
        }

        public int GuardarNuevoPedidoProveedor(pedidoproveedor nuevoPedido)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.pedidoproveedor.Add(nuevoPedido);
                    int resultado = context.SaveChanges();
                    return resultado;
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Error al guardar el pedido del proveedor: " + ex.Message, ex);
            }
        }

        public List<pedidoproveedor> ObtenerPedidosProveedores()
        {
            List<pedidoproveedor> pedidosproveedores = new List<pedidoproveedor>();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    pedidosproveedores = context.pedidoproveedor.Include("pedidoproveedor").ToList();
                }

            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return pedidosproveedores;
        }
        public static int registrarNuevoPedido(pedidoproveedor nuevoPedido)
        {
            int resultado = 0;
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.pedidoproveedor.Add(nuevoPedido);
                    context.SaveChanges();
                    resultado = nuevoPedido.idPedidoProveedor;
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        internal static void registrarPedidoProveedorProducto(int idPedidoRegistro, int idPedido, int cantidad)
        {
            throw new NotImplementedException();
        }

        internal static int EliminarRelacionesPedidos(int idPedidoEdicion)
        {
            throw new NotImplementedException();
        }

        internal static int eliminarPedido(int idPedidoEdicion)
        {
            throw new NotImplementedException();
        }

        internal static int incrementarCantidadProductos(List<producto> productos)
        {
            throw new NotImplementedException();
        }

        internal static int registrarPedidoProducto(int idPedidoEdicion, int idProducto, int cantidad)
        {
            throw new NotImplementedException();
        }

        internal static void actualizarTotalPedido(decimal nuevoTotal, int idPedidoEdicion)
        {
            throw new NotImplementedException();
        }

        internal static List<ProductoResumenDTO> recuperarProductos(int idPedido)
        {
            throw new NotImplementedException();
        }

        internal static int decrementarCantidadPedido(int idPedidoEdicion, ProductoGeneral resumenProducto)
        {
            throw new NotImplementedException();
        }

        internal static int incrementarCantidadPedido(int idPedidoEdicion, ProductoGeneral resumenProducto)
        {
            throw new NotImplementedException();
        }
        public static List<ProductoGeneral> ObtenerProductosEIngredientes()
        {
            List<ProductoGeneral> productosEIngredientes = new List<ProductoGeneral>();

            using (var context = new italianpizzaEntities())
            {
                // Fetch productos
                var productos = from p in context.producto
                                select new ProductoGeneral
                                {
                                    IdProducto = p.idProducto,
                                    TipoPedido = "Producto",
                                    UnidadMedida = p.medida,
                                    Precio = (decimal)p.precio,
                                    Nombre = p.nombre, // Assuming 'nombre' exists in 'producto'
                                    Cantidad = p.cantidad ?? 0 // Assuming 'cantidad' is nullable in 'producto'
                                };

                // Fetch ingredientes
                var ingredientes = from i in context.ingrediente
                                   select new ProductoGeneral
                                   {
                                       IdProducto = i.idIngrediente,
                                       TipoPedido = "Ingrediente",
                                       UnidadMedida = i.medida,
                                       Precio = (decimal)i.precio,
                                       Nombre = i.nombre, // Assuming 'nombre' exists in 'ingrediente'
                                       Cantidad = i.cantidad ?? 0 // Assuming 'cantidad' is nullable in 'ingrediente'
                                   };

                // Combine both lists
                productosEIngredientes = productos
                    .Union(ingredientes)
                    .OrderBy(pe => pe.TipoPedido) // Optional ordering
                    .ToList();
            }

            return productosEIngredientes;
        }
    }
}
