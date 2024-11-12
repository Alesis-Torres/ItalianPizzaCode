using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Pruebas.PruebasDAOs
{
    [TestClass]
    public class PruebaProductosDAO
    {
        public static ProductosDAO productosDAO;

        [TestMethod]
        public void PruebaObtenerProductos()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                productosDAO = new ProductosDAO();
                List<producto> listaProductos = productosDAO.ObtenerProductos();
                Assert.IsTrue(listaProductos.Count > 0);
            }
        }
    }
}
