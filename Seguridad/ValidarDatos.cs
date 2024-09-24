using System;
using System.Text.RegularExpressions;

namespace Seguridad
{
    public static class ValidarDatos
    {
        private const int MaximoCaracteresContrasena = 45;

        private const int MaximoCaracteresNombreUsuario = 15;

        private const int MilisegundosMaximosParaExpresionRegular = 100;

        private const string PatronContrasena = "^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,}$";

        private const string PatronNombreUsuario = @"^[a-zA-Z0-9]+(?:\s[a-zA-Z0-9]+)?$";

        public static bool ExisteLongitudExcedidaEnContrasena(string contrasena)
        {
            bool camposExcedidos = false;

            if (contrasena.Length > MaximoCaracteresContrasena)
            {
                camposExcedidos = true;
            }

            return camposExcedidos;
        }

        public static bool ExisteLongitudExcedidaEnNombreUsuario(string nombreUsuario)
        {
            bool camposExcedidos = false;

            if (nombreUsuario.Length > MaximoCaracteresNombreUsuario)
            {
                camposExcedidos = true;
            }

            return camposExcedidos;
        }

        public static bool ExistenCaracteresInvalidosParaContrasena(string contrasena)
        {
            bool contrasenaInvalida = false;

            if (!Regex.IsMatch(contrasena, PatronContrasena, RegexOptions.None,
                TimeSpan.FromMilliseconds(MilisegundosMaximosParaExpresionRegular)))
            {
                contrasenaInvalida = true;
            }

            return contrasenaInvalida;
        }

        public static bool ExistenCaracteresInvalidosParaNombreUsuario(string nombreUsuario)
        {
            bool resultado = false;

            if (!Regex.IsMatch(nombreUsuario, PatronNombreUsuario, RegexOptions.None,
                TimeSpan.FromMilliseconds(MilisegundosMaximosParaExpresionRegular)))
            {
                resultado = true;
            }

            return resultado;
        }

        public static bool EsCadenaVacia(string cadena)
        {
            bool resultado = false;

            if (string.IsNullOrWhiteSpace(cadena))
            {
                resultado = true;
            }

            return resultado;
        }
    }
}
