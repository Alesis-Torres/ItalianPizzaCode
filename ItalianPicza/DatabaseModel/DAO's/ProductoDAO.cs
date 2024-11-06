using ItalianPicza.DatabaseModel.DataBaseMapping;
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
        internal static List<producto> ObtenerListaProductos()
        {
            {
                List<producto> listaProductos;
                try
                {
                    using (var context = new italianpizzaEntities())
                    {
                            listaProductos = context.producto
                                .Select(p => new producto
                                {
                                    nombre = p.nombre,
                                    lote = p.lote,
                                    caducidad = p.caducidad,
                                    tipo = p.tipoproducto.nombre,
                                    proveedor = p.proveedor,
                                    medida = p.medida,
                                    codigo = p.codigo
                                }).ToList();

                        }

                }
                catch (EntityException ex)
                {
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                }

                return listaProductos;
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
    }
}
