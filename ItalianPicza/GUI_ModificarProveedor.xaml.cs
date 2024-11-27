using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.Win32;
using Seguridad;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ItalianPicza
{

    public partial class GUI_ModificarProveedor : Page
    {
        private static int proveedorID;
        private static proveedor proveedorRegistrado;
        private static proveedor proveedorModificado;
        public GUI_ModificarProveedor(int idProveedor)
        {
            proveedorID = idProveedor;
            InitializeComponent();
            configurarDatosProveedor();
            cuadroTextoTelefono.PreviewTextInput += SoloNumeros;
            DataObject.AddPastingHandler(cuadroTextoTelefono, new DataObjectPastingEventHandler(VerificarPegado));
        }

        string nombre, telefono, descripcion, rutaArchivo;
        byte[] imagenProveedor;

        private void seleccionarImagen(object sender, RoutedEventArgs e)
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
                    imagenUsuario.Source = bitmap;
                    imagenProveedor = File.ReadAllBytes(rutaArchivo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }

        private void configurarDatosProveedor()
        {
            ProveedoresDAO proveedoresDAO = new ProveedoresDAO();
            proveedorRegistrado = new proveedor();
            try
            {
                proveedorRegistrado = proveedoresDAO.ObtenerInformacionProveedor(proveedorID);

                if (proveedorRegistrado != null)
                {
                    cuadroTextoNombre.Text = proveedorRegistrado.nombre;
                    cuadroTextoTelefono.Text = proveedorRegistrado.telefono;
                    cuadroTextoDescripcion.Text = proveedorRegistrado.descripcion;
                    cbTipoProducto.SelectedIndex = (int)proveedorRegistrado.idTipoProducto - 1;

                    if (proveedorRegistrado.imagen != null)
                    {
                        imagenUsuario.Source = ConvertirBytesAImagen(proveedorRegistrado.imagen);
                    }

                    cuadroTextoNombre.IsReadOnly = true;
                    cbTipoProducto.IsEnabled = false; 
                    btnCargarImagen.IsEnabled = false; 
                }
                else
                {
                    GestorCuadroDialogo.MostrarError(
                        "La información del Proveedor no pudo ser recuperada de manera correcta en el sistema",
                        "Recuperación fallida");
                }
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError(
                    "No hay conexión con la base de datos, por favor, inténtelo más tarde",
                    "Sin conexión a la base de datos");
            }
        }


        private void GuardarProveedor(object sender, RoutedEventArgs e)
        {
            telefono = cuadroTextoTelefono.Text.Trim();
            descripcion = cuadroTextoDescripcion.Text.Trim();
            bool estado = verificarEstado();

            if (!ExistenCamposVacíosProveedor() && !ExistenCamposInvalidosProveedor())
            {
                proveedorModificado = new proveedor()
                {
                    nombre = proveedorRegistrado.nombre,
                    telefono = telefono,
                    descripcion = descripcion,
                    idTipoProducto = cbTipoProducto.SelectedIndex >= 0 ? cbTipoProducto.SelectedIndex + 1 : proveedorRegistrado.idTipoProducto,
                    imagen = imagenProveedor ?? proveedorRegistrado.imagen
                };
                string telefonoAVerificar = proveedorModificado.telefono;
                string descripcionAVerificar = proveedorModificado.descripcion;
                if (DatosIguales(telefonoAVerificar, descripcionAVerificar))
                {
                    GestorCuadroDialogo.MostrarAdvertencia(
                    "Los valores son los mismos, por favor, registre nuevos datos si asi lo requiere.",
                    "Mismos valores");
                }
                else
                {
                    ProveedoresDAO proveedoresDAO = new ProveedoresDAO();

                    try
                    {
                        int resultado = proveedoresDAO.ModificarProveedor(proveedorID, proveedorModificado);

                        if (resultado == 1)
                        {
                            GestorCuadroDialogo.MostrarInformacion
                                ("Proveedor modificado de manera exitosa en el sistema",
                                "Proveedor modificado");
                            VentanaPrincipal.CambiarPagina(new GUI_Proveedores());
                        }
                        else
                        {
                            GestorCuadroDialogo.MostrarError
                                ("El Proveedor no pudo ser modificado de manera correcta en el sistema",
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
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Proveedores());
        }

        private BitmapImage ConvertirBytesAImagen(byte[] imageData)
        {
            using (var stream = new System.IO.MemoryStream(imageData))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        private bool verificarEstado()
        {
            bool estado = false;

            if (cbTipoProducto.SelectedIndex <= 0)
            {
                estado = true;
            }

            return estado;
        }

        private bool ExistenCamposInvalidosProveedor()
        {
            bool hayCamposInvalidos = false;

            if (!hayCamposInvalidos && ValidarDatos.ExistenCaracteresInvalidosParaDescripcion(descripcion))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "La Descripción no es válida, por favor, ingresela nuevamente.",
                    "Descripción inválida");
                hayCamposInvalidos = true;
            }

            return hayCamposInvalidos;
        }

        private bool ExistenCamposVacíosProveedor()
        {
            bool existenCamposVacios = false;

            if (ValidarDatos.EsCadenaVacia(telefono) || ValidarDatos.EsCadenaVacia(descripcion))
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                "Campos vacíos");
            }

            return existenCamposVacios;
        }

        private bool DatosIguales(string telefono, string descripcion)
        {
            bool sonIguales =
            proveedorRegistrado.telefono == proveedorModificado.telefono &&
            proveedorRegistrado.descripcion == proveedorRegistrado.descripcion;
            return sonIguales;
        }

        private void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !EsNumero(e.Text);
        }

        private bool EsNumero(string texto)
        {
            return int.TryParse(texto, out _);
        }

        private void VerificarPegado(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string textoPegado = (string)e.DataObject.GetData(typeof(string));
                if (!EsNumero(textoPegado))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
