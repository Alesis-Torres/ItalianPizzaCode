using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ItalianPicza
{
    public partial class GUI_Proveedores : Page
    {
        public GUI_Proveedores()
        {
            InitializeComponent();

            List<Proveedor> proveedores = new List<Proveedor>()
            {
                new Proveedor() { NombreProveedor = "Molinos Italia", DescripcionProveedor = "Empresa especializada en la producción de harinas de alta calidad para pizzerías. Ofrecen una variedad de mezclas de harina para diferentes estilos de pizza, desde la napolitana clásica hasta opciones sin gluten.", ImagenProveedor = "/Imagenes/Logos/image 9.png" },
                new Proveedor() { NombreProveedor = "Lacteos Frescos S.A.", DescripcionProveedor = "Proveedor de productos lácteos frescos, incluyendo mozzarella de alta calidad, queso parmesano y ricotta. Trabajan directamente con granjas locales para garantizar la frescura y calidad de sus productos.", ImagenProveedor = "/Imagenes/Logos/image 9.png" },
                new Proveedor() { NombreProveedor = "Huerta Organica", DescripcionProveedor = "Cooperativa de agricultores que suministra verduras y hierbas orgánicas frescas. Ofrecen una amplia gama de ingredientes para pizzas, como tomates, albahaca, rúcula y pimientos, todos cultivados sin pesticidas.", ImagenProveedor = "/Imagenes/Logos/image 9.png" },
                new Proveedor() { NombreProveedor = "Carnes Selectas", DescripcionProveedor = "Proveedor especializado en carnes curadas y embutidos de alta calidad. Ofrecen una selección de pepperoni, salami, jamón y otros productos cárnicos ideales para pizzas gourmet.", ImagenProveedor = "/Imagenes/Logos/image 9.png" },
            };

            lvProveedores.ItemsSource = proveedores;

            //CargarProveedores();
        }
        /*
        private void CargarProveedores()
        {
            ProveedoresDAO proveedoresDAO = new ProveedoresDAO();

            try
            {
                List<Proveedor> proveedores = proveedoresDAO.ObtenerListaProveedores();

                lvProveedores.ItemsSource = proveedores;
            }
            catch (EntityException ex)
            {
                MessageBox.Show("Error al cargar la lista de proveedores: " + ex.Message);
            }
        }
        */
        private void AgregarProveedor(object sender, RoutedEventArgs e)
        {
            VentanaPrincipal.CambiarPagina(new GUI_AgregarProveedores());
        }

        public class Proveedor
        {
            public string NombreProveedor { get; set; }
            public string DescripcionProveedor { get; set; }
            public string ImagenProveedor { get; set; }
        }

        private void LvProveedores_Loaded(object sender, RoutedEventArgs e)
        {
            AjustarAnchoColumnas();
        }

        private void LvProveedores_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AjustarAnchoColumnas();
        }

        private void AjustarAnchoColumnas()
        {
            double totalWidth = lvProveedores.ActualWidth - 35; // Resta para el borde/scrollbar

            colImagen.Width = totalWidth * 0.15;
            colNombre.Width = totalWidth * 0.25;
            colDescripcion.Width = totalWidth * 0.40;
            colModificar.Width = totalWidth * 0.20;
        }

    }
}
