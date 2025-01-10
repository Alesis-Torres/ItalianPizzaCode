using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class ProductosDAO
    {
        public ProductosDAO() { }   

        public List<producto> ObtenerProductos()
        {
            List<producto> productos = new List<producto>();

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    productos = context.producto.ToList();
                }

            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }

            return productos;
        }

        public int GenerarReporteProducto(producto producto)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    var productoExistente = context.producto.FirstOrDefault(p => p.idProducto == producto.idProducto);

                    if (productoExistente == null)
                    {
                        throw new Exception("El Producto  con el ID especificado no existe.");
                    }
                    productoExistente.cantidadActual = producto.cantidadActual;
                    productoExistente.observacion = producto.observacion;
                    resultado = context.SaveChanges();
                }
                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al modificar el proveedor.", ex);
            }
        }
    }
}
