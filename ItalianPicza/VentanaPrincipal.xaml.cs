using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ItalianPicza
{

    public partial class VentanaPrincipal : Window
    {
        private static Page PaginaActual { get; set; }

        public static Page PaginaAnterior { get; set; }

        public static int empleado;

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

        private void CerrarSesion(object sender, RoutedEventArgs e)
        {
            CambiarPagina(new GUI_InicioSesion());
            panelNavegacion.Visibility = Visibility.Collapsed;
        }

        private void irUsuarios(object sender, RoutedEventArgs e)
        {
            CambiarPagina(new GUI_Usuarios());
        }

        private void irInventario(object sender, RoutedEventArgs e)
        {
            CambiarPagina(new GUI_Inventario());
        }

        private void irPedidos(object sender, RoutedEventArgs e)
        {
            MostrarGUISegunRol();
        }

        private void irProveedores(object sender, RoutedEventArgs e)
        {
            CambiarPagina(new GUI_Proveedores());
        }

        private void irFinanzas(object sender, RoutedEventArgs e)
        {
            CambiarPagina(new GUI_Finanzas());
        }

        public void ActualizarAccesoSegunRol()
        {
            switch (empleado)
            {
                case 2: 
                    btnUsuarios.IsEnabled = false;
                    btnFinanzas.IsEnabled = false;
                    btnProveedores.IsEnabled = false;
                    break;

                case 3: // Si es rol 3, deshabilitar otros botones
                    btnUsuarios.IsEnabled = false;
                    btnFinanzas.IsEnabled = false;
                    btnProveedores.IsEnabled = false;
                    btnInventario.IsEnabled = false;
                    break;
                default: 
                    btnUsuarios.IsEnabled = true;
                    btnFinanzas.IsEnabled = true;
                    btnProveedores.IsEnabled = true;
                    btnPedidos.IsEnabled = true;
                    btnInventario.IsEnabled = true;
                    break;
            }
        }


        public void MostrarGUISegunRol()
        {
            if (empleado != 1) //Segun sea el rol
            {
                //CambiarPagina(new GUI_Finanzas()); Segun el rol del empleado en GUI_Empleados
            }
        }

    }
}