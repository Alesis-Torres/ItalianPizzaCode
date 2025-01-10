using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{

    public partial class GUI_Productos : Page
    {
        public GUI_Productos()
        {
            InitializeComponent();
            CargarProductos();
            
        }

        private void irMenuPrincipal(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());
        }

        private void VerReceta(object sender, RoutedEventArgs e)
        {

            Button botonVerReceta = sender as Button;
            producto productoSeleccionado = botonVerReceta.DataContext as producto;

            if (productoSeleccionado != null)
            {
                VentanaPrincipal.CambiarPagina(new GUI_ConsultarReceta(productoSeleccionado));
            }
        }


        private void CargarProductos()
        {
            ProductosDAO productosDAO = new ProductosDAO();

            List<producto> productos = new List<producto>();

            try
            {
                productos = productosDAO.ObtenerProductos();
                lvProductos.ItemsSource = productos;

            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }

        }

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {

        }

        private void irInventario(object sender, RoutedEventArgs e)
        {

        }
    }
}
