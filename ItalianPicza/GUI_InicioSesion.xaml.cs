using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Seguridad;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{

    public partial class GUI_InicioSesion : Page
    {
        public GUI_InicioSesion()
        {
            InitializeComponent();
        }

        private void IniciarSesion(object objetoOrigen, RoutedEventArgs evento)
        {
            string nombreUsuario = cuadroTextoNombreUsuario.Text;
            string contrasena = cuadroContrasenaContrasena.Password;

            if (!ValidarDatos.EsCadenaVacia(nombreUsuario) &&
                !ValidarDatos.EsCadenaVacia(contrasena))
            {
                if (!ExistenDatosInvalidos(nombreUsuario, contrasena))
                {

                    usuario usuario = new usuario()
                    {
                        nombreUsuario = nombreUsuario,
                        password = contrasena
                    };

                    UsuariosDAO usuariosDAO = new UsuariosDAO();

                    try
                    {
                        usuario usuarioVerificado = usuariosDAO.verificarExistenciaDeUsuario(usuario);
                        if(usuarioVerificado != null)
                        {

                            VentanaPrincipal ventanaPrincipal = (VentanaPrincipal)Application.Current.MainWindow;
                            ventanaPrincipal.panelNavegacion.Visibility = Visibility.Visible;

                            VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());

                        }
                        else
                        {
                            GestorCuadroDialogo.MostrarAdvertencia(
                            "El usuario ingresado no existe, por favor, verifique la información ingresada", 
                            "Usuario inexistente");
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
            else
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "Los campos están vacíos, por favor, ingrese la información solicitada.",
                    "Campos vacíos");
            }

        }

        private bool ExistenDatosInvalidos(string nombreUsuario, string contrasena)
        {
            bool hayCamposInvalidos = false;

            if (ValidarDatos.ExistenCaracteresInvalidosParaContrasena(contrasena))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "La contraseña es inválida, por favor, ingrese una contraseña válida.",
                    "Contraseña inválida");
                hayCamposInvalidos = true;
            }

            if (!hayCamposInvalidos && ValidarDatos.
                ExistenCaracteresInvalidosParaNombreUsuario(nombreUsuario))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El nombre de usuario no es válido, por favor, ingreselo nuevamente.",
                    "Nombre de usuario inválido");
                hayCamposInvalidos = true;
            }

            if (hayCamposInvalidos && ExistenLongitudesExcedidas(nombreUsuario, contrasena))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El nombre de usuario es muy largo, por favor, vuelvalo a ingresar.",
                    "Longitud de nombre de usaurio");
                hayCamposInvalidos = true;
            }

            return hayCamposInvalidos;
        }

        private bool ExistenLongitudesExcedidas(string nombreUsuario, string contrasena)
        {
            bool resultado = false;

            if (ValidarDatos.ExisteLongitudExcedidaEnNombreUsuario(nombreUsuario) ||
                ValidarDatos.ExisteLongitudExcedidaEnContrasena(contrasena))
            {
                resultado = true;
            }

            return resultado;
        }

    }
}
