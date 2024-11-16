using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Pruebas.PruebasDAOs
{
    [TestClass]
    public class PruebaRecetasDAO
    {
        private static RecetaDAO recetaDAO;
        private static receta recetaParaRegistrar;
        private static receta recetaRegistrada;
        private static List<ingrediente> ingredientesPrueba;
        private static producto productoPrueba;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            recetaDAO = new RecetaDAO();
            ingredientesPrueba = new List<ingrediente>
            {
                new ingrediente { idIngrediente = 1, nombre = "Queso", cantidad = "200" },
                new ingrediente { idIngrediente = 2, nombre = "Tomate", cantidad = "500" },
            };

            recetaRegistrada = new receta
            {
                idReceta = 4
            };
            recetaParaRegistrar = new receta()
            {
                instrucciones = "InstruccionesPrueba",
                descripcion = "DescripcionPrueba"
            };
            productoPrueba = new producto()
            {
                idProducto = 7,
                nombre = "Pizza de salchicha",
                tipo = "Alimento"
            };
        }


        [TestMethod]
        public void PruebaDarDeAltaReceta()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int resultado = recetaDAO.DarDeAltaReceta(recetaParaRegistrar.instrucciones, ingredientesPrueba, productoPrueba);
                Assert.AreEqual(3, resultado);
            }
        }

        [TestMethod]
        public void PruebaObtenerIngredientesReceta()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<ingrediente> ingredientesReceta = recetaDAO.ObtenerIngredientesReceta(recetaRegistrada.idReceta);
                Assert.IsTrue(ingredientesReceta.Count > 0);
            }
        }

        [TestMethod]
        public void PruebaObtenerRecetaProducto()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                receta resultado = recetaDAO.ObtenerRecetaProducto(recetaRegistrada.idReceta);
                Assert.IsNotNull(resultado);
            }
        }

    }
}
