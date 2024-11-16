using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPicza.Model
{
    internal class PedidoGeneral
    {
            public int IdPedido { get; set; }
            public string Fecha { get; set; }
            public decimal Cantidad { get; set; }
            public string TipoPedido { get; set; } // Local o Domicilio
            public string InfoExtra { get; set; }  // Mesa o Cliente

    }
}
