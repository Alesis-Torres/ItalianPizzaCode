using Microsoft.Win32;
using Seguridad;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ItalianPicza
{
    public partial class GUI_AgregarProveedores : Page
    {
        public GUI_AgregarProveedores()
        {
            InitializeComponent();
        }

        private void GuardarProveedor(object sender, RoutedEventArgs e)
        {
            string nombre = cuadroTextoNombre.Text;
            string telefono = cuadroTextoTelefono.Text;
            string descripcion = cuadroTextoDescripcion.Text;

            if (!ValidarDatos.EsCadenaVacia(nombre) && !ValidarDatos.EsCadenaVacia(telefono) && !ValidarDatos.EsCadenaVacia(descripcion))
            {
                //DAO Dar alta usuario  -- Pendiente

                GestorCuadroDialogo.MostrarInformacion
                ("Se ha creado el Proveedor de manera exitosa en el sistema",
                "Proveedor dado de alta");
                VentanaPrincipal.CambiarPagina(new GUI_Proveedores());
            }
            else
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "Los campos están vacíos, por favor, ingrese la información solicitada.",
                    "Campos vacíos");
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Proveedores());
        }

        private void SeleccionarImagen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de imagen (*.png;*.jpg)|*.png;*.jpg",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(filePath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    imagenProveedor.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }
    }
}
