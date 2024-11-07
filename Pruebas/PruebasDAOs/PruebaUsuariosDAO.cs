using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.DatabaseModel.DAO_s;
using System.Transactions;
using System.Collections.Generic;

namespace Pruebas.PruebasDAOs
{
    [TestClass]
    public class PruebaUsuariosDAO
    {
        private static UsuariosDAO usuarioDAO;
        private static usuario usuarioParaRegistrar;
        private static usuario usuarioRegistrado;
        private static empleado empleadoParaRegistrar;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            usuarioDAO = new UsuariosDAO();
            usuarioRegistrado = new usuario
            {
                nombreUsuario = "ItalianLuis",
                password = "Xhantusz17"
            };
            empleadoParaRegistrar = new empleado()
            {
                nombre = "Luis",
                apellidoPaterno = "Elizalde",
                apellidoMaterno = "Arroyo" ,
                email = "luis@gmail.com" ,
                telefono = "2222222222",
                idRol = 1,
                estado = true
            };
            usuarioParaRegistrar = new usuario
            {
                nombreUsuario = "ItalianPrueba",
                password = "Italian17@"
            };
        }

        [TestMethod]
        public void PruebaRegistrarUsuarioValido()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int resultado = usuarioDAO.DarDeAltaNuevoUsuario(empleadoParaRegistrar, usuarioParaRegistrar);
                Assert.AreEqual(1, resultado);
            }
        }

        [TestMethod]
        public void PruebaObtenerEmpleados()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<empleado> empleados = usuarioDAO.ObtenerEmpleados();
                Assert.IsTrue(empleados.Count > 0);
            }
        }

        [TestMethod]
        public void PruebaVerificarUsuarioExistente()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                usuario resultado = usuarioDAO.verificarExistenciaDeUsuario(usuarioRegistrado);

                Assert.IsNotNull(resultado, "El usuario no fue encontrado en la base de datos, pero debería existir.");
            }
        }

        [TestMethod]
        public void PruebaConsultarUsuariosPorRol()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<empleado> empleadosGerentes = usuarioDAO.ObtenerEmpleadosPorRol(1);
                List<empleado> empleadosCocineros = usuarioDAO.ObtenerEmpleadosPorRol(2);
                Assert.IsTrue(empleadosGerentes.Count > 0);
                Assert.IsTrue(empleadosCocineros.Count > 0);
            }
        }

    }
}
