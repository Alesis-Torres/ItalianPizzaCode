using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ItalianPicza.GUI_Inventario;

namespace ItalianPicza
{
    /// <summary>
    /// Interaction logic for GUI_ResponsivaCocineros.xaml
    /// </summary>
    public partial class GUI_ResponsivaCocineros : Page
    {
        List<PedidoGeneral> pedidos = new List<PedidoGeneral>();
        public GUI_ResponsivaCocineros()
        {
            InitializeComponent();
            cargarListaPedidos();
            establecerEstadosPedido();
        }
        private void establecerEstadosPedido()
        {
            List<estadopedido> listaEstados = new List<estadopedido>();
            listaEstados = PedidoDAO.obtenerCatalogoEstados();
            cbEstadoPedido.ItemsSource = null;
            cbEstadoPedido.ItemsSource = listaEstados;
            cbEstadoPedido.DisplayMemberPath = "nombreEstado";
            cbEstadoPedido.SelectedValuePath = "idEstadoPedido";
        }

        private void cargarListaPedidos()
        {
            try
            {
                pedidos = PedidoDAO.ObtenerPedidosUnificados();
                lvPedidos.ItemsSource = pedidos; // Asigna la lista al ItemsSource del ListView

            }
            catch (EntityException ex)
            {
                GestorCuadroDialogo.MostrarError("Error de conexión", "Error al acceder a la base de datos.");
            }
        }

        private void FiltrarPedidos(List<PedidoGeneral> pedidos)
        {
            estadopedido estadoFiltro = (estadopedido)cbEstadoPedido.SelectedItem;
            string filtro = estadoFiltro.nombreEstado.Trim();
            Console.WriteLine(filtro);
            if (string.IsNullOrEmpty(filtro) || filtro.Equals("Sin filtro"))
            {
                actualizarListaPedidos(pedidos);
            }
            else
            {
                List<PedidoGeneral> pedidosFiltrados = pedidos
                .Where(p => p.estado.Trim().ToLower().Contains(filtro.Trim().ToLower()))
                .ToList();
                actualizarListaPedidos(pedidosFiltrados);
            }
        }

        private void actualizarListaPedidos(List<PedidoGeneral> pedidosFiltrados)
        {
            lvPedidos.ItemsSource = pedidosFiltrados.ToList();
        }
        private void recargarListaPedidos(object sender, RoutedEventArgs e)
        {
            cargarListaPedidos();
            cbEstadoPedido.SelectedIndex = 5;
        }

        private void cbEstadoPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrarPedidos(pedidos);
            
            Console.WriteLine(cbEstadoPedido.SelectedIndex.ToString());
        }

        private void click_CancelarPedido(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            ListViewItem listViewItem = GetAncestorOfType<ListViewItem>(boton);
            if (listViewItem == null)
                return;
            var pedidoSeleccionado = listViewItem.DataContext as PedidoGeneral;
            Console.WriteLine(pedidoSeleccionado.IdPedido);
            if (pedidoSeleccionado != null)
            {
                
            }
        }
        private void click_pedidoListo(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            ListViewItem listViewItem = GetAncestorOfType<ListViewItem>(boton);
            if (listViewItem == null)
                return;
            var pedidoSeleccionado = listViewItem.DataContext as PedidoGeneral;
            Console.WriteLine(pedidoSeleccionado.IdPedido);
            if (pedidoSeleccionado != null)
            {
                int ESTADO_PEDIDO = 4;
                int respuestaDB = PedidoDAO.cambiarEstadoPedido(pedidoSeleccionado.IdPedido, ESTADO_PEDIDO);
                if (respuestaDB == 0)
                {
                    MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Deseas actualizar el pedido a listo?",
               "Confirmación");
                    if (respuesta != MessageBoxResult.No)
                    {
                        GestorCuadroDialogo.MostrarInformacion("El pedido ahora se encuentra en preparacion",
              "Confirmación");
                        cbEstadoPedido.SelectedIndex = 0;
                        cargarListaPedidos();
                    }
                }
            }
        }
        private void click_enPreparacion(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            ListViewItem listViewItem = GetAncestorOfType<ListViewItem>(boton);
            if (listViewItem == null)
                return;
            var pedidoSeleccionado = listViewItem.DataContext as PedidoGeneral;
            Console.WriteLine(pedidoSeleccionado.IdPedido);
            if (pedidoSeleccionado != null)
            {
                int ESTADO_PEDIDO = 3;
                int respuestaDB = PedidoDAO.cambiarEstadoPedido(pedidoSeleccionado.IdPedido, ESTADO_PEDIDO);
                if (respuestaDB == 0)
                {
                    MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Deseas actualizar el pedido a en preparacion ?",
               "Confirmación");
                    if (respuesta != MessageBoxResult.No)
                    {
                        GestorCuadroDialogo.MostrarInformacion("El pedido ahora se encuentra en preparacion",
              "Confirmación");
                        cbEstadoPedido.SelectedIndex = 0;
                        cargarListaPedidos();
                        
                    }
                }
            }

        }
        private T GetAncestorOfType<T>(DependencyObject element) where T : DependencyObject
        {
            while (element != null && !(element is T))
            {
                element = VisualTreeHelper.GetParent(element);
            }
            return element as T;
        }
        private FrameworkElement FindVisualChildByName(DependencyObject parent, string name)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement frameworkElement && frameworkElement.Name == name)
                {
                    return frameworkElement;
                }

                var result = FindVisualChildByName(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void ConsultaTest(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
        }
    }

}
