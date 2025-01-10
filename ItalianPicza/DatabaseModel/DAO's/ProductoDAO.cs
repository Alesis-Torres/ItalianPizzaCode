
using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.DatabaseModel.DTO;
using ItalianPicza.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    internal class ProductoDAO
    {
        public static List<producto> ObtenerListaProductos()
        {
            List<producto> productos = new List<producto>();
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var listaProductos = context.producto.ToList();
                    productos = listaProductos;
                }
            }
            catch (InvalidOperationException)
            {
                GestorCuadroDialogo.MostrarError("No hay conexion con la base de datos, intentelo más tarde.",
                    "Sin conexion a la base de datos");
            }
            return productos;
        }

        public static int incrementarCantidadProducto(producto nuevoProducto, int cantidad)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var existingProduct = context.producto.FirstOrDefault(p => p.idProducto == nuevoProducto.idProducto);
                    Console.WriteLine("Encontre " + existingProduct.nombre);
                    if (existingProduct != null)
                    {
                        Console.WriteLine("voy a actualizar con " + (existingProduct.cantidad ?? 0) + nuevoProducto.cantidad);
                        existingProduct.cantidad = (existingProduct.cantidad ?? 0) + cantidad;
                        resultado = context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Producto no encontrado.");
                    }
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al actualizar la cantidad del producto.", ex);
            }
        }

        

        public int DarDeAltaNuevoProducto(producto nuevoProducto)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.producto.Add(nuevoProducto);
                    resultado = context.SaveChanges();
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
        }
        public static int reducirCantidadProductos(List<producto> listaResumenDTO)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    foreach (var productoResumen in listaResumenDTO)
                    {

                        var producto = context.producto.FirstOrDefault(p => p.idProducto == productoResumen.idProducto);

                        if (producto != null)
                        {
                            if (producto.cantidad >= productoResumen.cantidad)
                            {
                                producto.cantidad -= productoResumen.cantidad;
                            }
                            else
                            {
                                return -2;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    context.SaveChanges();
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

        public static int ReducirCantidadProducto(producto productoResumen, int cantidad)
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var producto = context.producto.FirstOrDefault(p => p.idProducto == productoResumen.idProducto);

                    if (producto != null)
                    {
                        if (producto.cantidad >= cantidad)
                        {
                            producto.cantidad = producto.cantidad - cantidad;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -1;
                    }

                    context.SaveChanges();
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
                                       Precio = (decimal)i.cantidad,
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
