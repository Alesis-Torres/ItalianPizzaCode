using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItalianPicza
{
    public partial class GUI_Inventario : Page
    {
        public GUI_Inventario()
        {
            InitializeComponent();
        }

        private void VerProductos(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Productos());
        }

        private void VerRecetas(object sender, RoutedEventArgs e)
        {

        }

        private void VerInventario(object sender, RoutedEventArgs e)
        {

        }

        private void irRegresar(object sender, RoutedEventArgs e)
        {

        }
    }
}
