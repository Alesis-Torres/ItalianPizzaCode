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
        /*
        public int GuardarProductoPedidoProveedor(pedidoproveedorproducto productoPedido)
        {
            int resultado = 0;

            try
            {
                using (var context = new italianpizzaEntities())
                {
                    context.pedidoproveedorproducto.Add(productoPedido);
                    resultado = context.SaveChanges();
                }

                return resultado;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Error al guardar los productos del pedido en la base de datos.", ex);
            }
        }

        public List<pedidoproveedor> ObtenerTodosLosPedidos()
        {
            try
            {
                using (var context = new italianpizzaEntities())
                {
                    return context.pedidoproveedor.Include("pedidoproveedoringrediente").Include("pedidoproveedorproducto").ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Error al obtener los pedidos de proveedores.", ex);
            }
        }
        */
    }
}
