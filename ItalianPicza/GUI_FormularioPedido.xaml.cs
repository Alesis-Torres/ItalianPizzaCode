using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
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
using System.Xml.Linq;
using static ItalianPicza.GUI_Inventario;

namespace ItalianPicza
{

    public partial class GUI_FormularioPedido : Page
    
    {
        string fecha, nombreCliente, domicilio;
        decimal cantidad, cantidadProducto;
        int idCorteCaja, idPedido, idClienteRegistro, idProducto, numeroMesa, numeroCliente, idDomicilio;
        List<producto> productos = new List<producto>();
        List<producto> productosResumen = new List<producto>();
        pedido pedidoEdicion = new pedido();
        bool esEdicion;
        bool esLocal;
        public GUI_FormularioPedido(bool esEdicion, pedido pedidoEdicion)
        {
            InitializeComponent();
            pedidoEdicion = this.pedidoEdicion;
            esEdicion = this.esEdicion;
            if (esEdicion)
            {
                cargarPantallaEdicion(pedidoEdicion);
            }
            else {
                cargarPantallaAlta();
             }
            cargarListaProductos();

        }

        private void cargarPantallaAlta()
        {
            gridLocalEdicion.Visibility = Visibility.Hidden;
            gridDomicilioEdicion.Visibility = Visibility.Hidden;
            gridTipoPedidoAlta.Visibility = Visibility.Visible;
        }

        private void cargarPantallaEdicion(pedido pedidoEdicion)
        {
            if (pedidoEdicion.pedidolocal.mesa != null)
            {
                gridLocalEdicion.Visibility = Visibility.Visible;
                gridDomicilioEdicion.Visibility = Visibility.Hidden;
                cargarInformacionPedido(esLocal);
            }
            else {
                gridLocalEdicion.Visibility = Visibility.Hidden;
                gridDomicilioEdicion.Visibility = Visibility.Visible;
                cargarInformacionPedido(esLocal);
            }
            botonEliminarPedido.Visibility = Visibility.Visible;

        }

        private void cargarInformacionPedido(bool esLocal)
        {
            if (esLocal)
            {
                cuadroTextoMesaEdicion.Text = pedidoEdicion.pedidolocal.mesa.ToString();
            }
            else
            {
                cuadroTextoClienteEdicion.Text = pedidoEdicion.pedidodomicilio.cliente.nombre.ToString() 
                    + " " + pedidoEdicion.pedidodomicilio.cliente.apellidoPaterno.ToString() 
                    + " " + pedidoEdicion.pedidodomicilio.cliente.apellidoMaterno.ToString();
                cuadroTextoDomicilioEdicion.Text = pedidoEdicion.pedidodomicilio.cliente.direccion.ToString();
                cuadroTextoTelefonoEdicion.Text = pedidoEdicion.pedidodomicilio.cliente.telefono.ToString();
            }
            cargarResumen(pedidoEdicion.idPedido);
        }

        private void cargarResumen(int idPedido)
        {
            lvResumen.ItemsSource = PedidoDAO.recuperarProductos(idPedido);
        }

        private void cargarListaProductos()
        {
            
            try
            {
                productos = ProductoDAO.ObtenerListaProductos();
                lvProductos.ItemsSource = productos;
            }
            catch (EntityException ex)
            {
                GestorCuadroDialogo.MostrarError("Error de conexión", "Error al acceder a la base de datos.");
            }

        }

        private void cuadroTextoProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarProductos(productos);
        }
        private void FiltrarProductos(List<producto> productos)
        {
            string filtro = cuadroTextoProducto.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(filtro))
            {
                actualizarListaProductos(productos);
            }
            else
            {
                var productosFiltrados = productos
                    .Where(p => p.nombre.ToLower().Contains(filtro) || p.codigo.ToString().Contains(filtro))
                    .ToList();
                actualizarListaProductos(productosFiltrados);
            }
        }

        private void actualizarListaProductos(List<producto> productos)
        {
            lvProductos.ItemsSource = productos.ToList();
        }

        private void rbDomicilio_Checked(object sender, RoutedEventArgs e)
        {
            gridDomicilio.Visibility = Visibility.Visible;
            gridLocal.Visibility = Visibility.Hidden;
        }
        private void rbLocal_Checked(object sender, RoutedEventArgs e)
        {
            gridLocal.Visibility = Visibility.Visible;
            gridLocal.Visibility = Visibility.Hidden;
            cuadroTextoCliente.Text = null;
            cuadroTextoDomicilio.Text = null;
            cuadroTextoTelefono.Text = null;
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Estás seguro de que desea cancelar el pedido? " +
                "la informacion registrada se perdera de manera permanente",
                "Confirmación");
            if (respuesta != MessageBoxResult.Yes)
            {
                VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
            }
        }

        private void eliminarProducto(object sender, RoutedEventArgs e)
        {
                producto productoSeleccionado = (producto)lvResumen.SelectedItem;
                if (productoSeleccionado != null)
                {
                    productosResumen.Remove(productoSeleccionado);
                    lvResumen.ItemsSource = null;
                    lvResumen.ItemsSource = productosResumen;
                    MessageBox.Show("Producto eliminado.");
                }
                else
                {
                    MessageBox.Show("Por favor selecciona un producto para eliminar.");
                }
        }

        private void eliminarPedido(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Estás seguro de que quieres realizar esta acción?",
                "Confirmación");
            if (respuesta != MessageBoxResult.Yes)
            {
                int resultadoBD = PedidoDAO.EliminarPedido(pedidoEdicion.idPedido);
                if (resultadoBD == 0) {
                    GestorCuadroDialogo.MostrarInformacion("Pedido eliminado exitosamente",
                "Pedido eliminado");
                }
            }
        }
        private void agregarProducto(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var producto = button.CommandParameter;
            agregarProductos((producto)producto);
            productosResumen = productosResumen.OrderBy(p => p.nombre).ToList();
            lvResumen.ItemsSource = null;
            lvResumen.ItemsSource = productosResumen;
        }
        private void reducirProducto(object sender, RoutedEventArgs e)
        {
            producto productoSeleccionado = (producto)lvProductos.SelectedItem;
            var existingItem = productosResumen.FirstOrDefault(i => i.nombre.Equals(productoSeleccionado.nombre, StringComparison.OrdinalIgnoreCase));

            if (existingItem != null)
            {
                existingItem.cantidad -= productoSeleccionado.cantidad;
            }
            else
            {
                productosResumen.Remove(productoSeleccionado);
            }
        }
        private void agregarProductos(producto producto)
        {
            var existingItem = productosResumen.FirstOrDefault(i => i.nombre.Equals(producto.nombre, StringComparison.OrdinalIgnoreCase));

            if (existingItem != null)
            {
                existingItem.cantidad += producto.cantidad;
            }
            else
            {
                productosResumen.Add(producto);
            }
        }

        private void GuardarPedido(object sender, RoutedEventArgs e)
        {
            cantidad= 0;
            DateTime fechaActual = DateTime.Today;
            fecha = fechaActual.ToString();
            nombreCliente = cuadroTextoCliente.Text;
            domicilio = cuadroTextoDomicilio.Text;
            numeroMesa = cbMesas.SelectedIndex;
            numeroCliente = int.Parse(cuadroTextoTelefono.Text);
            idCorteCaja = 1; //TODO
            foreach(var item in productosResumen)
            {
                cantidad = (decimal)(cantidad + item.cantidad);            
            }
            pedido nuevoPedido = new pedido()
            {
                fecha = fecha,
                cantidad = cantidad,
                idCorteCaja = idCorteCaja
            };
            cliente nuevoCliente = new cliente()
            {
                nombre = nombreCliente,//TODO modificar bdd un solo nombre
                telefono = numeroCliente
            };

            try
            {
                int resultado = PedidoDAO.registrarNuevoPedido(nuevoPedido);
                foreach (var item in productosResumen) {
                    pedidoproducto nuevoPedidoProducto = new pedidoproducto()
                    {
                        idProducto = item.idProducto,
                        idPedido = resultado,
                        cantidad = item.cantidad
                    };
                    PedidoDAO.registrarProductosPedido(nuevoPedidoProducto);
                    idClienteRegistro = UsuariosDAO.registrarCliente(nuevoCliente);
                    direccion nuevoDomicilio = new direccion()
                    {
                        nombre = domicilio,
                        idCliente = idClienteRegistro
                    };
                    idDomicilio = DomicilioDAO.registrarDomicilio(nuevoDomicilio);
                    pedidodomicilio nuevoPedidoDomicilio = new pedidodomicilio()
                    {
                        //idPedido = idPedido, TODO FALTA ID DE PEDIDO EN TABLA
                        idCliente = idClienteRegistro
                    };
                    PedidoDAO.registrarDireccionRegistro(idDomicilio, resultado);
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
}
