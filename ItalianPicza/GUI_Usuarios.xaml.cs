using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{

    public partial class GUI_Usuarios : Page
    {
        public GUI_Usuarios()
        {
            InitializeComponent();

            //Datos de ejemplo 
            List<Usuario> usuarios = new List<Usuario>()
            {
                new Usuario() { NombreUsuario = "Pizza lover", EstatusUsuario = "Activo", ImagenUsuario = "/Imagenes/Logos/image 9.png" },
                new Usuario() { NombreUsuario = "Tlachy", EstatusUsuario = "Activo", ImagenUsuario = "/Imagenes/Logos/image 9.png" },
                new Usuario() { NombreUsuario = "Luis", EstatusUsuario = "Activo", ImagenUsuario = "/Imagenes/Logos/image 9.png" },
                new Usuario() { NombreUsuario = "Pizza lover", EstatusUsuario = "Activo", ImagenUsuario = "/Imagenes/Logos/image 9.png" },
                new Usuario() { NombreUsuario = "Tlachy", EstatusUsuario = "Activo", ImagenUsuario = "/Imagenes/Logos/image 9.png" },
                new Usuario() { NombreUsuario = "Tlachy", EstatusUsuario = "Activo", ImagenUsuario = "/Imagenes/Logos/image 9.png" },
                new Usuario() { NombreUsuario = "Luis", EstatusUsuario = "Activo", ImagenUsuario = "/Imagenes/Logos/image 9.png" }
            };

            lvUsuarios.ItemsSource = usuarios;
        }

        private void DarAltaUsuario(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_DarAltaUsuario());
        }

        public class Usuario
        {
            public string NombreUsuario { get; set; }
            public string EstatusUsuario { get; set; }
            public string ImagenUsuario { get; set; } // Path to image
        }

    }
}
