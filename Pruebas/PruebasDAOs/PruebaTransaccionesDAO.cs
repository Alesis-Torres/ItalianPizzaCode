using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Transactions;

namespace Pruebas.PruebasDAOs
{
    [TestClass]
    public class PruebaTransaccionesDAO
    {
        private static TransaccionesDAO transaccionesDAO;
        private static entradaextraordinaria entradaParaRegistrar;
        private static salidaextraordinaria salidaParaRegistrar;
        private static cortecaja caja;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            transaccionesDAO = new TransaccionesDAO();
            entradaParaRegistrar = new entradaextraordinaria
            {
                cantidad = 100,
                fechaRealizacion = new DateTime(2024, 11, 23),
                idEmpleado = 11,
                idCorteCaja = 2,
                descripcion = "Pedido de Tlachy"
            };

            salidaParaRegistrar = new salidaextraordinaria
            {
                cantidad = 100,
                fechaRealizacion = new DateTime(2024, 11, 23),
                idEmpleado = 11,
                idCorteCaja = 2,
                descripcion = "Pedido de Tlachy"
            };
            caja = new cortecaja
            {
                idCorteCaja = 1,
                balance = 500
            };
        }


        [TestMethod]
        public void PruebaRegistrarEntrada()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions 
            { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int resultado = transaccionesDAO.RegistrarTransaccionEntrada(entradaParaRegistrar);
                Assert.AreEqual(1, resultado);
            }
        }

        [TestMethod]
        public void PruebaRegistrarSalida()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions 
            { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int resultado = transaccionesDAO.RegistrarTransaccionSalida(salidaParaRegistrar);
                Assert.AreEqual(1, resultado);
            }
        }

        [TestMethod]
        public void PruebaObtenerCorteCaja()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                cortecaja resultado = transaccionesDAO.ObtenerCorteCajaActual();
                Assert.IsNotNull(resultado);
            }
        }

        [TestMethod]
        public void PruebaRegistrarEntradaEnCaja()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                bool resultado = transaccionesDAO.ActualizarBalanceCorteCajaEntrada(caja.idCorteCaja, 100);
                Assert.IsTrue(resultado);
            }
        }

        [TestMethod]
        public void PruebaRegistrarSalidaEnCaja()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                bool resultado = transaccionesDAO.ActualizarBalanceCorteCajaSalida(caja.idCorteCaja, 100);
                Assert.IsTrue(resultado);
            }
        }
    }
}
