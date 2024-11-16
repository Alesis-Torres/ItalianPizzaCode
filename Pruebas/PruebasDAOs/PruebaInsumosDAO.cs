using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Pruebas.PruebasDAOs
{
    [TestClass]
    public class PruebaInsumosDAO
    {
        public static InsumosDAO insumosDAO;

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
    }
}
