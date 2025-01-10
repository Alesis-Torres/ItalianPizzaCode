using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.DatabaseModel.DAO_s;
using System.Transactions;
using System.Collections.Generic;

namespace Pruebas.PruebasDAOs
{
    [TestClass]
    public class PruebaProveedoresDAO
    {
        private static ProveedoresDAO proveedorDAO;
        private static proveedor proveedorRegistrado;
        private static proveedor proveedorParaRegistrar;
        private static proveedor proveedorParaModificar;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            using (var scope = new TransactionScope())
            {
                proveedorDAO = new ProveedoresDAO();

                // Inicializar proveedor para registrar
                proveedorParaRegistrar = new proveedor()
                {
                    idProveedor = 13,
                    nombre = "Farmacias Guadalajara",
                    descripcion = "Esto es una prueba para registrar",
                    telefono = "56465675",
                    imagen = null,
                    idTipoProducto = 1
                };

                // Registrar un proveedor para simular un "existente"
                proveedorDAO.DarDeAltaNuevoProveedor(proveedorParaRegistrar);
                proveedorRegistrado = proveedorParaRegistrar; // Asignar el proveedor registrado para pruebas

                // Inicializar proveedor para modificar
                proveedorParaModificar = new proveedor()
                {
                    nombre = "Verduleria Jimenez",
                    descripcion = "Esto es una prueba para modificar",
                    telefono = "45216734",
                    idTipoProducto = 2
                };

                Assert.IsNotNull(proveedorRegistrado, "El proveedor registrado no fue inicializado correctamente.");

                // Completar la transacción
                scope.Complete();
            }
        }

        [TestMethod]
        public void PruebaObtenerProveedores()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<proveedor> proveedores = proveedorDAO.ObtenerProveedores();
                Assert.IsTrue(proveedores.Count > 0, "No se encontraron proveedores en la base de datos.");
            }
        }

        [TestMethod]
        public void PruebaModificarProveedor()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Modificar el proveedor registrado
                int resultado = proveedorDAO.ModificarProveedor(proveedorRegistrado.idProveedor, proveedorParaModificar);
                Assert.IsTrue(resultado > 0, "No se modificó ningún registro en la base de datos.");
            }
        }
    }
}