using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ItalianPicza.Model
{
    public class ProductoGeneral
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string TipoPedido { get; set; } // "Ingrediente" o "Producto"
        public int Cantidad {  get; set; }
        public string UnidadMedida {  get; set; }
        public decimal Precio {  get; set; } 
    }
}
