using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    
    public partial class GUI_Inventario : Page
    {
        public GUI_Inventario()
        {
            InitializeComponent();

            List<Productos> productos = new List<Productos>
            {
                new Productos { NombreProducto = "Pizza con piña", TipoProducto = "Tipo A", EstatusUsuario = "Disponible", ImagenProducto = "/Imagenes/Logos/image 1.png" },
                new Productos { NombreProducto = "Pizza de pepperoni", TipoProducto = "Tipo B", EstatusUsuario = "Agotado", ImagenProducto = "/Imagenes/Logos/image 1.png" },
                new Productos { NombreProducto = "Pizza 3 quesos", TipoProducto = "Tipo C", EstatusUsuario = "Disponible", ImagenProducto = "/Imagenes/Logos/image 1.png" }
            };

            lvProductos.ItemsSource = productos;
        }

        private void irMenuPrincipal(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_MenuPrincipal());
        }

        public class Productos
        {
            public string NombreProducto { get; set; }
            public string TipoProducto { get; set; }
            public string EstatusUsuario { get; set; }
            public string ImagenProducto { get; set; }  // Path de la imagen como string
        }

    }
}
