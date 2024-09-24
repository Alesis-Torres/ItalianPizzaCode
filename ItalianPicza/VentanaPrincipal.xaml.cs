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
using System.Windows.Shapes;

namespace ItalianPicza
{
    
    public partial class VentanaPrincipal : Window
    {
        private static Page PaginaActual { get; set; }

        public static Page PaginaAnterior { get; set; }

        public VentanaPrincipal()
        {
            InitializeComponent();
            PaginaActual = new GUI_InicioSesion();
            marcoPaginaActual.Navigate(PaginaActual);
        }

        public static void CambiarPagina(Page nuevaPagina)
        {
            VentanaPrincipal ventanaPrincipal = ObtenerVentanaActual();
            PaginaActual = nuevaPagina;
            ventanaPrincipal?.marcoPaginaActual.Navigate(nuevaPagina);
        }

        public static VentanaPrincipal ObtenerVentanaActual()
        {
            return (VentanaPrincipal)GetWindow(PaginaActual);
        }

        public static void CambiarPaginaGuardandoAnterior(Page nuevaPagina)
        {
            PaginaAnterior = PaginaActual;
            CambiarPagina(nuevaPagina);
        }

    }
}
