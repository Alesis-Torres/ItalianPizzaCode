using System.Windows;

namespace ItalianPicza
{
    public static class GestorCuadroDialogo
    {
        public static void MostrarAdvertencia(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        public static void MostrarError(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public static void MostrarInformacion(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        public static MessageBoxResult MostrarConfirmacion(string mensaje, string titulo)
        {
            MessageBoxResult respuesta =  MessageBox.Show(mensaje, titulo, MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            return respuesta;
        }
    }
}
