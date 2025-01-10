
using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.DatabaseModel.DTO;
using ItalianPicza.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ItalianPicza.DatabaseModel.DAO_s
{
    internal class PedidoDAO
    {
        public static List<PedidoGeneral> ObtenerPedidosUnificados()
        {
            List<PedidoGeneral> pedidosUnificados = new List<PedidoGeneral>();

            using (var context = new italianpizzaEntities())
            {
                // Fetch pedidosDomicilio
                var pedidosDomicilio = from pd in context.pedidocliente
                                       join p in context.pedido on pd.idPedido equals p.idPedido
                                       join c in context.cliente on pd.idCliente equals c.idCliente
                                       join e in context.estadopedido on p.idEstadoPedido equals e.idEstadoPedido
                                       select new
                                       {
                                           IdPedido = p.idPedido,
                                           Fecha = p.fecha ?? "sin fecha",
                                           Cantidad = p.cantidad ?? 0,
                                           TipoPedido = "Domicilio",
                                           Detalle = c.nombre,
                                           Estado = e.nombreEstado.Trim()
                                       };

                // Fetch pedidosLocal
                var pedidosLocal = from pl in context.pedidolocal
                                   join p in context.pedido on pl.idPedidoLocal equals p.idPedido
                                   join m in context.mesa on pl.idMesa equals m.idMesa
                                   join e in context.estadopedido on p.idEstadoPedido equals e.idEstadoPedido
                                   select new
                                   {
                                       IdPedido = p.idPedido,
                                       Fecha = p.fecha ?? "sin fecha",
                                       Cantidad = p.cantidad ?? 0,
                                       TipoPedido = "Local",
                                       NumeroMesa = m.numero,
                                       Estado = e.nombreEstado.Trim()
                                   };

                // Fetch related products for all pedidos
                var productosRelacionados = from pp in context.pedidoproducto // Many-to-many table
                                            join prod in context.producto on pp.idProducto equals prod.idProducto
                                            select new
                                            {
                                                pp.idPedido,
                                                Producto = prod
                                            };

                // Convert pedidosDomicilio to PedidoGeneral and include related products
                var pedidosDomicilioList = pedidosDomicilio
                    .ToList()
                    .Select(pd => new PedidoGeneral
                    {
                        IdPedido = pd.IdPedido,
                        Fecha = pd.Fecha,
                        Cantidad = pd.Cantidad,
                        TipoPedido = pd.TipoPedido,
                        Detalle = pd.Detalle,
                        estado = pd.Estado,
                        productosRelacionados = productosRelacionados
                            .Where(pp => pp.idPedido == pd.IdPedido)
                            .Select(pp => pp.Producto)
                            .ToList()
                    }).ToList();

                // Convert pedidosLocal to PedidoGeneral and include related products
                var pedidosLocalList = pedidosLocal
                    .ToList()
                    .Select(pl => new PedidoGeneral
                    {
                        IdPedido = pl.IdPedido,
                        Fecha = pl.Fecha,
                        Cantidad = pl.Cantidad,
                        TipoPedido = pl.TipoPedido,
                        Detalle = "Mesa: " + (pl.NumeroMesa.HasValue ? pl.NumeroMesa.Value.ToString() : "N/A"),
                        estado = pl.Estado,
                        productosRelacionados = productosRelacionados
                            .Where(pp => pp.idPedido == pl.IdPedido)
                            .Select(pp => pp.Producto)
                            .ToList()
                    }).ToList();

                // Combine and order pedidos
                pedidosUnificados = pedidosDomicilioList
                    .Union(pedidosLocalList)
                    .OrderBy(p => p.Fecha)
                    .ToList();
            }

            return pedidosUnificados;
        }

        public static List<ProductoResumenDTO> recuperarProductos(int idPedido)
        {
            List<ProductoResumenDTO> listaProductosPedido = new List<ProductoResumenDTO>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    listaProductosPedido = context.pedidoproducto
                        .Where(pp => pp.idPedido == idPedido)
                        .Select(pp => new ProductoResumenDTO
                        {
                            idProducto = pp.producto.idProducto,
                            nombre = pp.producto.nombre,
                            precio = (decimal)pp.producto.precio,
                            cantidad = (int)pp.cantidad,
                            precioTotal = (decimal)(pp.producto.precio * pp.cantidad)
                        })
                        .ToList();
                }
            }
            catch (InvalidOperationException)
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
                    context.SaveChanges();
                    resultado = nuevoPedido.idPedido;
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        public static int registrarNuevoPedidoLocal(int idPedido, int idMesa)
        {
            int resultado = 0;
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    pedidolocal pedidoLocalRegistro = new pedidolocal
                    {
                        idPedidoLocal = idPedido,
                        idMesa = idMesa,
                    };
                    context.pedidolocal.Add(pedidoLocalRegistro);
                    context.SaveChanges();
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        public static int eliminarPedido(int idPedido)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var pedidoAEliminar = context.pedido
                        .FirstOrDefault(p => p.idPedido == idPedido);
                    if (pedidoAEliminar != null)
                    {
                        context.pedido.Remove(pedidoAEliminar);
                        context.SaveChanges();
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
                return -1;
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                GestorCuadroDialogo.MostrarError($"Ocurrió un error al eliminar el pedido: {ex.Message}",
                    "Error al eliminar pedido");
                return -1;
            }

            return 0;
        }

        public static List<estadopedido> obtenerCatalogoEstados()
        {
            List<estadopedido> listaEstados = new List<estadopedido>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    listaEstados = context.estadopedido.ToList();
                    foreach (var estado in listaEstados)
                    {
                        estado.nombreEstado = estado.nombreEstado?.Trim();
                    }
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexión con la base de datos, inténtelo más tarde.",
                    "Sin conexión a la base de datos");
            }
            catch (Exception ex)
            {
                GestorCuadroDialogo.MostrarError($"Ocurrió un error al obtener los estados de pedido: {ex.Message}",
                    "Error al obtener estados");
            }
            return listaEstados;
        }

        public static int registrarPedidoProducto(int idPedido, int idProducto, int cantidad)
        {
            pedidoproducto pedidoProductoRegistro = new pedidoproducto
            {
                idPedido = idPedido,
                idProducto = idProducto,
                cantidad = cantidad
            };

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.pedidoproducto.Add(pedidoProductoRegistro);
                    int resultado = context.SaveChanges();
                    return resultado > 0 ? 0 : -1;
                }
            }
            catch (EntityException ex)
            {
                GestorCuadroDialogo.MostrarError($"Ocurrió un error registrarPedidoProducto: {ex.Message}",
                    "Error al registrarPedidoProducto");
                return -1;
            }
            catch (Exception ex)
            {
                GestorCuadroDialogo.MostrarError($"Ocurrió un error registrarPedidoProducto: {ex.Message}",
                    "Error al registrarPedidoProducto");
                return -1;
            }
        }

        public static void registrarPedidoCliente(int idCliente, int idPedido)
        {
            int resultado = 0;
            pedidocliente pedidoclienteRegistro = new pedidocliente
            {
                idCliente = idCliente,
                idPedido = idPedido,

            };
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.pedidocliente.Add(pedidoclienteRegistro);
                    resultado = context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }

        public static (pedido pedido, string tipo) obtenerPedidoPorID(int idPedidoEdicion)
        {

            pedido ProductoPedido = null;
            pedidolocal ProductoLocal = null;
            pedidocliente ProductoCliente = null;// Initialize to null
            string tipo = null;
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    // Use Include to load related entities
                    ProductoPedido = context.pedido
                        .FirstOrDefault(p => p.idPedido == idPedidoEdicion);
                    ProductoCliente = context.pedidocliente
                        .FirstOrDefault(p => p.idPedido == idPedidoEdicion);
                    ProductoLocal = context.pedidolocal
                        .FirstOrDefault(p => p.idPedidoLocal == idPedidoEdicion);
                }
                if (ProductoPedido != null && ProductoLocal != null)
                {
                    tipo = "Local";
                }
                else if
                    (ProductoPedido != null && ProductoCliente != null){
                        tipo = "Domicilio";
                    
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return (ProductoPedido, tipo); // Return the loaded pedido or null if not found
        }

        public static int EliminarRelacionesPedidos(int idPedido)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var productosAEliminar = context.pedidoproducto
                        .Where(pp => pp.idPedido == idPedido)
                        .ToList();

                    foreach (var producto in productosAEliminar)
                    {
                        context.pedidoproducto.Remove(producto);
                    }

                    context.SaveChanges();
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
                return -1;
            }
            catch (Exception ex)
            {

                GestorCuadroDialogo.MostrarError($"Ocurrió un error al eliminar los productos: {ex.Message}",
                    "Error al eliminar productos");
                return -1;
            }

            return 0;
        }
       
        public static int incrementarCantidadProductos(List<producto> productosResumen)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    foreach (var productoResumen in productosResumen)
                    {

                        var producto = context.producto.FirstOrDefault(p => p.idProducto == productoResumen.idProducto);

                        if (producto != null)
                        {
                            producto.cantidad += productoResumen.cantidad; // Restar la cantidad
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    context.SaveChanges();
                    return 0;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexión con la base de datos, inténtelo más tarde.",
                    "Sin conexión a la base de datos");
                return -1;
            }
            catch (Exception ex)
            {
                GestorCuadroDialogo.MostrarError($"Ocurrió un error al reducir la cantidad de productos: {ex.Message}",
                    "Error al reducir cantidad");
                return -1;
            }

            return 0;
        }

        public static int EliminarRelacionPedido(int idPedido, int idProducto)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var productosAEliminar = context.pedidoproducto
                        .Where(pp => pp.idPedido == idPedido && pp.idProducto == idProducto)
                        .ToList();
                    if (productosAEliminar.Count == 0)
                    {
                        return -1;
                    }

                    foreach (var producto in productosAEliminar)
                    {
                        context.pedidoproducto.Remove(producto);
                    }

                    context.SaveChanges();
                    return 0;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexión con la base de datos, inténtelo más tarde.",
                    "Sin conexión a la base de datos");
                return -1;
            }
            catch (Exception ex)
            {
                GestorCuadroDialogo.MostrarError($"Ocurrió un error al eliminar los productos: {ex.Message}",
                    "Error al eliminar productos");
                return -1;
            }
        }

        public static int actualizarTotalPedido(decimal nuevoTotal, int idPedido)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var pedido = context.pedido.FirstOrDefault(p => p.idPedido == idPedido);
                    if (pedido == null)
                    {
                        return -1; 
                    }
                    pedido.cantidad = nuevoTotal;
                    context.SaveChanges();
                    return 0;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexión con la base de datos, inténtelo más tarde.",
                    "Sin conexión a la base de datos");
                return -1;
            }
            catch (Exception ex)
            {
                GestorCuadroDialogo.MostrarError($"Ocurrió un error al actualizar el total del pedido: {ex.Message}",
                    "Error al actualizar total");
                return -1;
            }
        }
        public static int incrementarCantidadPedido(int idPedido, producto nuevoProducto)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var existingPedidoProducto = context.pedidoproducto
                        .FirstOrDefault(pp => pp.idPedido == idPedido && pp.idProducto == nuevoProducto.idProducto);
                    Console.WriteLine("Quiero actualizar la relacion de pedido de "+ existingPedidoProducto.idPedido);
                    if (existingPedidoProducto != null)
                    {
                        existingPedidoProducto.cantidad = nuevoProducto.cantidad;
                        Console.WriteLine("Quiero actualizar cantidad de pedido de " + existingPedidoProducto.idPedido + " con " + existingPedidoProducto.cantidad);
                        context.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        throw new Exception("No se encontró el producto en el pedido.");
                        return -1;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                return -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al actualizar la cantidad del producto.", ex);
                return -1;
            }
        }

        public static int decrementarCantidadPedido(int idPedido, producto nuevoProducto)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var existingPedidoProducto = context.pedidoproducto
                        .FirstOrDefault(pp => pp.idPedido == idPedido && pp.idProducto == nuevoProducto.idProducto);

                    if (existingPedidoProducto != null)
                    {
                        if (existingPedidoProducto.cantidad >= nuevoProducto.cantidad)
                        {
                            existingPedidoProducto.cantidad = nuevoProducto.cantidad; 
                            context.SaveChanges();
                            return 0;
                        }
                        else
                        {
                            throw new Exception("No hay suficiente cantidad para decrementar.");
                            return -1;
                        }
                    }
                    else
                    {
                        throw new Exception("No se encontró el producto en el pedido.");
                        return -1;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                return -1;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al actualizar la cantidad del producto.", ex);
                return -1;
            }
        }

        public static int cambiarEstadoPedido(int idPedido, int v)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var producto = context.pedido.FirstOrDefault(p => p.idPedido == idPedido);
                        producto.idEstadoPedido = v;
                    context.SaveChanges();
                    return 0;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexión con la base de datos, inténtelo más tarde.",
                    "Sin conexión a la base de datos");
                return -1;
            }
            catch (Exception ex)
            {
                GestorCuadroDialogo.MostrarError($"Ocurrió un error al reducir la cantidad de productos: {ex.Message}",
                    "Error al reducir cantidad");
                return -1;
            }

            return 0;
        }
    }
}