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
    public partial class GUI_ModificarUsuario : Page
    {
        private static int usuarioID;
        private static string usuarioNombre;
        private static empleado empleadoRegistrado;
        private static empleado empleadoModificado;

        public GUI_ModificarUsuario(int idUsuario)
        {
            usuarioID = idUsuario;
            InitializeComponent();
            configurarDatosUsuario();
            cuadroTextoTelefono.PreviewTextInput += SoloNumeros;
            DataObject.AddPastingHandler(cuadroTextoTelefono, new DataObjectPastingEventHandler(VerificarPegado));
        }

        string nombre, apellidoPaterno, apellidoMaterno, nombreUsuario,
            telefono, email, rutaArchivo;
        byte[] imagenEmpleado;

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

                    imagenEmpleado = File.ReadAllBytes(rutaArchivo);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }

        private void configurarDatosUsuario()
        {
            UsuariosDAO usuariosDAO = new UsuariosDAO();
            empleadoRegistrado = new empleado();
            try
            {
                (empleadoRegistrado, usuarioNombre) = usuariosDAO.ObtenerInformacionEmpleado(usuarioID);

                if (empleadoRegistrado != null)
                {
                    cuadroTextoNombre.Text = empleadoRegistrado.nombre;
                    cuadroTextoTelefono.Text = empleadoRegistrado.telefono;
                    cuadroTextoCorreo.Text = empleadoRegistrado.email;
                    cuadroTextoApellidoPaterno.Text = empleadoRegistrado.apellidoPaterno;
                    cuadroTextoApellidoMaterno.Text = empleadoRegistrado.apellidoMaterno;
                    cbRol.SelectedIndex = (int)empleadoRegistrado.idRol - 1;
                    cuadroTextoNombreUsuario.Text = usuarioNombre;
                    imagenEmpleado = empleadoRegistrado.imagen;

                    if (empleadoRegistrado.imagen != null)
                    {
                        imagenUsuario.Source = ConvertirBytesAImagen(empleadoRegistrado.imagen);
                    }
                }
                else
                {
                    GestorCuadroDialogo.MostrarError(
                        "La información del usuario no pudo ser recuperada de manera correcta en el sistema",
                        "Recuperación fallida");
                }
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError(
                    "No hay conexión con la base de datos, por favor, intentelo más tarde",
                    "Sin conexión a la base de datos");
            }
        }


        private void Guardar(object sender, RoutedEventArgs e)
        {
            nombreUsuario = cuadroTextoNombreUsuario.Text.Trim();
            nombre = cuadroTextoNombre.Text.Trim();
            apellidoPaterno = cuadroTextoApellidoPaterno.Text.Trim();
            apellidoMaterno = cuadroTextoApellidoMaterno.Text.Trim();
            nombreUsuario = cuadroTextoNombreUsuario.Text.Trim();
            telefono = cuadroTextoTelefono.Text.Trim();
            email = cuadroTextoCorreo.Text.Trim();

            if (!ExistenCamposVaciosUsuario() && !ExistenCamposInvalidosUsuario()
                && !ExistenCamposVacíosEmpleado() && !ExistenCamposInvalidosEmpleado())
            {

                usuario usuario = new usuario()
                {
                    nombreUsuario = nombreUsuario
                };

                empleadoModificado = new empleado()
                {
                    nombre = nombre,
                    apellidoPaterno = apellidoPaterno,
                    apellidoMaterno = apellidoMaterno,
                    email = email,
                    telefono = telefono,
                    idRol = cbRol.SelectedIndex + 1,
                    imagen = imagenEmpleado
                };

                string nombreAVerificar = usuario.nombreUsuario;

                if (DatosIguales(nombreAVerificar))
                {
                    GestorCuadroDialogo.MostrarAdvertencia(
                    "Los valores son los mismos, por favor, registre nuevos datos si asi lo requiere.",
                    "Mismos valores");
                }
                else
                {
                    UsuariosDAO usuariosDAO = new UsuariosDAO();

                    try
                    {

                        int resultado = usuariosDAO.ModificarUsuario(usuarioID, empleadoModificado, usuario);

                        if (resultado > 0)
                        {
                            GestorCuadroDialogo.MostrarInformacion
                                ("Usuario modificado de manera exitosa en el sistema",
                                "Usuario modificado");
                            VentanaPrincipal.CambiarPagina(new GUI_Usuarios());
                        }

                    }
                    catch (EntityException)
                    {
                        GestorCuadroDialogo.MostrarError(
                            "No hay conexión con la base de datos, por favor, intentelo más tarde",
                            "Sin conexión a la base de datos");
                    }
                }

            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Usuarios());
        }

        #region Validaciones

        private bool ExistenCamposInvalidosEmpleado()
        {
            bool hayCamposInvalidos = false;

            if (ValidarDatos.ExistenCaracteresInvalidosParaNombre(nombre))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El nombre es inválido, por favor, ingreselo nuevamente.",
                    "Nombre inválido");
                hayCamposInvalidos = true;
            }

            if (!hayCamposInvalidos && ValidarDatos.
                ExistenCaracteresInvalidosParaNombre(apellidoPaterno))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El apellido paterno no es válido, por favor, ingreselo nuevamente.",
                    "Apellido paterno inválido");
                hayCamposInvalidos = true;
            }

            if (!hayCamposInvalidos && ValidarDatos.
                ExistenCaracteresInvalidosParaNombre(apellidoMaterno))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El apellido materno no es válido, por favor, ingreselo nuevamente.",
                    "Apellido materno inválido");
                hayCamposInvalidos = true;
            }

            if (!hayCamposInvalidos && ValidarDatos.
              ExistenCaracteresInvalidosParaCorreo(email))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El correo no es válido, por favor, ingreselo nuevamente.",
                    "Correo inválido");
                hayCamposInvalidos = true;
            }

            return hayCamposInvalidos;
        }

        private bool ExistenCamposVacíosEmpleado()
        {
            bool existenCamposVacios = false;

            if (ValidarDatos.EsCadenaVacia(nombre) || ValidarDatos.EsCadenaVacia(apellidoPaterno)
                || ValidarDatos.EsCadenaVacia(apellidoMaterno) || ValidarDatos.EsCadenaVacia(telefono)
                || ValidarDatos.EsCadenaVacia(email))
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                "Campos vacíos");
            }

            return existenCamposVacios;
        }

        private bool ExistenCamposInvalidosUsuario()
        {
            bool hayCamposInvalidos = false;

            if (ValidarDatos.ExistenCaracteresInvalidosParaNombreUsuario(nombreUsuario))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El nombre de usuario es inválido, por favor, ingreselo nuevamente.",
                    "Nombre inválido");
                hayCamposInvalidos = true;
            }

            return hayCamposInvalidos;
        }

        private bool ExistenCamposVaciosUsuario()
        {
            bool hayCamposVacios = false;

            if (ValidarDatos.EsCadenaVacia(nombreUsuario))
            {
                hayCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                   "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                   "Campos vacíos");
            }

            return hayCamposVacios;
        }

        private bool DatosIguales(string nombre)
        {
            bool sonIguales =
            empleadoRegistrado.nombre == empleadoModificado.nombre &&
            empleadoRegistrado.apellidoPaterno == empleadoModificado.apellidoPaterno &&
            empleadoRegistrado.apellidoMaterno == empleadoModificado.apellidoMaterno &&
            empleadoRegistrado.telefono == empleadoModificado.telefono &&
            empleadoRegistrado.email == empleadoModificado.email &&
            empleadoRegistrado.idRol == empleadoModificado.idRol &&
            ((empleadoRegistrado.imagen == null && empleadoModificado.imagen == null) ||
            (empleadoRegistrado.imagen != null && empleadoModificado.imagen != null &&
            empleadoRegistrado.imagen.SequenceEqual(empleadoModificado.imagen))) &&
            usuarioNombre == nombre;

            return sonIguales;
        }

        #endregion

        #region Numeros
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
        #endregion

        #region ConversionBytesImagen
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
        #endregion
    }
}
