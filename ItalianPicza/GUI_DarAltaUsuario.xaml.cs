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

    public partial class GUI_DarAltaUsuario : Page
    {
        public GUI_DarAltaUsuario()
        {
            InitializeComponent();

            cuadroTextoTelefono.PreviewTextInput += SoloNumeros;
            DataObject.AddPastingHandler(cuadroTextoTelefono, new DataObjectPastingEventHandler(VerificarPegado));
        }

        string nombre, apellidoPaterno, apellidoMaterno, nombreUsuario, 
            telefono, email, contrasena, contrasenaConfirmar, rutaArchivo;
        byte[] imagenEmpleado;

        private void GuardarUsuario(object sender, RoutedEventArgs e)
        {
            nombreUsuario = cuadroTextoNombreUsuario.Text.Trim();
            contrasena = cuadroContrasenaContrasena.Password.Trim();
            contrasenaConfirmar = cuadroContasenaConfirmarContrasena.Password.Trim();
            bool estado = verificarEstado();

            if (!ExistenCamposVaciosUsuario() && !ExistenCamposInvalidosUsuario())
            {

                usuario usuario = new usuario()
                {
                    nombreUsuario = nombreUsuario,
                    password = contrasena
                };

                empleado empleado = new empleado()
                {
                    nombre = nombre,
                    apellidoPaterno = apellidoPaterno,
                    apellidoMaterno = apellidoMaterno,
                    email = email,
                    telefono = telefono,
                    idRol = cbRol.SelectedIndex,
                    imagen = imagenEmpleado,
                    estado = estado
                };

                UsuariosDAO usuariosDAO = new UsuariosDAO();

                try { 
                
                    int resultado = usuariosDAO.DarDeAltaNuevoUsuario(empleado, usuario);

                    if(resultado == 1)
                    {
                        GestorCuadroDialogo.MostrarInformacion
                            ("Usuario registrado de manera exitosa en el sistema",
                            "Usuario dado de alta");
                        VentanaPrincipal.CambiarPagina(new GUI_Usuarios());
                    }
                    else 
                    {
                        GestorCuadroDialogo.MostrarError
                            ("El usuario no pudo ser registrado de manera correcta en el sistema",
                            "Registro fallido");
                    }

                }
                catch(EntityException ex)
                {
                    //GestorCuadroDialogo.MostrarError
                    //      ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                    //    "Sin conexión a la base de datos");
                    Console.WriteLine("EntityException: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("InnerException: " + ex.InnerException.Message);
                    }
                }

            }

        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Usuarios());
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

                    imagenEmpleado = File.ReadAllBytes(rutaArchivo);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message);
                }
            }
        }

        private void Siguiente(object sender, RoutedEventArgs e)
        {
            nombre = cuadroTextoNombre.Text.Trim();
            apellidoPaterno = cuadroTextoApellidoPaterno.Text.Trim();
            apellidoMaterno = cuadroTextoApellidoMaterno.Text.Trim();
            nombreUsuario = cuadroTextoNombreUsuario.Text.Trim();
            telefono = cuadroTextoTelefono.Text.Trim();
            email = cuadroTextoEmail.Text.Trim();

            if (!ExistenCamposVacíosEmpleado() && !ExistenCamposInvalidosEmpleado())
            {
                ocultarPanelEmpleado();
            }
        }

        private void ocultarPanelEmpleado()
        {
            panelNavegacion.Visibility = Visibility.Collapsed;
            panelNavegacionUsuario.Visibility = Visibility.Visible;
        }

        private void RegresarPanel(object sender, RoutedEventArgs e)
        {
            panelNavegacion.Visibility = Visibility.Visible;
            panelNavegacionUsuario.Visibility = Visibility.Collapsed;
        }

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

            if(ValidarDatos.EsCadenaVacia(nombre) || ValidarDatos.EsCadenaVacia(apellidoPaterno)
                || ValidarDatos.EsCadenaVacia(apellidoMaterno) || ValidarDatos.EsCadenaVacia(telefono) 
                || ValidarDatos.EsCadenaVacia(email) || cbRol.SelectedIndex == 0 || cbEstado.SelectedIndex == 0
                || rutaArchivo == null)
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

            if (!hayCamposInvalidos && ValidarDatos.
               ExistenCaracteresInvalidosParaContrasena(contrasena))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "La contraseña no es valida, por favor, ingreselo nuevamente.",
                    "Correo inválido");
                hayCamposInvalidos = true;
            }

            if(!hayCamposInvalidos && !ValidarDatos.SonLaMismaContrasena(contrasena, contrasenaConfirmar))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                   "Las contraseñas son diferentes, por favor, ingreselas nuevamente.",
                   "Contraseñas desiguales");
                hayCamposInvalidos = true;
            }

            return hayCamposInvalidos;
        }

        private bool ExistenCamposVaciosUsuario()
        {
            bool hayCamposVacios = false;

            if (ValidarDatos.EsCadenaVacia(nombreUsuario) || ValidarDatos.EsCadenaVacia(contrasena)
              || ValidarDatos.EsCadenaVacia(contrasenaConfirmar))
            {
                hayCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                   "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                   "Campos vacíos");
            }

            return hayCamposVacios;
        }

        private bool verificarEstado()
        {
            bool estado = false;

            if(cbEstado.SelectedIndex == 1)
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
