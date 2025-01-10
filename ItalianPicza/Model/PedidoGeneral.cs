using ItalianPicza.DatabaseModel.DataBaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ItalianPicza.Model
{
    internal class PedidoGeneral
    {
        public int IdPedido { get; set; }
        public string Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public string TipoPedido { get; set; } // "Domicilio" o "Local"
        public string estado { get; set; }
        public string Detalle { get; set; }
        public List<producto> productosRelacionados { get; set;}

        public Visibility IsListoButtonVisible => estado == "preparacion" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsPreparacionButtonVisible => estado == "pendiente" ? Visibility.Visible : Visibility.Collapsed;
        public string ProductosDisplay => productosRelacionados != null && productosRelacionados.Any()
        ? string.Join(", \n", productosRelacionados.Select(p => p.nombre))
        : "No products";
    }
}
