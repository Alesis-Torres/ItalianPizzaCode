using ItalianPicza.DatabaseModel.DAO_s;
using ItalianPicza.DatabaseModel.DataBaseMapping;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using Microsoft.Win32;
namespace ItalianPicza
{
    public partial class GUI_ReporteInventario : Page
    {
        private List<producto> productos;
        private List<ingrediente> ingredientes;
        public GUI_ReporteInventario()
        {
            InitializeComponent();
            CargarProductos();
            //CargarIngredientes();
        }

        string observacion, cantidadActual;

        private void CargarProductos()
        {
            ProductosDAO productosDAO = new ProductosDAO();

            List<producto> productos = new List<producto>();

            try
            {
                productos = productosDAO.ObtenerProductos();
                lvProductos.ItemsSource = productos;

            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }
        }
        /*
        private void CargarIngredientes()
        {
            IngredientesDAO ingredientesDAO = new IngredientesDAO();

            List<ingrediente> ingredientes = new List<ingrediente>();

            try
            {
                ingredientes = ingredientesDAO.ObtenerIngredientes();
                lvProductos.ItemsSource = ingredientes;

            }
            catch (EntityException)
            {
                GestorCuadroDialogo.MostrarError
                           ("No hay conexión con la base de datos, por favor, intentelo más tarde",
                           "Sin conexión a la base de datos");
            }
        }
        */
        private void Guardar(object sender, RoutedEventArgs e)
        {
            bool camposInvalidos = false;

            foreach (var item in lvProductos.Items)
            {
                if (item is producto productoItem)
                {
                    cantidadActual = productoItem.cantidadActual?.ToString() ?? string.Empty;
                    observacion = productoItem.observacion ?? string.Empty;

                    if (ExistenCamposVaciosReporte())
                    {
                        camposInvalidos = true;
                        break;
                    }

                    if (ExistenCamposInvalidosReporte())
                    {
                        camposInvalidos = true;
                        break;
                    }
                }
            }

            if (camposInvalidos)
            {
                return;
            }

            ProductosDAO productosDAO = new ProductosDAO();
            bool reporteGenerado = true;

            foreach (var item in lvProductos.Items)
            {
                if (item is producto productoItem)
                {
                    try
                    {
                        int resultado = productosDAO.GenerarReporteProducto(productoItem);

                        if (resultado != 1)
                        {
                            reporteGenerado = false;
                            break;
                        }
                    }
                    catch (EntityException)
                    {
                        GestorCuadroDialogo.MostrarError(
                            "No hay conexión con la base de datos, por favor, inténtelo más tarde.",
                            "Sin conexión");
                        return;
                    }
                }
            }

            if (reporteGenerado)
            {
                GestorCuadroDialogo.MostrarInformacion(
                    "Reporte de inventario creado exitosamente en el sistema.",
                    "Reporte creado");
                VentanaPrincipal.CambiarPagina(new GUI_Inventario());
            }
            else
            {
                GestorCuadroDialogo.MostrarError(
                    "El reporte de inventario no pudo ser creado correctamente.",
                    "Registro fallido");
            }
        }

        private T FindVisualChild<T>(DependencyObject parent, string childName) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t && t.Name == childName)
                {
                    return t;
                }

                var childOfChild = FindVisualChild<T>(child, childName);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private bool ExistenCamposInvalidosReporte()
        {
            bool hayCamposInvalidos = false;

            if (!hayCamposInvalidos && ValidarDatos.ExistenCaracteresInvalidosParaNombre(observacion))
            {
                GestorCuadroDialogo.MostrarAdvertencia(
                    "La observación no es válida, por favor, ingrésela nuevamente.",
                    "Observación inválida");
                hayCamposInvalidos = true;
            }

            return hayCamposInvalidos;
        }

        private bool ExistenCamposVaciosReporte()
        {
            bool existenCamposVacios = false;

            if (ValidarDatos.EsCadenaVacia(cantidadActual))
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                    "Existen campos que están vacíos, por favor, ingrese toda la información solicitada.",
                    "Campos vacíos");
            }

            if (ValidarDatos.EsCadenaVacia(observacion))
            {
                existenCamposVacios = true;
                GestorCuadroDialogo.MostrarAdvertencia(
                    "La observación es obligatoria, por favor, ingrese una observación.",
                    "Observación obligatoria");
            }

            return existenCamposVacios;
        }

        private void ExportarPDF(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string rutaArchivo = saveFileDialog.FileName;

                try
                {
                    PdfDocument documento = new PdfDocument();

                    PdfPage paginaGeneral = documento.AddPage();
                    paginaGeneral.Orientation = PdfSharp.PageOrientation.Landscape;
                    XGraphics graficoPaginaGeneral = XGraphics.FromPdfPage(paginaGeneral);
                    XFont fuenteTitulo = new XFont("Verdana", 14);
                    XFont fuenteEncabezado = new XFont("Verdana", 10);
                    XFont fuenteContenido = new XFont("Verdana", 10);

                    double yPos = 40;
                    graficoPaginaGeneral.DrawString("Reporte de Inventario", fuenteTitulo, XBrushes.Black, new XPoint(300, yPos));
                    yPos += 30;
                    graficoPaginaGeneral.DrawString("ID Producto", fuenteEncabezado, XBrushes.Black, new XPoint(40, yPos));
                    graficoPaginaGeneral.DrawString("Caducidad", fuenteEncabezado, XBrushes.Black, new XPoint(120, yPos));
                    graficoPaginaGeneral.DrawString("Nombre", fuenteEncabezado, XBrushes.Black, new XPoint(220, yPos));

                    yPos += 20;

                    double alturaFila = 20;
                    foreach (var item in lvProductos.Items)
                    {
                        if (item is producto productoItem)
                        {
                            graficoPaginaGeneral.DrawString(productoItem.idProducto.ToString(), fuenteContenido, XBrushes.Black, new XPoint(40, yPos));
                            graficoPaginaGeneral.DrawString(productoItem.caducidad ?? "N/A", fuenteContenido, XBrushes.Black, new XPoint(120, yPos));
                            graficoPaginaGeneral.DrawString(productoItem.nombre ?? "N/A", fuenteContenido, XBrushes.Black, new XPoint(220, yPos));

                            yPos += alturaFila;

                            if (yPos > paginaGeneral.Height - 40)
                            {
                                paginaGeneral = documento.AddPage();
                                paginaGeneral.Orientation = PdfSharp.PageOrientation.Landscape;
                                graficoPaginaGeneral = XGraphics.FromPdfPage(paginaGeneral);
                                yPos = 40;

                                graficoPaginaGeneral.DrawString("ID Producto", fuenteEncabezado, XBrushes.Black, new XPoint(40, yPos));
                                graficoPaginaGeneral.DrawString("Caducidad", fuenteEncabezado, XBrushes.Black, new XPoint(120, yPos));
                                graficoPaginaGeneral.DrawString("Nombre", fuenteEncabezado, XBrushes.Black, new XPoint(220, yPos));

                                yPos += 20;
                            }
                        }
                    }

                    PdfPage paginaDetalle = documento.AddPage();
                    paginaDetalle.Orientation = PdfSharp.PageOrientation.Landscape;
                    XGraphics graficoPaginaDetalle = XGraphics.FromPdfPage(paginaDetalle);

                    yPos = 40;
                    graficoPaginaDetalle.DrawString("Reporte de Inventario", fuenteTitulo, XBrushes.Black, new XPoint(300, yPos));
                    yPos += 30;

                    graficoPaginaDetalle.DrawString("ID Producto", fuenteEncabezado, XBrushes.Black, new XPoint(40, yPos));
                    graficoPaginaDetalle.DrawString("Cantidad Registrada", fuenteEncabezado, XBrushes.Black, new XPoint(220, yPos));
                    graficoPaginaDetalle.DrawString("Cantidad Actual", fuenteEncabezado, XBrushes.Black, new XPoint(420, yPos));
                    graficoPaginaDetalle.DrawString("Observación", fuenteEncabezado, XBrushes.Black, new XPoint(620, yPos));

                    yPos += 20;

                    foreach (var item in lvProductos.Items)
                    {
                        if (item is producto productoItem)
                        {
                            graficoPaginaDetalle.DrawString(productoItem.idProducto.ToString(), fuenteContenido, XBrushes.Black, new XPoint(40, yPos));
                            graficoPaginaDetalle.DrawString(productoItem.cantidadRegistrada?.ToString() ?? "0", fuenteContenido, XBrushes.Black, new XPoint(220, yPos));
                            graficoPaginaDetalle.DrawString(productoItem.cantidadActual?.ToString() ?? "0", fuenteContenido, XBrushes.Black, new XPoint(420, yPos));
                            graficoPaginaDetalle.DrawString(productoItem.observacion ?? "N/A", fuenteContenido, XBrushes.Black, new XPoint(620, yPos));

                            yPos += alturaFila;

                            if (yPos > paginaDetalle.Height - 40)
                            {
                                paginaDetalle = documento.AddPage();
                                paginaDetalle.Orientation = PdfSharp.PageOrientation.Landscape;
                                graficoPaginaDetalle = XGraphics.FromPdfPage(paginaDetalle);
                                yPos = 40;

                                graficoPaginaDetalle.DrawString("ID Producto", fuenteEncabezado, XBrushes.Black, new XPoint(40, yPos));
                                graficoPaginaDetalle.DrawString("Cantidad Registrada", fuenteEncabezado, XBrushes.Black, new XPoint(220, yPos));
                                graficoPaginaDetalle.DrawString("Cantidad Actual", fuenteEncabezado, XBrushes.Black, new XPoint(420, yPos));
                                graficoPaginaDetalle.DrawString("Observación", fuenteEncabezado, XBrushes.Black, new XPoint(620, yPos));

                                yPos += 20;
                            }
                        }
                    }

                    documento.Save(rutaArchivo);

                    GestorCuadroDialogo.MostrarInformacion("El reporte ha sido exportado como PDF.", "Exportación Exitosa");
                }
                catch (Exception ex)
                {
                    GestorCuadroDialogo.MostrarError("Hubo un error al exportar el reporte: " + ex.Message, "Error de Exportación");
                }
            }
            else
            {
                GestorCuadroDialogo.MostrarInformacion("La exportación ha sido cancelada.", "Cancelado");
            }
        }


        private void irRegresar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_Inventario());
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_ReporteInventario());
        }
    }
}
