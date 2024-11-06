using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.Win32;
using Seguridad;
using System;
using System.Data;
using System.IO;
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

        string nombreProveedor, telefono, descripcion, rutaArchivo;
        byte[] imagenProveedor;

        private void GuardarProveedor(object sender, RoutedEventArgs e)
        {
            nombreProveedor = cuadroTextoNombre.Text.Trim();
            telefono = cuadroTextoTelefono.Text.Trim();
            descripcion = cuadroTextoDescripcion.Text.Trim();

            if (!ExistenCamposVacíosProveedor() && !ExistenCamposInvalidosProveedor())
            {
                proveedor proveedor = new proveedor()
                {
                    nombre = nombreProveedor,
                    telefono = telefono,
                    descripcion = descripcion,
                    imagen = imagenProveedor
                };

                ProveedoresDAO proveedoresDAO = new ProveedoresDAO();

                try
                {

                    int resultado = proveedoresDAO.DarDeAltaNuevoProveedor(proveedor);

                    if (resultado == 1)
                    {
                        GestorCuadroDialogo.MostrarInformacion
                            ("Proveedor registrado de manera exitosa en el sistema",
                            "Proveedor dado de alta");
                        VentanaPrincipal.CambiarPagina(new GUI_Usuarios());
                    }
                    else
                    {
                        GestorCuadroDialogo.MostrarError
                            ("El Proveedor no pudo ser registrado de manera correcta en el sistema",
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

        private bool ExistenCamposInvalidosProveedor()
        {
            bool hayCamposInvalidos = false;

            if (ValidarDatos.ExistenCaracteresInvalidosParaNombre(nombreProveedor))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El Nombre es inválido, por favor, ingreselo nuevamente.",
                    "Nombre inválido");
                hayCamposInvalidos = true;
            }

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

            if (ValidarDatos.EsCadenaVacia(nombreProveedor) || ValidarDatos.EsCadenaVacia(telefono)
                || ValidarDatos.EsCadenaVacia(descripcion) || rutaArchivo == null)
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                "Campos vacíos");
            }

            return existenCamposVacios;
        }
    }
}
