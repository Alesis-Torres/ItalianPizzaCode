using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ItalianPicza.DatabaseModel.DAO_s
{
    public class PedidoProveedorDAO
    {
        public PedidoProveedorDAO() { }

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
    }
}
