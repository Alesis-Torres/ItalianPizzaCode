using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Microsoft.Win32;
using Seguridad;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ItalianPicza
{
    public partial class GUI_AgregarProveedores : Page
    {
        public GUI_AgregarProveedores()
        {
            InitializeComponent();
            cuadroTextoTelefono.PreviewTextInput += SoloNumeros;
            DataObject.AddPastingHandler(cuadroTextoTelefono, new DataObjectPastingEventHandler(VerificarPegado));

        }

        string nombre, telefono, descripcion, rutaArchivo;
        byte[] imagenProveedor;

        private void GuardarProveedor(object sender, RoutedEventArgs e)
        {
            nombre = cuadroTextoNombre.Text.Trim();
            telefono = cuadroTextoTelefono.Text.Trim();
            descripcion = cuadroTextoDescripcion.Text.Trim();
            bool estado = verificarEstado();

            if (!ExistenCamposVacíosProveedor() && !ExistenCamposInvalidosProveedor())
            {
                proveedor proveedor = new proveedor()
                {
                    nombre = nombre,
                    telefono = telefono,
                    descripcion = descripcion,
                    idTipoProducto = cbTipoProducto.SelectedIndex,
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
                        VentanaPrincipal.CambiarPagina(new GUI_Proveedores());
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

        private bool ExistenCamposInvalidosProveedor()
        {
            bool hayCamposInvalidos = false;

            if (ValidarDatos.ExistenCaracteresInvalidosParaNombre(nombre))
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

            if (ValidarDatos.EsCadenaVacia(nombre) || ValidarDatos.EsCadenaVacia(telefono)
                || cbTipoProducto.SelectedIndex <= 0 || ValidarDatos.EsCadenaVacia(descripcion) || rutaArchivo == null)
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                "Campos vacíos");
            }

            return existenCamposVacios;
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
