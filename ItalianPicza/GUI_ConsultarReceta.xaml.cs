using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    
    public partial class GUI_ConsultarReceta : Page
    {
        producto productoReceta;
        public GUI_ConsultarReceta(producto producto)
        {
            InitializeComponent();
            ConfigurarDatosProducto(producto);
            productoReceta = producto;
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Inventario());
        }

        private void ModificarReceta(object sender, RoutedEventArgs e)
        {
            // MODULO PENDIENTE
        }

        private void AgregarReceta(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_DarAltaReceta(productoReceta));
        }

        private void CargarReceta(int receta)
        {
            RecetaDAO recetaDAO = new RecetaDAO();
            receta RecetaProducto = new receta();

            try
            {
                RecetaProducto = recetaDAO.ObtenerRecetaProducto(receta);
                if(RecetaProducto != null)
                {
                    ConfigurarDatosReceta(RecetaProducto);
                }
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }
        }

        private void ConfigurarDatosReceta(receta receta)
        {
            tbInstrucciones.Text = receta.instrucciones;
        }

        private void ConfigurarDatosProducto(producto producto)
        {
            lbNombreProducto.Content = producto.nombre;

            if (producto.idReceta != null)
            {
                CargarReceta((int)producto.idReceta);

                try
                {
                    RecetaDAO recetaDAO = new RecetaDAO();

                    List<ingrediente> ingredientesReceta = recetaDAO.ObtenerIngredientesReceta((int)producto.idReceta);

                    lvIngredientes.ItemsSource = ingredientesReceta;
                }
                catch (EntityException)
                {
                    GestorCuadroDialogo.MostrarError
                        ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                        "Sin conexión a la base de datos");
                }
            }
            else
            {
                btnAgregarReceta.Visibility = Visibility.Visible;
                tbInstrucciones.Visibility = Visibility.Collapsed;
            }
        }


    }
}
