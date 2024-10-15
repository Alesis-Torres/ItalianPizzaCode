using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{

    public partial class GUI_Usuarios : Page
    {
        private List<empleado> empleados;

        public GUI_Usuarios()
        {
            InitializeComponent();

            CargarEmpleados();
            
        }

        private void DarAltaUsuario(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_DarAltaUsuario());
        }

        private void CargarEmpleados()
        {
            UsuariosDAO usuariosDAO = new UsuariosDAO();

            //List<empleado> empleados = new List<empleado>();

            try
            {
                empleados = usuariosDAO.ObtenerEmpleados();
                lvUsuarios.ItemsSource = empleados;

            }
            catch(EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }

        }

        private void FiltrarPorNombre(object sender, TextChangedEventArgs e)
        {
            string textoBusqueda = cuadroTextoNombreEmpleado.Text.ToLower().Trim(); 

            if (!string.IsNullOrWhiteSpace(textoBusqueda))
            {
                var empleadosFiltrados = empleados.Where(emp => emp.nombre.ToLower().Contains(textoBusqueda))
                    .ToList();

                lvUsuarios.ItemsSource = empleadosFiltrados; 
            }
            else
            {
                lvUsuarios.ItemsSource = empleados;
            }
        }


        private void FiltrarPorRol(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string seleccion = (comboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (seleccion == "Gerente de sucursal")
            {
                FiltrarEmpleadosPorRol(1); 
            }
            else if (seleccion == "Cocinero")
            {
                FiltrarEmpleadosPorRol(2); 
            }
            else
            {
                CargarEmpleados();
            }
        }

        private void FiltrarEmpleadosPorRol(int idRol)
        {
            UsuariosDAO usuariosDAO = new UsuariosDAO();
            List<empleado> empleados = new List<empleado>();

            try
            {
                empleados = usuariosDAO.ObtenerEmpleadosPorRol(idRol); 
                lvUsuarios.ItemsSource = empleados;
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                    ("No hay conexión con la base de datos, por favor, inténtelo más tarde", 
                    "Sin conexión a la base de datos");
            }
        }

    }
}
