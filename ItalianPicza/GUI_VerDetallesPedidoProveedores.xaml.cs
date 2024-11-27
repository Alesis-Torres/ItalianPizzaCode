using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    public partial class GUI_VerDetallesPedidoProveedores : Page
    {
        private static int pedidoproveedorID;
        private static pedidoproveedor pedidoproveedorRegistrado;
        public GUI_VerDetallesPedidoProveedores(int idPedidoProveedor)
        {
            pedidoproveedorID = idPedidoProveedor;
            InitializeComponent();

            List<DetallesPedidoProveedor> detallesPedidoProveedor = new List<DetallesPedidoProveedor>()
            {
                new DetallesPedidoProveedor() { NombreProducto = "Tomate Saladet", CantidadProducto = "3" , UnidaMedidaPedido = "Kilogramo", CostoUnidadPedido = "$ 12.00" },
                new DetallesPedidoProveedor() { NombreProducto = "Aguacatae", CantidadProducto = "4", UnidaMedidaPedido = "Kilogramo", CostoUnidadPedido = "$ 40.00" },
                new DetallesPedidoProveedor() { NombreProducto = "Jitomate Saladet", CantidadProducto = "4", UnidaMedidaPedido = "Kilogramo", CostoUnidadPedido = "$ 20.50"},
                new DetallesPedidoProveedor() { NombreProducto = "Cebollin", CantidadProducto = "4", UnidaMedidaPedido = "Kilogramo", CostoUnidadPedido = "$ 25.00" },
            };

            lvPedidoProveedor.ItemsSource = detallesPedidoProveedor;
        }

        private void irRegresar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());
        }

        public class DetallesPedidoProveedor
        {
            public string NombreProducto { get; set; }
            public string CantidadProducto { get; set; }
            public string UnidaMedidaPedido { get; set; }
            public string CostoUnidadPedido { get; set; }
        }

        private void CancelarPedido(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}