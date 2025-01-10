using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    public partial class GUI_Proveedores : Page
    {
        private List<proveedor> proveedores;
        public GUI_Proveedores()
        {
            InitializeComponent();
            CargarProveedores();
        }
        private void CargarProveedores()
        {
            ProveedoresDAO proveedoresDAO= new ProveedoresDAO();

            try
            {
                proveedores = proveedoresDAO.ObtenerProveedores();
                lvProveedores.ItemsSource = proveedores;

            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }

        }
        private void AgregarProveedor(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_AgregarProveedores());
        }

        private void Modificar(object sender, RoutedEventArgs e)
        {

            Button botonModificar = sender as Button;
            proveedor proveedorSeleccionado = botonModificar.DataContext as proveedor;

            if (proveedorSeleccionado != null)
            {
                VentanaPrincipal.CambiarPagina(new GUI_ModificarProveedor((int)proveedorSeleccionado.idProveedor));
            }
        }

        private void irRegresar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());
        }

    }
}
