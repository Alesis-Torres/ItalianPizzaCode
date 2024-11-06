using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    public partial class GUI_PedidosProveedores : Page
    {
        public GUI_PedidosProveedores()
        {
            InitializeComponent();
            List<PedidoProveedor> pedidosProveedor = new List<PedidoProveedor>()
            {
                new PedidoProveedor() { NombreProveedor = "Molinos Italia", FechaRealizacionPedidoProveedor = "07/05/2023" , CostoPedidoProveedor = "$ 264.21" },
                new PedidoProveedor() { NombreProveedor = "Lacteos Frescos S.A.", FechaRealizacionPedidoProveedor = "15/07/2023", CostoPedidoProveedor = "$ 121.10" },
                new PedidoProveedor() { NombreProveedor = "Huerta Organica", FechaRealizacionPedidoProveedor = "02/07/2023", CostoPedidoProveedor = "$ 824.04" },
                new PedidoProveedor() { NombreProveedor = "Carnes Selectas", FechaRealizacionPedidoProveedor = "24/11/2023", CostoPedidoProveedor = "$ 454.21" },
            };

            lvPedidosProveedores.ItemsSource = pedidosProveedor;
        }

        private void VerDetallesPedidoProveedor(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_VerDetallesPedidoProveedores());
        }

        public class PedidoProveedor
        {
            public string NombreProveedor { get; set; }
            public string FechaRealizacionPedidoProveedor { get; set; }
            public string CostoPedidoProveedor { get; set; }
        }

        private void AgregarPedido(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_AgregarPedidoProveedor());
        }
    }
}
