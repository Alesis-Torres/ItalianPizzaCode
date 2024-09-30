using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ItalianPicza
{
  
    public partial class GUI_MenuPrincipal : Page
    {
        public GUI_MenuPrincipal()
        {
            InitializeComponent();
        }

        private void CerrarSesion(object objetoOrigen, MouseButtonEventArgs evento)
        {
            VentanaPrincipal.CambiarPagina(new GUI_InicioSesion());
        }

    }
}
