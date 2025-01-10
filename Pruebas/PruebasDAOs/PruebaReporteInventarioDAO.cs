using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Pruebas.PruebasDAOs
{
    internal class PruebaReporteInventarioDAO
    {
        public static InsumosDAO insumosDAO;
        public static ProductosDAO productosDAO;

        [TestMethod]
        public void PruebaObtenerInsumos()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                insumosDAO = new InsumosDAO();
                List<ingrediente> ingredientesPrueba = insumosDAO.ObtenerIngredientes();
                Assert.IsTrue(ingredientesPrueba.Count > 0);
            }
        }

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
