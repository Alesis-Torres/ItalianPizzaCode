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
    public partial class GUI_ConsultarPedidos : Page
    {
        List<PedidoGeneral> pedidos = new List<PedidoGeneral>();
        public GUI_ConsultarPedidos()
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
                    foreach(PedidoGeneral pedido in pedidos)
                {
                    Console.WriteLine(pedido.estado);
                }
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

        private void agregarPedido(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_FormularioPedido(false, -1));
        }
        private T GetAncestorOfType<T>(DependencyObject element) where T : DependencyObject
        {
            while (element != null && !(element is T))
            {
                element = VisualTreeHelper.GetParent(element);
            }
            return element as T;
        }

        private void modificarPedido(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            ListViewItem listViewItem = GetAncestorOfType<ListViewItem>(boton);
            if (listViewItem == null)
                return;
            var pedidoSeleccionado = listViewItem.DataContext as PedidoGeneral;
            Console.WriteLine(pedidoSeleccionado.IdPedido);
            if (pedidoSeleccionado != null)
            {
                VentanaPrincipal.CambiarPagina(new GUI_FormularioPedido(true, pedidoSeleccionado.IdPedido));
            }
        }
        private void recargarListaPedidos(object sender, RoutedEventArgs e)
        {
            cargarListaPedidos();
            cbEstadoPedido.SelectedIndex = 0;
        }

        private void cbEstadoPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrarPedidos(pedidos);
        }

        private void lvPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvPedidos.SelectedItem != null) // Ensure an item is selected
            {
                // Cast SelectedItem to PedidoGeneral
                PedidoGeneral pedidoSeleccionado = lvPedidos.SelectedItem as PedidoGeneral;

                if (pedidoSeleccionado != null)
                {
                    Console.WriteLine($"Pedido seleccionado: ID = {pedidoSeleccionado.IdPedido}, Estado = {pedidoSeleccionado.estado}");

                    if (pedidoSeleccionado.estado == "listo")
                    {
                        btnExpedirEntrega.Visibility = Visibility.Visible;
                        btnConfirmarEntrega.Visibility = Visibility.Hidden;
                    }
                    else if (pedidoSeleccionado.estado == "expedido p/ entrega")
                    {
                        btnExpedirEntrega.Visibility = Visibility.Hidden;
                        btnMarcarMerma.Visibility = Visibility.Visible;
                        btnConfirmarEntrega.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnExpedirEntrega.Visibility = Visibility.Hidden;
                        btnConfirmarEntrega.Visibility = Visibility.Hidden;
                        btnMarcarMerma.Visibility = Visibility.Hidden;
                    }

                }
            }
        }

        private void btnExpedirEntrega_Click(object sender, RoutedEventArgs e)
        {
            PedidoGeneral pedidoSeleccionado = (PedidoGeneral)lvPedidos.SelectedItem;
            int ESTADO_PEDIDO = 5;
            
                MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Deseas actualizar el pedido a expedido para entrega?",
           "Confirmación");
                if (respuesta != MessageBoxResult.No)
                {
                int respuestaDB = PedidoDAO.cambiarEstadoPedido(pedidoSeleccionado.IdPedido, ESTADO_PEDIDO);
                if (respuestaDB == 0)
                {
                    GestorCuadroDialogo.MostrarInformacion("El pedido ahora se encuentra en expedido para entrega",
          "Confirmación");
                    cbEstadoPedido.SelectedIndex = 0;
                    cargarListaPedidos();
                    btnExpedirEntrega.Visibility = Visibility.Hidden;
                }
            }
        }
    

        private void btnConfirmarEntrega_Click(object sender, RoutedEventArgs e)
        {
            PedidoGeneral pedidoSeleccionado = (PedidoGeneral)lvPedidos.SelectedItem;
            int ESTADO_PEDIDO = 7;
                
                
                    MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Deseas actualizar el pedido a entregado?",
               "Confirmación");
                    if (respuesta != MessageBoxResult.No)
                    {
                    int respuestaDB = PedidoDAO.cambiarEstadoPedido(pedidoSeleccionado.IdPedido, ESTADO_PEDIDO);
                if (respuestaDB == 0)
                {
                    GestorCuadroDialogo.MostrarInformacion("El pedido ha sido entregado",
              "Confirmación");
                        cbEstadoPedido.SelectedIndex = 0;
                        cargarListaPedidos();
                    btnConfirmarEntrega.Visibility = Visibility.Hidden;
                    }
                }
            }

        private void responsivaTest(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_ResponsivaCocineros());
        }

        private void btnConsiderarMerma_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string fecha = dateTime.ToString("dd MM yyyy");
            PedidoGeneral pedidoSeleccionado = (PedidoGeneral)lvPedidos.SelectedItem;
            int ESTADO_PEDIDO = 8;
                MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Deseas considerar el pedido como merma??",
           "Confirmación");
                if (respuesta != MessageBoxResult.No)
                {
                    int respuestaDB = PedidoDAO.cambiarEstadoPedido(pedidoSeleccionado.IdPedido, ESTADO_PEDIDO);
                    if (respuestaDB == 0)
                    {
                    foreach(producto producto in pedidoSeleccionado.productosRelacionados)
                    {
                        MiscDAO.agregarBaja(producto.idProducto, "Producto dañado durante entrega", fecha, 2, 6);
                    }
                    
                        GestorCuadroDialogo.MostrarInformacion("El pedido ahora forma parte de la merma",
          "Confirmación");
                    cbEstadoPedido.SelectedIndex = 0;
                    cargarListaPedidos();
                    btnConfirmarEntrega.Visibility = Visibility.Hidden;
                }
            }
        }
    }
    }

