using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    public partial class GUI_Pedidos : Page
    {
        public GUI_Pedidos()
        {
            InitializeComponent();
        }
        private void VerPedidosCliente(object sender, RoutedEventArgs e)
        {
            //VentanaPrincipal.CambiarPagina(new GUI_PedidosClientes());
        }

        private void VerPedidosProveedor(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_PedidosProveedores());
        }

        private void irRegresar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());
        }
    }
}
