using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ItalianPicza.Model.PedidoGeneral;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    internal class PedidoDAO
    {
        public static List<PedidoGeneral> ObtenerPedidosUnificados()
        {
            using (var context = new italianpizzaEntities())
            {
                // Obtener pedidos a domicilio
                var pedidosDomicilio = from pedido in context.pedido
                                       join domicilio in context.pedidodomicilio
                                       on pedido.idPedido equals domicilio.idPedidoDomicilio
                                       select new PedidoGeneral
                                       {
                                           IdPedido = pedido.idPedido,
                                           Fecha = pedido.fecha,
                                           Cantidad = (decimal)pedido.cantidad,
                                           TipoPedido = "Domicilio",
                                           InfoExtra = domicilio.idCliente.ToString()
                                       };

                // Obtener pedidos locales
                var pedidosLocales = from pedido in context.pedido
                                     join local in context.pedidolocal
                                     on pedido.idPedido equals local.idPedidoLocal
                                     select new PedidoGeneral
                                     {
                                         IdPedido = pedido.idPedido,
                                         Fecha = pedido.fecha,
                                         Cantidad = (decimal)pedido.cantidad,
                                         TipoPedido = "Local",
                                         InfoExtra = local.idMesa.ToString()
                                     };

                // Combinar ambas listas
                var pedidosUnificados = pedidosDomicilio.Union(pedidosLocales).ToList();

                return pedidosUnificados;
            }
        }
        public static int EliminarPedido(int idPedido)
            {
            int resultado = 0;
                using (var context = new italianpizzaEntities())
                {
                    // Buscar el pedido por id
                    var pedidoAEliminar = context.pedido.SingleOrDefault(p => p.idPedido == idPedido);

                    if (pedidoAEliminar != null)
                    {
                        // Eliminar el pedido si se encuentra
                        context.pedido.Remove(pedidoAEliminar);
                        resultado = context.SaveChanges(); //
                    }
                    return resultado;
                }
            }

        public static List<producto> recuperarProductos(int idPedido)
        {
            List<producto>listaProductosPedido = new List<producto> ();
            try {
                using (var context = new italianpizzaEntities())
                {
                    // Buscar el pedido por id
                    var listaProductos = context.pedido.Where(p => p.idPedido == idPedido)
                        .SelectMany(p => p.pedidoproducto.Select(pp => pp.producto))
                        .ToList();
                    listaProductosPedido = listaProductos;
                }
            }catch(InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return listaProductosPedido;

            }

        public static int registrarNuevoPedido(pedido nuevoPedido)
        {
            int resultado = 0;
                try
                {
                    using (var context = new italianpizzaEntities())
                    {
                        context.pedido.Add(nuevoPedido);
                        resultado = context.SaveChanges();
                        nuevoPedido.idPedido = resultado;
                    }

                    return resultado;
                }
                catch (EntityException ex)
                {
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
            }

        public static int registrarProductosPedido(pedidoproducto nuevoPedidoProducto)
        {
            int resultado = 0;
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.pedidoproducto.Add(nuevoPedidoProducto);
                    resultado = context.SaveChanges();
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }
    }
}
