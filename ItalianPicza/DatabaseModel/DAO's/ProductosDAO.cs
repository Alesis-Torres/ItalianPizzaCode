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

    }

}
