using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using static ItalianPicza.GUI_AgregarPedidoProveedor;
using static ItalianPicza.GUI_VerDetallesPedidoProveedores;

namespace ItalianPicza
{
    public partial class GUI_AgregarPedidoProveedor : Page
    {
        public GUI_AgregarPedidoProveedor()
        {
            InitializeComponent();

            List<ProductosProveedor> productosProveedor = new List<ProductosProveedor>()
            {
                new ProductosProveedor() { ProductoProveedor = "Tomate Saladet", CantidadProductoProveedor = "3" , UnidaMedidaProductoProveedor = "Kilogramo", CostoUnidadProductoProveedor = "$ 12.00" },
                new ProductosProveedor() { ProductoProveedor = "Aguacatae", CantidadProductoProveedor = "4", UnidaMedidaProductoProveedor = "Kilogramo", CostoUnidadProductoProveedor = "$ 40.00" },
                new ProductosProveedor() { ProductoProveedor = "Jitomate Saladet", CantidadProductoProveedor = "4", UnidaMedidaProductoProveedor = "Kilogramo", CostoUnidadProductoProveedor = "$ 20.50"},
                new ProductosProveedor() { ProductoProveedor = "Cebollin", CantidadProductoProveedor = "4", UnidaMedidaProductoProveedor = "Kilogramo", CostoUnidadProductoProveedor = "$ 25.00" },
            };

            lvProductosProveedor.ItemsSource = productosProveedor;

            List<ResumenPedidoProveedor> resumenPedidoProveedor = new List<ResumenPedidoProveedor>()
            {
                new ResumenPedidoProveedor() { CantidadResumen = "3", ProductoResumen = "Tomate Saladet", CostoResumen = "$ 12.00" },
                new ResumenPedidoProveedor() { CantidadResumen = "4", ProductoResumen = "Aguacatae", CostoResumen = "$ 40.00" },
                new ResumenPedidoProveedor() { CantidadResumen = "4", ProductoResumen = "Jitomate Saladet",  CostoResumen = "$ 20.50"},
                new ResumenPedidoProveedor() { CantidadResumen = "4", ProductoResumen = "Cebollin", CostoResumen = "$ 25.00" },
            };

            lvResumenPedido.ItemsSource = resumenPedidoProveedor;

        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            /*
            var selectedProduct = (ProductosProveedor)lvProductosProveedor.SelectedItem;

            if (selectedProduct != null)
            {
                var resumenItem = new ResumenPedidoProveedor()
                {
                    CantidadResumen = selectedProduct.CantidadProductoProveedor,
                    ProductoResumen = selectedProduct.ProductoProveedor,
                    CostoResumen = selectedProduct.CostoUnidadProductoProveedor
                };

                var resumenPedido = (List<ResumenPedidoProveedor>)lvResumenPedido.ItemsSource;
                resumenPedido.Add(resumenItem);

                lvResumenPedido.ItemsSource = null;
                lvResumenPedido.ItemsSource = resumenPedido;
            }
            */
        }

        private void Quitar(object sender, RoutedEventArgs e)
        {
            /*
            var selectedResumen = (ResumenPedidoProveedor)lvResumenPedido.SelectedItem;

            if (selectedResumen != null)
            {
                var resumenPedido = (List<ResumenPedidoProveedor>)lvResumenPedido.ItemsSource;
                resumenPedido.Remove(selectedResumen);

                lvResumenPedido.ItemsSource = null;
                lvResumenPedido.ItemsSource = resumenPedido;
            }
            */
        }

        private void GuardarPedido(object sender, RoutedEventArgs e)
        {/*
            pedidoproveedor nuevoPedido = new pedidoproveedor()
            {
                fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                monto = null,
                idProveedor = 0,
                idEmpleado = 0
                monto = CalcularMontoTotal(),
                idProveedor = ObtenerIdProveedorSeleccionado(),
                idEmpleado = ObtenerIdEmpleado()
        };

            PedidoProveedorDAO pedidosDAO = new PedidoProveedorDAO();

            try
            {
                int resultado = pedidosDAO.GuardarNuevoPedidoProveedor(nuevoPedido);

                if (resultado == 1)
                {
                    //var resumenPedido = (List<ResumenPedidoProveedor>)lvResumenPedido.ItemsSource;
                    
                    foreach (var item in resumenPedido)
                    {
                        pedidoproveedorproducto productoPedido = new pedidoproveedorproducto()
                        {
                            idPedidoProveedor = nuevoPedido.idPedidoProveedor, 
                            idProducto = item.ProductoResumen.idProducto,
                            cantidad = int.Parse(item.CantidadResumen) 
                        };

                        pedidosDAO.GuardarProductoPedidoProveedor(productoPedido);
                    }
                    GestorCuadroDialogo.MostrarInformacion("Pedido guardado correctamente", "Éxito");
                    VentanaPrincipal.CambiarPagina(new GUI_Pedidos());
                }
                else
                {
                    GestorCuadroDialogo.MostrarError("No se pudo guardar el pedido", "Error");
                }
            }
            catch (Exception ex)
            {
                GestorCuadroDialogo.MostrarError("Error al guardar el pedido: " + ex.Message, "Error");
            }*/
        }
        
        private byte[] CalcularMontoTotal()
        {
            var resumenPedido = (List<ResumenPedidoProveedor>)lvResumenPedido.ItemsSource;
            decimal total = 0;

            foreach (var item in resumenPedido)
            {
                string costoStr = item.CostoResumen.Replace("$", "").Trim();
                if (decimal.TryParse(costoStr, out decimal costo))
                {
                    total += costo * decimal.Parse(item.CantidadResumen);
                }
            }

            return BitConverter.GetBytes((double)total);
        }
        private void CancelarPedido(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Pedidos());
        }

        public class ProductosProveedor
        {
            public string ProductoProveedor { get; set; }
            public string CantidadProductoProveedor { get; set; }
            public string UnidaMedidaProductoProveedor { get; set; }
            public string CostoUnidadProductoProveedor { get; set; }
        }

        public class ResumenPedidoProveedor
        {
            public string CantidadResumen { get; set; }
            public string ProductoResumen { get; set; }
            public string CostoResumen { get; set; }
        }
    }
}
