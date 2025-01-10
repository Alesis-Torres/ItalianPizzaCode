using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.DatabaseModel.DAO_s;
using System.Transactions;
using System.Collections.Generic;

namespace Pruebas.PruebasDAOs
{
    [TestClass]
    public class PruebaPedidoProveedoresDAO
    {
        private static PedidoProveedorDAO pedidoproveedorDAO;
        private static pedidoproveedor pedidoProveedorParaRegistrar;
        private static pedidoproveedor pedidoproveedorParaModificar;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            pedidoproveedorDAO = new PedidoProveedorDAO();
            pedidoProveedorParaRegistrar = new pedidoproveedor()

            {
                idPedidoProveedor = 11,
                fecha = "2024-11-11",
                monto = null,
                estado = "Pendiente",
                idProveedor = 1,
                idEmpleado = null
            };
            pedidoproveedorParaModificar = new pedidoproveedor()
            {
                fecha = "2024-12-12",
                monto = null,
                estado = "Finalizado",
                idProveedor = 1,
                idEmpleado = null
            };
        }

        [TestMethod]
        public void PruebaObtenerPedidosProveedores()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<pedidoproveedor> pedidosproveedores = pedidoproveedorDAO.ObtenerPedidosProveedores();
                Assert.IsTrue(pedidosproveedores.Count > 0);
            }
        }
    }
}