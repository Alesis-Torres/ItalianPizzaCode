using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    public partial class GUI_PedidosProveedores : Page
    {

        //private List<pedidoproveedor> pedidosproveedores;
        public GUI_PedidosProveedores()
        {
            InitializeComponent();
            CargarPedidosProveedores();
        }

        private void Modificar(object sender, RoutedEventArgs e)
        {

            Button botonModificarProveedor = sender as Button;
            pedidoproveedor pedidoproveedorSeleccionado = botonModificarProveedor.DataContext as pedidoproveedor;

            if (pedidoproveedorSeleccionado != null)
            {
                VentanaPrincipal.CambiarPagina(new GUI_ModificarUsuario((int)pedidoproveedorSeleccionado.idPedidoProveedor));
            }
        }

        private void VerDetalles(object sender, RoutedEventArgs e)
        {

            Button botonVerDetallesProveedor = sender as Button;
            pedidoproveedor pedidoproveedorSeleccionado = botonVerDetallesProveedor.DataContext as pedidoproveedor;

            if (pedidoproveedorSeleccionado != null)
            {
                VentanaPrincipal.CambiarPagina(new GUI_VerDetallesPedidoProveedores((int)pedidoproveedorSeleccionado.idPedidoProveedor));
            }
        }

        private void CargarPedidosProveedores()
        {
            PedidoProveedorDAO pedidosproveedoresDAO = new PedidoProveedorDAO();

           List<pedidoproveedor> pedidosproveedores = new List<pedidoproveedor>();

            try
            {
                pedidosproveedores = pedidosproveedoresDAO.ObtenerPedidosProveedores();
                lvPedidosProveedores.ItemsSource = pedidosproveedores;

            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }
        }

        private void AgregarPedido(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_AgregarPedidoProveedor());
        }

        private void ConfirmarPedido(object sender, RoutedEventArgs e)
        {
            //TO DO
        }

        private void irRegresar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());
        }
    }
}
