using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using ItalianPicza.DatabaseModel.DTO;
using ItalianPicza.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using static ItalianPicza.GUI_AgregarPedidoProveedor;
using static ItalianPicza.GUI_VerDetallesPedidoProveedores;

namespace ItalianPicza
{
    public partial class GUI_AgregarPedidoProveedor : Page
    {
        string fecha;
        decimal cantidad, cantidadProducto;
        int idCorteCaja, idPedido, idClienteRegistro, idProducto, numeroMesa, idDomicilio;
        List<ProductoGeneral> productos = new List<ProductoGeneral>();
        List<ProductoGeneral> productosResumen = new List<ProductoGeneral>();
        List<ProductoGeneral> productosResumenEdicion = new List<ProductoGeneral>();

        int idPedidoEdicion;
        bool esEdicion;
        bool esLocal;
        bool isUpdatingSelection = false;
        bool isEditingText = false;
        private bool _isUpdatingTextBox = false;



        public GUI_AgregarPedidoProveedor(bool esEdicion, int idPedidoEdicion)
        {
            InitializeComponent();

            this.DataContext = this;
            this.idPedidoEdicion = idPedidoEdicion;
            this.esEdicion = esEdicion;
            cargarListaProductos();
            if (esEdicion)
            {

            }
            else
            {
            }

        }
        private void cargarResumen(int idPedido)
        {
            productosResumenEdicion.Clear();
            lvResumen.ItemsSource = null;
            List<ProductoResumenDTO> listaResumenDTO = PedidoProveedorDAO.recuperarProductos(idPedido);
            foreach (ProductoResumenDTO productoDTO in listaResumenDTO)
            {
                var matchingProducto = productos.FirstOrDefault(p => p.IdProducto == productoDTO.IdProducto);
                if (matchingProducto != null)
                {
                    matchingProducto.Cantidad += productoDTO.Cantidad;
                }
            }
            lvProductos.ItemsSource = null;
            lvProductos.ItemsSource = productos;
            Console.WriteLine("Si estoy llenando el resumen con esto");
            lvResumen.ItemsSource = listaResumenDTO;
            foreach (ProductoResumenDTO item in listaResumenDTO)
            {
                Console.WriteLine("llegue hasta aqui");
                CrearCopiaResumen(item);
                setCantidadesProductos(item.IdProducto, item.Cantidad);
            }

        }

        private void CrearCopiaResumen(ProductoResumenDTO item)
        {
            ProductoGeneral productoOriginal = new ProductoGeneral
            {
                IdProducto = item.IdProducto,
                Cantidad = item.Cantidad
            };
            productosResumenEdicion.Add(productoOriginal);
        }

        private void setCantidadesProductos(int idProducto, int cantidad)
        {
            var producto = lvProductos.Items.OfType<ProductoGeneral>()
                                             .FirstOrDefault(p => p.IdProducto == idProducto);

            if (producto != null)
            {
                lvProductos.ItemContainerGenerator.StatusChanged += (s, e) =>
                {
                    if (lvProductos.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                    {
                        var container = lvProductos.ItemContainerGenerator.ContainerFromItem(producto) as ListViewItem;
                        if (container != null)
                        {
                            var textBox = FindVisualChild<TextBox>(container);
                            if (textBox != null)
                            {
                                textBox.Text = cantidad.ToString();
                            }
                        }
                    }
                };
                lvProductos.ScrollIntoView(producto);
            }
        }

        private void cargarListaProductos()
        {
            try
            {
                lvProductos.ItemsSource = null;
                productos = ProductoDAO.ObtenerProductosEIngredientes();
                lvProductos.ItemsSource = productos;
            }
            catch (EntityException ex)
            {
                GestorCuadroDialogo.MostrarError("Error de conexión", "Error al acceder a la base de datos.");
            }

        }

        private void FiltrarProductos(List<producto> productos)
        {
            
        }

        private void actualizarListaProductos(List<producto> productos)
        {
            lvProductos.ItemsSource = null;
            lvProductos.ItemsSource = productos.ToList();
        }

        private void cantidadProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;

            ListViewItem listViewItem = GetAncestorOfType<ListViewItem>(textBox);
            if (listViewItem == null)
                return;

            var producto = listViewItem.DataContext as producto;
            if (producto == null)
                return;

            if (int.TryParse(textBox.Text, out int cantidadIngresada))
            {
                if (cantidadIngresada > producto.cantidad)
                {
                    _isUpdatingTextBox = true;
                    textBox.Text = producto.cantidad.ToString();
                    _isUpdatingTextBox = false;

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show($"La cantidad ingresada ({cantidadIngresada}) excede la cantidad disponible ({producto.cantidad}).",
                                        "Advertencia",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }));

                    cantidadIngresada = (int)producto.cantidad;
                }

                AgregarOActualizarResumen(producto, cantidadIngresada);
                Console.WriteLine("El producto añadido es " + producto.nombre + " ");
            }
            else
            {
                // Prevent re-entrance
                _isUpdatingTextBox = true;
                textBox.Text = "";
                _isUpdatingTextBox = false;
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

        private void AgregarOActualizarResumen(producto producto, int cantidad)
        {
            var resumenItem = productosResumen.FirstOrDefault(r => r.IdProducto == producto.idProducto);
            if (resumenItem == null)
            {
                resumenItem = new ProductoGeneral
                {
                    IdProducto = producto.idProducto,
                    Nombre = producto.nombre,
                    Cantidad = cantidad,
                    Precio = (decimal)(producto.precio * cantidad)
                };

                productosResumen.Add(resumenItem);
            }
            else
            {
                resumenItem.Cantidad = cantidad;
                resumenItem.Precio = (decimal)(producto.precio * cantidad);
            }
            lvResumen.ItemsSource = null;
            lvResumen.ItemsSource = productosResumen;

            CalcularTotalResumen();

        }

        private decimal CalcularTotalResumen()
        {
            decimal total = (decimal)lvResumen.Items.OfType<producto>().Sum(item => item.precio);
            etiquetaTotal.Content = "Total: $" + total.ToString();
            return total;
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Estás seguro de que desea cancelar el pedido? " +
                "la informacion registrada se perdera de manera permanente",
                "Confirmación");
            if (respuesta != MessageBoxResult.No)
            {
                VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
            }
        }

        private void eliminarProducto(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            ListViewItem listViewItem = GetAncestorOfType<ListViewItem>(boton);
            if (listViewItem == null)
                return;
            var productoSeleccionado = listViewItem.DataContext as ProductoGeneral;
            if (productoSeleccionado != null)
            {
                MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion(
                    "¿Eliminar " + productoSeleccionado.Nombre + "?",
                    "Confirmación"
                );

                if (respuesta != MessageBoxResult.No)
                {
                    productosResumen.Remove(productoSeleccionado);
                    lvResumen.ItemsSource = null;
                    lvResumen.ItemsSource = productosResumen;

                    ActualizarCantidadProductoEnLvProductos(productoSeleccionado.IdProducto);
                    CalcularTotalResumen();
                }
            }
        }

        private void ActualizarCantidadProductoEnLvProductos(int idProducto)
        {
            var producto = lvProductos.Items.OfType<producto>()
                                             .FirstOrDefault(p => p.idProducto == idProducto);

            if (producto != null)
            {
                var container = lvProductos.ItemContainerGenerator.ContainerFromItem(producto) as ListViewItem;
                if (container != null)
                {
                    var textBox = FindVisualChild<TextBox>(container);

                    if (textBox != null)
                    {
                        textBox.Text = "";
                    }
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                    return tChild;

                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void eliminarPedido(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Estás seguro de que quieres realizar esta acción?",
                "Confirmación");
            if (respuesta != MessageBoxResult.No)
            {
                int resultadoBD = PedidoProveedorDAO.EliminarRelacionesPedidos(idPedidoEdicion);
                resultadoBD = PedidoProveedorDAO.eliminarPedido(idPedidoEdicion);
                List<producto> productos = new List<producto>();
                {
                    foreach (ProductoGeneral producto in productosResumen)
                    {
                        producto productoTransfer = new producto()
                        {
                            idProducto = producto.IdProducto

                    };
                        productos.Add(productoTransfer);
                    }
                }
                resultadoBD = PedidoProveedorDAO.incrementarCantidadProductos(productos);
                if (resultadoBD == 0)
                {
                    GestorCuadroDialogo.MostrarInformacion("Pedido eliminado exitosamente",
                "Pedido eliminado");
                    VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
                }
                else
                {
                    GestorCuadroDialogo.MostrarInformacion("No se ha podido eliminar el pedido",
                "Error");
                }
            }
        }

        private void GuardarPedido(object sender, RoutedEventArgs e)
        {
            if (!esEdicion)
            {
                AltaPedido();
            }
            else
            {
                EditarPedido();
            }
        }

        private void EditarPedido()
        {
            MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion("¿Estás seguro de que quieres realizar esta acción?", "Confirmación");
            if (respuesta != MessageBoxResult.No)
            {
                int resultadoBD = 0;
                List<ProductoGeneral> nuevosProductos = new List<ProductoGeneral>();
                List<ProductoGeneral> eliminadosProductos = new List<ProductoGeneral>();

                (nuevosProductos, eliminadosProductos, resultadoBD) = CheckAddedNewAndDeletedProductos(productosResumen, productosResumenEdicion, nuevosProductos, eliminadosProductos);

                agregarProductosPedido(nuevosProductos);
                eliminarProductosPedido(eliminadosProductos);

                decimal nuevoTotal = CalcularTotalResumen();
                Console.WriteLine("Nuevo total es de " + nuevoTotal);
                PedidoProveedorDAO.actualizarTotalPedido(nuevoTotal, idPedidoEdicion);

                if (resultadoBD == 0)
                {
                    GestorCuadroDialogo.MostrarInformacion("Pedido actualizado exitosamente", "Pedido actualizado");
                    VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
                }
                else
                {
                    GestorCuadroDialogo.MostrarInformacion("No se ha podido actualizar el pedido", "Error");
                }
            }
        }

        private int agregarProductosPedido(List<ProductoGeneral> nuevosProductos)
        {
            int resultadoBD = 0;
            foreach (ProductoGeneral productoResumen in nuevosProductos)
            {
                Console.WriteLine("Estoy intentando registrar " + productoResumen.Nombre);
                resultadoBD = PedidoProveedorDAO.registrarPedidoProducto(idPedidoEdicion, productoResumen.IdProducto, (int)productoResumen.Cantidad);
            }
            return resultadoBD;
        }

        private (List<ProductoGeneral> nuevos, List<ProductoGeneral> eliminados, int resultadoDB) 
            CheckAddedNewAndDeletedProductos(List<ProductoGeneral> productosResumen, List<ProductoGeneral> originalProductos, List<ProductoGeneral> nuevosProductos, List<ProductoGeneral> eliminadosProductos)
        {
            int resultadoDB = 0;
            Console.WriteLine("Aqui si estoy llegando mano ");
            foreach (var producto in productosResumen)
            {
                if (!originalProductos.Any(p => p.IdProducto == producto.IdProducto))
                {
                    Console.WriteLine("Se va a anadir esto al pedido " + producto.Nombre);
                    nuevosProductos.Add(producto);
                }
            }

            foreach (var producto in originalProductos)
            {
                if (!productosResumen.Any(p => p.IdProducto == producto.IdProducto))
                {
                    eliminadosProductos.Add(producto);
                }
            }

            foreach (var producto in originalProductos)
            {
                var resumenProducto = productosResumen.FirstOrDefault(p => p.IdProducto == producto.IdProducto);
                if (resumenProducto != null && resumenProducto.Cantidad != producto.Cantidad)
                {
                    if (resumenProducto.Cantidad < producto.Cantidad)
                    {
                        int cantidadRestar = (int)(producto.Cantidad - resumenProducto.Cantidad);
                        resultadoDB = decrementarCantidadPedido(resumenProducto);
                        Console.WriteLine("Resultado decrementarCantidadPedido " + resultadoDB);
                    }
                    else if (resumenProducto.Cantidad > producto.Cantidad)
                    {
                        int cantidadRestar = (int)(resumenProducto.Cantidad - producto.Cantidad);
                        resultadoDB = incrementarCantidadPedido(resumenProducto);
                        Console.WriteLine("Resultado incrementarCantidadPedido " + resultadoDB);
                    }
                }
            }

            return (nuevosProductos, eliminadosProductos, resultadoDB);
        }

        private int eliminarProductosPedido(List<ProductoGeneral> eliminadosProductos)
        {
            int resultadoBD = 0;
            foreach (ProductoGeneral productoResumen in eliminadosProductos)
            {
                resultadoBD = PedidoProveedorDAO.EliminarRelacionPedidoProveedor(idPedidoEdicion, productoResumen.IdProducto);
            }
            return resultadoBD;
        }

        private int decrementarCantidadPedido(ProductoGeneral resumenProducto)
        {
            return PedidoProveedorDAO.decrementarCantidadPedido(idPedidoEdicion, resumenProducto);
        }

        private int incrementarCantidadPedido(ProductoGeneral resumenProducto)
        {
            return PedidoProveedorDAO.incrementarCantidadPedido(idPedidoEdicion, resumenProducto);
        }

        private void AltaPedido()
        {
            pedidoproveedor nuevoPedido = new pedidoproveedor()
            {

            };
            int idPedidoRegistro = PedidoProveedorDAO.registrarNuevoPedido(nuevoPedido);
            foreach (ProductoGeneral productoResumen in productosResumen)
            {
                Console.WriteLine(idPedidoRegistro + " " + productoResumen.IdProducto + " " + (int)productoResumen.Cantidad);
                PedidoProveedorDAO.registrarPedidoProveedorProducto(idPedidoRegistro, productoResumen.IdProducto, (int)productoResumen.Cantidad);
            }

            MessageBox.Show("Pedido creado correctamente.",
                                    "Pedido creado",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
            VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
        }

    }
}
