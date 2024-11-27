using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ItalianPicza
{
   
    public partial class GUI_DarAltaReceta : Page
    {
        private List<ingrediente> ingredientes;
        private List<ingrediente> ingredientesReceta = new List<ingrediente>();
        producto _producto;

        public GUI_DarAltaReceta(producto producto)
        {
            InitializeComponent();
            CargarInsumos();
            _producto = producto;
        }

        private void AgregarReceta(object sender, RoutedEventArgs e)
        {
            if (lvReceta.Items.Count == 0)
            {
                GestorCuadroDialogo.MostrarAdvertencia
                           ("Por favor, ingrese los ingredientes correspondientes para la receta",
                           "Sin ingredientes");
            }
            else
            {
                panelInsumos.Visibility = Visibility.Collapsed;
                panelInstrucciones.Visibility = Visibility.Visible;
            }
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            panelInsumos.Visibility = Visibility.Visible;
            panelInstrucciones.Visibility = Visibility.Collapsed;
        }

        private void RegistrarInstrucciones(object sender, RoutedEventArgs e)
        {
            string instrucciones = tbInstrucciones.Text.Trim();

            if (tbInstrucciones.Text != null)
            {
                RecetaDAO recetaDAO = new RecetaDAO();

                try
                {
                    int resultado = recetaDAO.DarDeAltaReceta(instrucciones, ingredientesReceta, _producto);

                    if (resultado != 0)
                    {
                        GestorCuadroDialogo.MostrarInformacion
                            ("Receta registrada de manera exitosa en el sistema",
                            "Receta dada de alta");
                        VentanaPrincipal.CambiarPagina(new GUI_Inventario());
                    }
                    else
                    {
                        GestorCuadroDialogo.MostrarError
                            ("La receta no pudo ser registrado de manera correcta en el sistema",
                            "Registro fallido");
                    }
                }
                catch (EntityException)
                {
                    GestorCuadroDialogo.MostrarError
                               ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                               "Sin conexión a la base de datos");
                }

            }
        }

        private void CuadroTextoCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                int cursorPos = textBox.SelectionStart;

                string textoOriginal = textBox.Text;
                string textoFiltrado = new string(textoOriginal.Where(c => char.IsDigit(c)).ToArray());

                if (textoOriginal != textoFiltrado)
                {
                    textBox.Text = textoFiltrado;

                    textBox.SelectionStart = cursorPos > textBox.Text.Length ? textBox.Text.Length : cursorPos;
                }
            }
        }

        private void AñadirIngrediente_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;

            if (boton != null)
            {
                ListViewItem listViewItem = FindParent<ListViewItem>(boton);

                if (listViewItem != null)
                {
                    TextBox cuadroTextoCantidad = FindChild<TextBox>(listViewItem, "cuadroTextoCantidad");
                    string cantidadTexto = cuadroTextoCantidad.Text;

                    if (!string.IsNullOrWhiteSpace(cantidadTexto))
                    {
                        var ingrediente = listViewItem.DataContext as ingrediente;

                        if (ingrediente != null)
                        {
                            string nombreIngrediente = ingrediente.nombre;

                            int idInsumo = ingrediente.idIngrediente;

                                
                                bool ingredienteYaExiste = ingredientesReceta.Any(i => i.nombre == nombreIngrediente);

                                if (ingredienteYaExiste)
                                {
                                    MessageBox.Show($"El ingrediente '{nombreIngrediente}' ya está en la receta.");
                                }
                                else
                                {
                                    ingrediente nuevoIngrediente = new ingrediente
                                    {
                                        nombre = nombreIngrediente,
                                        cantidadActual = cantidadTexto,
                                        idIngrediente = idInsumo
                                    };

                                    ingredientesReceta.Add(nuevoIngrediente);

                                    lvReceta.ItemsSource = null;  
                                    lvReceta.ItemsSource = ingredientesReceta;
                                }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Por favor, ingrese una cantidad del ingrediente");
                    }
                }
            }
        }


        private void EliminarIngrediente(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;

            if (boton != null)
            {
                var ingredienteReceta = boton.DataContext as ingrediente;

                if (ingredienteReceta != null)
                {
                    ingredientesReceta.Remove(ingredienteReceta);

                    lvReceta.ItemsSource = null;  
                    lvReceta.ItemsSource = ingredientesReceta;
                }
            }
        }


        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T tChild && (child as FrameworkElement).Name == childName)
                {
                    return tChild;
                }

                var foundChild = FindChild<T>(child, childName);
                if (foundChild != null)
                {
                    return foundChild;
                }
            }

            return null;
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
            {
                return parent;
            }

            return FindParent<T>(parentObject);
        }



        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Inventario());
        }

        private void CargarInsumos()
        {
            InsumosDAO insumosDAO = new InsumosDAO();

            try
            {
                ingredientes = insumosDAO.ObtenerIngredientes();
                lvInsumos.ItemsSource = ingredientes;
            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }
        }

    }
}
