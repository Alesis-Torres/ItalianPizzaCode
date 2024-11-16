using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.Win32;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItalianPicza
{
    /// <summary>
    /// Interaction logic for GUI_DarAltaProducto.xaml
    /// </summary>
    public partial class GUI_DarAltaProducto : Page
    {
        string nombreProducto, lote, caducidad, rutaArchivo;
        int codigo,idMedida, idProveedor, idTipo;
        byte[] imagenEmpleado;
        public GUI_DarAltaProducto()
        {
            InitializeComponent();
        }

        private void GuardarUsuario(object sender, RoutedEventArgs e)
        {
            nombreProducto = cuadroTextoNombre.Text.Trim();
            lote = cuadroTextoLote.Text.Trim();
            codigo = int.Parse(cuadroTextoCodigo.Text.Trim());
            idMedida = cbMedida.SelectedIndex;
            idProveedor = cbTipo.SelectedIndex;
            idTipo = cbMedida.SelectedIndex;
            if (!ExistenCamposVaciosProducto())
            {

                producto nuevoProducto = new producto()
                {
                    nombre = nombreProducto,
                    lote = lote,
                    codigo = codigo,
                    idMedidaProducto = idMedida,
                    idProveedor = idProveedor,
                    idTipoProducto = idTipo
                };

                ProductoDAO productoDAO = new ProductoDAO();

                try
                {

                    int resultado = productoDAO.DarDeAltaNuevoProducto(nuevoProducto);

                    if (resultado == 1)
                    {
                        GestorCuadroDialogo.MostrarInformacion
                            ("Producto registrado de manera exitosa en el sistema",
                            "Producto dado de alta");
                        VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
                    }
                    else
                    {
                        GestorCuadroDialogo.MostrarError
                            ("El producto no pudo ser registrado de manera correcta en el sistema",
                            "Registro fallido");
                    }

                }
                catch (EntityException)
                {
                    GestorCuadroDialogo.MostrarError
                            ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                            "Sin conexión a la base de datos");
                }

            }

        }
        private bool ExistenCamposVaciosProducto()
        {
            bool hayCamposVacios = false;

            if (ValidarDatos.EsCadenaVacia(nombreProducto) || ValidarDatos.EsCadenaVacia(lote)
              || ValidarDatos.EsCadenaVacia(caducidad) || ValidarDatos.EsCadenaVacia(codigo.ToString())
              || cbMedida.SelectedIndex == 0 ||cbTipo.SelectedIndex == 0 || cbProveedor.SelectedIndex == 0)
            {
                hayCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                   "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                   "Campos vacíos");
            }

            return hayCamposVacios;
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

                rutaArchivo = openFileDialog.FileName;

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(rutaArchivo);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    imagenProducto.Source = bitmap;

                    imagenEmpleado = File.ReadAllBytes(rutaArchivo);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }

    }
}
