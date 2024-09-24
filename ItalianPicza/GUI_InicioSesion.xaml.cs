using System.Windows;
using System.Windows.Controls;
using Seguridad;

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
                if(!ExistenDatosInvalidos(nombreUsuario, contrasena))
                {
                    VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());
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

            if(ValidarDatos.ExistenCaracteresInvalidosParaContrasena(contrasena))
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

            if(hayCamposInvalidos && ExistenLongitudesExcedidas(nombreUsuario, contrasena))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "El nombre de usuario es muy largo, por favor, vuelvalo a ingresar.",
                    "Longitud de nombre de usaurio");
                hayCamposInvalidos = true;  
            }

            return hayCamposInvalidos;
        }

        private bool ExistenLongitudesExcedidas(string nombreJugador, string contrasena)
        {
            bool resultado = false;

            if (ValidarDatos.ExisteLongitudExcedidaEnNombreUsuario(nombreJugador) ||
                ValidarDatos.ExisteLongitudExcedidaEnContrasena(contrasena))
            {
                resultado = true;
            }

            return resultado;
        }

    }
}
