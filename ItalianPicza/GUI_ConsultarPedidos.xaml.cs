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
        }

        private void cargarListaPedidos()
        {
            try
            {
                pedidos = PedidoDAO.ObtenerPedidosUnificados();
                lvPedidos.ItemsSource = pedidos;
            }
            catch (EntityException ex)
            {
                GestorCuadroDialogo.MostrarError("Error de conexión", "Error al acceder a la base de datos.");
            }
        }
        private void cuadroTextoPedido_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarPedidos(pedidos);
        }
        private void FiltrarPedidos(List<PedidoGeneral> pedidos)
        {
            string filtro = cuadroTextoPedido.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(filtro))
            {
                actualizarListaPedidos(pedidos);
            }
            else
            {
                var pedidosFiltrados = pedidos
                    .Where(p => p.InfoExtra.ToLower().Contains(filtro) )
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
            VentanaPrincipal.CambiarPagina(new GUI_FormularioPedido(false, null));
        }
        private void modificarPedido(object sender, RoutedEventArgs e)
        {
            pedido pedidoEdicion = (pedido)lvPedidos.SelectedItem;
            if (lvPedidos.SelectedItem != null)
            {
                VentanaPrincipal.CambiarPagina(new GUI_FormularioPedido(true, pedidoEdicion));
            }
        }

        private void cerrarDetalles(object sender, RoutedEventArgs e)
        {
            GridResumen.Visibility = Visibility.Hidden;
            lvResumen.ItemsSource = null;
        }
        private void botonDetalles(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var pedido = button.CommandParameter;
            List<producto> listaProductosPedido = cargarDatosPedido((pedido)pedido);
            lvResumen.ItemsSource = listaProductosPedido;
            GridResumen.Visibility = Visibility.Visible;
        }

        private List<producto> cargarDatosPedido(pedido pedido)
        {
            List<producto> listaPedidos = new List<producto>();
            try { 
                listaPedidos = PedidoDAO.recuperarProductos(pedido.idPedido);
                
            }
            catch (EntityException ex)
            {
                GestorCuadroDialogo.MostrarError("Error de conexión", "Error al acceder a la base de datos.");
            }
            return listaPedidos;
        }

        private void botonModificar(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var pedido = button.CommandParameter;
            VentanaPrincipal.CambiarPagina(new GUI_FormularioPedido(true, (pedido)pedido));
        }
    }
}
