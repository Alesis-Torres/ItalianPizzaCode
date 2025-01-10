using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
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

using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using ItalianPicza.DatabaseModel.DTO;
using System.Collections;
using static MaterialDesignThemes.Wpf.Theme;
using Button = System.Windows.Controls.Button;
using TextBox = System.Windows.Controls.TextBox;
using System.Windows.Controls.Primitives;
using ListView = System.Windows.Controls.ListView;

namespace ItalianPicza
{

    public partial class GUI_FormularioPedido : Page
    {
        string fecha, nombreCliente, domicilioCliente, numeroCliente, tipoPedido;
        decimal cantidad, cantidadProducto;
        int idCorteCaja, idPedido, idClienteRegistro, idProducto, numeroMesa, idDomicilio;
        List<producto> productos = new List<producto>();
        List<producto> productosResumen = new List<producto>();
        List<producto> productosResumenEdicion = new List<producto>();
        List<telefono> todoTelefono;
        List<cliente> todoCLiente;
        List<mesa> todoMesa;
        List<direccioncliente> todaDireccionCliente;
        List<direccion> todoDireccion;
        List<direccioncliente> relacionDireccionCliente;
        
        int idPedidoEdicion;
        bool esEdicion;
        bool esLocal;
        bool isUpdatingSelection = false;
        bool isEditingText = false;
        private bool _isUpdatingTextBox = false;



        public GUI_FormularioPedido(bool esEdicion, int idPedidoEdicion)
        {
            InitializeComponent();
            
            this.DataContext = this;
            this.idPedidoEdicion = idPedidoEdicion;
            this.esEdicion = esEdicion;
            todaDireccionCliente = UsuariosDAO.ObtenerClienteDirecciones();
            cargarListaProductos();
            establecerComboboxMesas();
            if (esEdicion)
            {
                Console.WriteLine("Esta entrando Edicion");
                ajustarPantallaEdicion(idPedidoEdicion);
            }
            else {
                Console.WriteLine("Esta entrando alta");
                rbLocal.IsChecked = true;
                ajustarPantallaAlta();
            }

            todoTelefono = UsuariosDAO.ObtenerNumerosTelefonicos();
            todoCLiente = UsuariosDAO.ObtenerClientes();
            todoDireccion = UsuariosDAO.obtenerDirecciones();
            relacionDireccionCliente = UsuariosDAO.obtenerRelacionDirecciones();

        }

        private void ajustarPantallaAlta()
        {
            establecerComboboxTelefonos();
            establecerComboboxClientes();
            establecerComboboxDomicilios();
            
        }

        private void ajustarPantallaEdicion(int idPedidoEdicion)
        {
            rbDomicilio.IsEnabled = false;
            rbLocal.IsEnabled = false;
            pedido pedidoEdicionAjuste = PedidoDAO.obtenerPedidoPorID(idPedidoEdicion).pedido;
            string tipo = PedidoDAO.obtenerPedidoPorID(idPedidoEdicion).tipo;
            if (pedidoEdicionAjuste != null)
            {
                if (tipo.Equals("Local"))
                {
                    rbLocal.IsChecked = true;
                    rbLocal.IsEnabled = false;
                    gridLocal.Visibility = Visibility.Visible;
                    gridPedidoDomicilio.Visibility = Visibility.Hidden;
                    cbMesas.IsEnabled = false;
                    mesa mesaPedido = MiscDAO.ObtenerMesaPedido(idPedidoEdicion);
                    List < mesa > mesas = new List<mesa>();
                    mesas.Add(mesaPedido);
                    cbMesas.ItemsSource = null;
                    cbMesas.ItemsSource = mesas;
                    cbMesas.SelectedIndex = 0;
                }
                else {
                    rbDomicilio.IsChecked = true;
                    gridLocal.Visibility = Visibility.Hidden;
                    gridPedidoDomicilio.Visibility = Visibility.Visible;
                    cbCliente.IsEnabled = false;
                    cbDomicilio.IsEnabled = false;
                    cbNumeroTelefonico.IsEnabled = false;
                    cliente clientePedido = UsuariosDAO.ObtenerClientePorPedido(idPedidoEdicion);
                    cbCliente.Text = clientePedido.nombre;
                    direccion direccionPedido = UsuariosDAO.obtenerDomicilioPedido(idPedidoEdicion);
                    cbDomicilio.Text = direccionPedido.nombre;
                    telefono telefonoPedido = UsuariosDAO.obtenerNumeroPedido(idPedidoEdicion);
                    cbNumeroTelefonico.Text = telefonoPedido.numero;
                }
            }
            botonEliminarPedido.Visibility = Visibility.Visible;
            cargarResumen(pedidoEdicionAjuste.idPedido);
        }

        private void establecerComboboxMesas()
        {
            todoMesa = MiscDAO.ObtenerMesas();
            cbMesas.ItemsSource = null;
            cbMesas.ItemsSource = todoMesa;
            cbMesas.DisplayMemberPath = "numero";
            cbMesas.SelectedValuePath = "idMesa";
            cbMesas.IsEditable = false;
        }

        private void establecerComboboxTelefonos()
        {
            todoTelefono = UsuariosDAO.ObtenerNumerosTelefonicos();
            cbNumeroTelefonico.ItemsSource = null;
            cbNumeroTelefonico.ItemsSource = todoTelefono;
            cbNumeroTelefonico.DisplayMemberPath = "numero";
            cbNumeroTelefonico.SelectedValuePath = "idTelefono";
            cbNumeroTelefonico.IsEditable = true;
            cbNumeroTelefonico.IsTextSearchEnabled = false;
            cbNumeroTelefonico.Loaded += ComboNumeroTelefonico_Loaded;
        }

        private void establecerComboboxDomicilios()
        {
            cbDomicilio.IsEditable = true;
            cbDomicilio.IsTextSearchEnabled = false;
            cbDomicilio.Loaded += ComboNombreDomicilio_Loaded;
        }
        
        private void establecerComboboxClientes()
        {
            cbCliente.IsEditable = true;
            cbCliente.IsTextSearchEnabled = false;
            cbCliente.Loaded += ComboNombreClientes_Loaded;
        }

        private void ComboNombreClientes_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbCliente.Template.FindName("PART_EditableTextBox", cbCliente) is TextBox textBox)
            {
                textBox.TextChanged += ComboNombreClientes_TextChanged;
            }

            cbCliente.SelectionChanged += cbCliente_SelectionChanged;
        }

        private void FiltrarClientesPorTelefono(int idTelefono)
        {
            if (isUpdatingSelection) return;

            isUpdatingSelection = true;
            var clientesRelacionados = todoCLiente
                .Where(c => c.idTelefono == idTelefono)
                .ToList();

            cbCliente.ItemsSource = clientesRelacionados;
            cbCliente.DisplayMemberPath = "nombre";
            cbCliente.SelectedValuePath = "idCliente";
            isUpdatingSelection = false;
        }

        private void ComboNombreClientes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdatingSelection) return; 

            isEditingText = true; 

            if (sender is TextBox textBox)
            {
                string searchText = textBox.Text.ToLower();

                if (!string.IsNullOrEmpty(searchText))
                {
                    var clientesFiltrados = todoCLiente
                        .Where(c => c.nombre.ToLower().Contains(searchText))
                        .ToList();

                    cbCliente.ItemsSource = clientesFiltrados;
                    cbCliente.DisplayMemberPath = "nombre";
                    cbCliente.SelectedValuePath = "idCliente";

                    cbCliente.IsDropDownOpen = clientesFiltrados.Any(); 
                }
                else
                {
                   
                    cbCliente.ItemsSource = null;
                    cbCliente.IsDropDownOpen = false;
                }

               
                textBox.SelectionStart = textBox.Text.Length;
            }

            isEditingText = false;
        }

        private void cbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isEditingText) return; 

            if (cbCliente.SelectedItem is cliente clienteSeleccionado)
            {
                isUpdatingSelection = true; 
                if (cbCliente.Template.FindName("PART_EditableTextBox", cbCliente) is TextBox textBox)
                {
                    textBox.TextChanged -= ComboNombreClientes_TextChanged; 
                    textBox.Text = clienteSeleccionado.nombre; 
                    textBox.SelectionStart = textBox.Text.Length; 
                    textBox.TextChanged += ComboNombreClientes_TextChanged; 
                }

                FiltrarDomicilioPorCliente(clienteSeleccionado.idCliente);

                isUpdatingSelection = false;
            }
        }

        private void ComboNumeroTelefonico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                if (isUpdatingSelection) return;

                if (cbNumeroTelefonico.SelectedItem is telefono telefonoSeleccionado)
                {
                    isUpdatingSelection = true;


                    var textBox = cbNumeroTelefonico.Template.FindName("PART_EditableTextBox", cbNumeroTelefonico) as TextBox;
                    if (textBox != null)
                    {

                        textBox.Text = telefonoSeleccionado.numero;
                        textBox.SelectionStart = textBox.Text.Length; 
                    }

                    FiltrarClientesPorTelefono(telefonoSeleccionado.idTelefono);

                    cbNumeroTelefonico.IsDropDownOpen = false;

                    isUpdatingSelection = false; 
                }
            }
        }

        private void ComboNumeroTelefonico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdatingSelection) return; 

            if (sender is TextBox textBox)
            {
                isUpdatingSelection = true; 

                if (textBox.Text.Length > 10)
                {
                    textBox.Text = textBox.Text.Substring(0, 10);
                    textBox.SelectionStart = textBox.Text.Length;
                }

                if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text.All(char.IsDigit))
                {
                    string searchText = textBox.Text;
                    var telefonosFiltrados = todoTelefono
                        .Where(t => t.numero.Contains(searchText))
                        .ToList();

                    cbNumeroTelefonico.ItemsSource = telefonosFiltrados;
                    cbNumeroTelefonico.IsDropDownOpen = telefonosFiltrados.Any();
                }
                else
                {

                    cbNumeroTelefonico.ItemsSource = null;
                    cbNumeroTelefonico.IsDropDownOpen = false;
                }

                isUpdatingSelection = false;
            }
        }

        private void ComboNumeroTelefonico_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbNumeroTelefonico.Template.FindName("PART_EditableTextBox", cbNumeroTelefonico) is TextBox textBox)
            {
                textBox.TextChanged += ComboNumeroTelefonico_TextChanged;
            }
            cbNumeroTelefonico.SelectionChanged += ComboNumeroTelefonico_SelectionChanged;
        }

        private void ComboNombreDomicilio_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbDomicilio.Template.FindName("PART_EditableTextBox", cbDomicilio) is TextBox textBox)
            {
                textBox.TextChanged += ComboNombreDomicilio_TextChanged;
            }
        }

        private void FiltrarDomicilioPorCliente(int idCliente)
        {
            cbDomicilio.ItemsSource = null;

            var direccionesRelacionadas = todaDireccionCliente
                .Where(dc => dc.idCliente == idCliente)
                .Join(todoDireccion, dc => dc.idDireccion, d => d.idDireccion, (dc, d) => d)
                .ToList();


            cbDomicilio.ItemsSource = direccionesRelacionadas;
            cbDomicilio.DisplayMemberPath = "nombre";
            cbDomicilio.SelectedValuePath = "idDireccion";

            if (cbDomicilio.Template.FindName("PART_EditableTextBox", cbDomicilio) is TextBox textBox)
            {
                textBox.TextChanged -= ComboNombreDomicilio_TextChanged;
                textBox.TextChanged += ComboNombreDomicilio_TextChanged;
            }
        }

        private void ComboNombreDomicilio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)  
            {
                textBox.SelectionStart = textBox.Text.Length;
                string searchText = textBox.Text.ToLower();

                if (cbCliente.SelectedItem is cliente clienteSeleccionado)
                {
                    var direccionesRelacionadas = todaDireccionCliente
                        .Where(dc => dc.idCliente == clienteSeleccionado.idCliente)
                        .Join(todoDireccion, dc => dc.idDireccion, d => d.idDireccion, (dc, d) => d)
                        .Where(d => d.nombre.ToLower().Contains(searchText))
                        .ToList();

                    cbDomicilio.ItemsSource = direccionesRelacionadas;
                    cbDomicilio.DisplayMemberPath = "nombre";
                    cbDomicilio.SelectedValuePath = "idDireccion";
                    textBox.Text = searchText;
                    textBox.SelectionStart = searchText.Length;

                    cbDomicilio.IsDropDownOpen = direccionesRelacionadas.Any();
                }
            }
        }

        private void cargarResumen(int idPedido)
        {
            productosResumenEdicion.Clear();
            lvResumen.ItemsSource = null;
            List<ProductoResumenDTO> listaResumenDTO = PedidoDAO.recuperarProductos(idPedido);
            foreach (ProductoResumenDTO productoDTO in listaResumenDTO)
            {
                var matchingProducto = productos.FirstOrDefault(p => p.idProducto == productoDTO.idProducto);
                if (matchingProducto != null)
                {
                    matchingProducto.cantidad += productoDTO.cantidad;
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
                setCantidadesProductos(item.idProducto, item.cantidad);
            }
            
        }

        private void CrearCopiaResumen(ProductoResumenDTO item)
        {
            producto productoOriginal = new producto
            {
                idProducto = item.idProducto,
                cantidad = item.cantidad
            };
            productosResumenEdicion.Add(productoOriginal);
        }

        private void setCantidadesProductos(int idProducto, int cantidad)
        {
            var producto = lvProductos.Items.OfType<producto>()
                                             .FirstOrDefault(p => p.idProducto == idProducto);

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
                lvProductos.ItemsSource= null;
                productos = ProductoDAO.ObtenerListaProductos();
                lvProductos.ItemsSource = productos;
            }
            catch (EntityException ex)
            {
                GestorCuadroDialogo.MostrarError("Error de conexión", "Error al acceder a la base de datos.");
            }

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
            lvProductos.ItemsSource =null;
            lvProductos.ItemsSource = productos.ToList();
        }

        private void rbDomicilio_Checked(object sender, RoutedEventArgs e)
        {
            gridPedidoDomicilio.Visibility = Visibility.Visible;
            gridLocal.Visibility = Visibility.Hidden;
            tipoPedido = "Domicilio";
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
            var resumenItem = productosResumen.FirstOrDefault(r => r.idProducto == producto.idProducto);
            if (resumenItem == null)
            {
                resumenItem = new producto
                {
                    idProducto = producto.idProducto,
                    nombre = producto.nombre,
                    cantidad = cantidad,
                    precio = producto.precio * cantidad
                };

                productosResumen.Add(resumenItem);
            }
            else
            {
                resumenItem.cantidad = cantidad;
                resumenItem.precio = producto.precio * cantidad;
            }
            lvResumen.ItemsSource = null;
            lvResumen.ItemsSource = productosResumen;

            CalcularTotalResumen();

        }

        private decimal CalcularTotalResumen()
        {
            decimal total = (decimal)lvResumen.Items.OfType<producto>().Sum(item => item.precio);
            etiquetaTotal.Content ="Total: $" + total.ToString();
            return total;
        }

        private void rbLocal_Checked(object sender, RoutedEventArgs e)
        {
            gridLocal.Visibility = Visibility.Visible;
            gridPedidoDomicilio.Visibility = Visibility.Hidden;
            cbCliente.Text = null;
            cbDomicilio.Text = null;
            cbNumeroTelefonico.Text = null;
            tipoPedido = "Local";
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
            var productoSeleccionado = listViewItem.DataContext as producto;
            if (productoSeleccionado != null)
            {
                MessageBoxResult respuesta = GestorCuadroDialogo.MostrarConfirmacion(
                    "¿Eliminar " + productoSeleccionado.nombre + "?",
                    "Confirmación"
                );

                if (respuesta != MessageBoxResult.No)
                {
                    productosResumen.Remove(productoSeleccionado);
                    lvResumen.ItemsSource = null;
                    lvResumen.ItemsSource = productosResumen;

                    ActualizarCantidadProductoEnLvProductos(productoSeleccionado.idProducto);
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
                int resultadoBD = PedidoDAO.EliminarRelacionesPedidos(idPedidoEdicion);
                resultadoBD = PedidoDAO.eliminarPedido(idPedidoEdicion);
                resultadoBD = PedidoDAO.incrementarCantidadProductos(productosResumen);
                if (resultadoBD == 0) {
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
                List<producto> nuevosProductos = new List<producto>();
                List<producto> eliminadosProductos = new List<producto>();

                (nuevosProductos, eliminadosProductos, resultadoBD) = CheckAddedNewAndDeletedProductos(productosResumen, productosResumenEdicion, nuevosProductos, eliminadosProductos);

                agregarProductosPedido(nuevosProductos);
                eliminarProductosPedido(eliminadosProductos);

                decimal nuevoTotal = CalcularTotalResumen();
                Console.WriteLine("Nuevo total es de " + nuevoTotal);
                PedidoDAO.actualizarTotalPedido(nuevoTotal, idPedidoEdicion);

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

        private int agregarProductosPedido(List<producto> nuevosProductos)
        {
            int resultadoBD = 0;
            foreach (producto productoResumen in nuevosProductos)
            {
                Console.WriteLine("Estoy intentando registrar " + productoResumen.nombre);
                resultadoBD = PedidoDAO.registrarPedidoProducto(idPedidoEdicion, productoResumen.idProducto, (int)productoResumen.cantidad);
                resultadoBD = ProductoDAO.ReducirCantidadProducto(productoResumen, (int) productoResumen.cantidad);
            }
            return resultadoBD;
        }

        private (List<producto> nuevos, List<producto> eliminados, int resultadoDB) CheckAddedNewAndDeletedProductos(List<producto> productosResumen, List<producto> originalProductos, List<producto> nuevosProductos, List<producto> eliminadosProductos)
        {
            int resultadoDB = 0;
            Console.WriteLine("Aqui si estoy llegando mano ");
            foreach (var producto in productosResumen)
            {
                if (!originalProductos.Any(p => p.idProducto == producto.idProducto))
                {
                    Console.WriteLine("Se va a anadir esto al pedido " + producto.nombre);
                    nuevosProductos.Add(producto);
                }
            }

            foreach (var producto in originalProductos)
            {
                if (!productosResumen.Any(p => p.idProducto == producto.idProducto))
                {
                    eliminadosProductos.Add(producto);
                }
            }

            foreach (var producto in originalProductos)
            {
                var resumenProducto = productosResumen.FirstOrDefault(p => p.idProducto == producto.idProducto);
                if (resumenProducto != null && resumenProducto.cantidad != producto.cantidad)
                {
                    if (resumenProducto.cantidad < producto.cantidad)
                    {
                        int cantidadRestar = (int)(producto.cantidad - resumenProducto.cantidad);
                        resultadoDB = incrementarCantidadProducto(resumenProducto, cantidadRestar);
                        Console.WriteLine("Resultado incrementarCantidadProducto " + resultadoDB);
                        resultadoDB = decrementarCantidadPedido(resumenProducto);
                        Console.WriteLine("Resultado decrementarCantidadPedido " + resultadoDB);
                    }
                    else if (resumenProducto.cantidad > producto.cantidad)
                    {
                        int cantidadRestar = (int)(resumenProducto.cantidad - producto.cantidad );
                        resultadoDB = decrementarCantidadProducto(resumenProducto, cantidadRestar);
                        Console.WriteLine("Resultado decrementarCantidadProducto " + resultadoDB);
                        resultadoDB = incrementarCantidadPedido(resumenProducto);
                        Console.WriteLine("Resultado incrementarCantidadPedido " + resultadoDB);
                    }
                }
            }

            return (nuevosProductos, eliminadosProductos, resultadoDB);
        }

        private int eliminarProductosPedido(List<producto> eliminadosProductos)
        {
            int resultadoBD = 0;
            foreach (producto productoResumen in eliminadosProductos)
            {
                resultadoBD = incrementarCantidadProducto(productoResumen, (int)productoResumen.cantidad);
                resultadoBD = PedidoDAO.EliminarRelacionPedido(idPedidoEdicion, productoResumen.idProducto);
            }
            return resultadoBD;
        }

        private int decrementarCantidadPedido(producto resumenProducto)
        {
            return PedidoDAO.decrementarCantidadPedido(idPedidoEdicion, resumenProducto);
        }

        private int incrementarCantidadPedido(producto resumenProducto)
        {
            return PedidoDAO.incrementarCantidadPedido(idPedidoEdicion, resumenProducto);
        }

        private int decrementarCantidadProducto(producto producto, int cantidad)
        {
            return  ProductoDAO.ReducirCantidadProducto(producto, cantidad);
        }

        private int incrementarCantidadProducto(producto producto, int cantidad)
        {
            return ProductoDAO.incrementarCantidadProducto(producto, cantidad);
        }

        private void AltaPedido()
        {
            cantidad = CalcularTotalResumen();
            DateTime fechaActual = DateTime.Today;
            string fecha = fechaActual.ToString("dd MM yyyy");
            idCorteCaja = 2; //TODO
            cliente clientePedido = new cliente();
            direccion domicilioPedido = new direccion();
            telefono telefonoPedido = new telefono();
            reducirCantidadProductos();
            var pedido = new pedido
            {
                fecha = fecha,
                cantidad = cantidad,
                idEstadoPedido = 2,
                idCorteCaja = idCorteCaja,
                idDireccion = null,
                idTelefono = null
            };
            if (rbDomicilio.IsChecked == true)
            {
                nombreCliente = cbCliente.Text;
                domicilioCliente = cbDomicilio.Text;
                numeroCliente = cbNumeroTelefonico.Text.Trim();
                bool checkDomicilio = ContieneTextoDireccion(todoDireccion, cbDomicilio.Text);
                bool checkTelefono = ContieneTextoTelefono(todoTelefono, cbNumeroTelefonico.Text);
                bool checkCliente = ContieneTextoCliente(todoCLiente, cbCliente.Text);
                if (!checkTelefono)
                {
                    var telefono = new telefono
                    {
                        numero = numeroCliente
                    };
                    telefonoPedido.idTelefono = UsuariosDAO.registrarTelefono(telefono.numero);
                }
                else
                {
                    if (cbNumeroTelefonico.SelectedItem == null) return;
                    var telefonoSeleccionado = (telefono)cbNumeroTelefonico.SelectedItem;
                    telefonoPedido = telefonoSeleccionado;
                }

                if (!checkDomicilio)
                {
                    var domicilio = new direccion
                    {
                        nombre = domicilioCliente
                    };
                    domicilioPedido.idDireccion = UsuariosDAO.registrarDomicilio(domicilio.nombre);
                }
                else
                {
                    if (cbDomicilio.SelectedItem == null) return;
                    var domicilioSeleccionado = (direccion)cbDomicilio.SelectedItem;
                    domicilioPedido = domicilioSeleccionado;
                }

                if (!checkCliente)
                {
                    var cliente = new cliente
                    {
                        nombre = nombreCliente,
                        idTelefono = telefonoPedido.idTelefono
                    };
                    clientePedido.idCliente = UsuariosDAO.registrarCliente(cliente);
                }
                else
                {
                    if (cbCliente.SelectedItem == null) return;
                    var clienteSeleccionado = (cliente)cbCliente.SelectedItem;
                    clientePedido = clienteSeleccionado;
                }
                pedido.idCorteCaja = idCorteCaja;
                pedido.idDireccion = domicilioPedido.idDireccion;
                pedido.idTelefono = telefonoPedido.idTelefono;
            }
            
            Console.WriteLine(fecha);
            int idPedidoRegistro = PedidoDAO.registrarNuevoPedido(pedido);
            Console.WriteLine(idPedidoRegistro);
            
            if (tipoPedido.Equals("Local"))
            {
                int idMesa = int.Parse(cbMesas.Text.Trim());
                registrarPedidoLocal(idPedidoRegistro, idMesa);
            }
            else
            {
                registrarPedidoCliente(clientePedido.idCliente, idPedidoRegistro);
            }

            foreach (producto productoResumen in productosResumen)
            {
                Console.WriteLine(idPedidoRegistro + " " + productoResumen.idProducto + " " + (int)productoResumen.cantidad);
                PedidoDAO.registrarPedidoProducto(idPedidoRegistro, productoResumen.idProducto, (int)productoResumen.cantidad);
            }
            
            MessageBox.Show("Pedido creado correctamente.",
                                    "Pedido creado",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
            VentanaPrincipal.CambiarPagina(new GUI_ConsultarPedidos());
        }

        private int reducirCantidadProductos()
        {
            int resultadoDB = ProductoDAO.reducirCantidadProductos(productosResumen);
            return resultadoDB;
        }
        
        private void registrarPedidoCliente(int idCliente, int idPedido)
        {
            PedidoDAO.registrarPedidoCliente(idCliente, idPedido);
        }

        private void registrarPedidoLocal(int idPedido,int idMesa)
        {
            PedidoDAO.registrarNuevoPedidoLocal(idPedido, idMesa);
        }

        private bool ContieneTextoTelefono(List<telefono> lista, string texto)
        {
            return lista.Any(t => t.numero != null && t.numero.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool ContieneTextoCliente(List<cliente> lista, string texto)
        {
            return lista.Any(c => c.nombre != null && c.nombre.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool ContieneTextoDireccion(List<direccion> lista, string texto)
        {
            return lista.Any(d => d.nombre != null && d.nombre.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0);
        }

    }
}
