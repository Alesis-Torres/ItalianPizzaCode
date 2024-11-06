using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Moq;
using System.Data;
using System.Data.Entity;

namespace ItalianPizzaTest
{
    [TestClass]
    public class ProveedorDAOTest
    {
        [TestMethod]
        public void DarDeAltaNuevoProveedor_DeberiaRetornar1_CuandoProveedorEsAgregado()
        {
            var mockSet = new Mock<DbSet<proveedor>>();
            var mockContext = new Mock<italianpizzaEntities>();
            mockContext.Setup(m => m.proveedor).Returns(mockSet.Object);

            var proveedorDao = new ProveedoresDAO();
            var nuevoProveedor = new proveedor { idProveedor = 1, nombre = "Proveedor de prueba" };

            var resultado = proveedorDao.DarDeAltaNuevoProveedor(nuevoProveedor);

            mockSet.Verify(m => m.Add(It.IsAny<proveedor>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityException))]
        public void DarDeAltaNuevoProveedor_DeberiaLanzarEntityException_CuandoOcurreErrorEnBaseDeDatos()
        {
            var mockContext = new Mock<italianpizzaEntities>();
            mockContext.Setup(m => m.SaveChanges()).Throws(new EntityException());

            var proveedorDao = new ProveedoresDAO();
            var nuevoProveedor = new proveedor { idProveedor = 1, nombre = "Proveedor de prueba" };

            proveedorDao.DarDeAltaNuevoProveedor(nuevoProveedor);

        }
    }
}